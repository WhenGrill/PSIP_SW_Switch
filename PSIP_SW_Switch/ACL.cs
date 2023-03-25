using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using PacketDotNet;
using PacketDotNet.Lsa;
using PacketDotNet.Utils;
using PSIP_SW_Switch;
using SharpPcap;

namespace PSIP_SW_Switch
{
    public enum ACLRuleDirection
    {
        INBOUND,
        OUTBOUND
    };

    enum ACLAllowance
    {
        ALLOW,
        DENY
    };

    public enum Protocol
    {
        TCP,
        UDP,
        ICMP,
        ANY
    }

    enum ICMPType: int
    {
        EchoReply = 0,
        DestinationUnreachable = 3,
        Redirect = 5,
        EchoRequest = 8,
        RouterAdvertisement = 9,
        RouterSolicitation = 10,
        TimeExceeded = 11,
        ParameterProblem = 12,
        Timestamp = 13,
        TimestampReply = 14,
        ExtendedEchoRequest = 42,
        ExtendedEchoReply = 43,
        ANY = int.MaxValue
    }

    static class ACL
    {
        public static BindingList<ACLRule> AclRules = new();

        public static bool Enabled = false;
        public static object AclRulesListLock = new();

        public static void Init()
        {
            GUIController.AclGUI.Init();
            // GUIController.AclGUI.AddRule(new ACLRule(ACLRuleDirection.INBOUND, new PacketInfo()));

        }
        public static void EnableProcessing()
        {
            Enabled = true;
        }

        public static void DisableProcessing()
        {
            Enabled = false;
        }

        public static void RefreshGUI()
        {
            GUIController.AclGUI.Refresh();
        }

        public static void AddRule()
        {
            var result = GUIController.AclGUI.AddRule();
            if (result.Item1)
            {
                SysLog.Log(SysLogSeverity.Informational, "[ACL - New Rule] " + result.Item2.ToStringRuleDescription());
            }
        }

        public static void DeleteAllRules()
        {
            Task.Run(() =>
            {
                GUIController.AclGUI.DeleteAllRules();
            });
        }

        public static bool IsAllowed<T>(T packet, ACLRuleDirection direction, ILiveDevice device)
        {
            if (Enabled == false)
            {
                //SysLog.Log(SysLogSeverity.Warning, "[ACL] Packet permitted by default");
                return true;
            }

            PacketInfo checkedPacket = getPacketInfo(packet);
            
            if(direction == ACLRuleDirection.INBOUND)
                checkedPacket.InDevice = device;
            else if(direction == ACLRuleDirection.OUTBOUND)
                checkedPacket.OutDevice = device;

            foreach (var rule in AclRules)
            {
                if (IsMatch(rule, checkedPacket, direction))
                {
                    rule.noMatches++;

                    if (rule.Allowance == ACLAllowance.ALLOW)
                    {
                        //SysLog.Log(SysLogSeverity.Informational, rule.ToString());
                        return true;
                    }
                    
                    SysLog.Log(SysLogSeverity.Alert, rule.ToString());
                    return false;
                    
                }
            }

            //ACLRule defDeny = new ACLRule(ACLRuleType.INBOUND, checkedPacket, device);
            //SysLog.Log(SysLogSeverity.Warning, "[ACL] Packet denied by default");
            return false; // default deny
        }

        private static bool IsMatch(ACLRule rule, PacketInfo checkedPacketInfo, ACLRuleDirection packetDirection)
        {
            // Check if rule has the same direction as packet
            if (rule.AclRuleDirection != packetDirection)
            {
                return false;
            }

            // If rule is not universal for any interface
            if (rule.device != null)
            {
                // Check if interfaces match with Packet 
                if (rule.AclRuleDirection == ACLRuleDirection.INBOUND && rule.device != checkedPacketInfo.InDevice)
                {
                    return false;
                }
                else
                {
                    var outDevice = checkedPacketInfo.OutDevice ?? rule.device;

                    if (rule.AclRuleDirection == ACLRuleDirection.OUTBOUND && rule.device != outDevice)
                    {
                        return false;
                    }
                }

                
            }
            
            // Check if the rule matches the packet info
            if (!rule.packetInfo.SourcePhysicalAddress.Equals(PhysicalAddress.None) && !rule.packetInfo.SourcePhysicalAddress.Equals(checkedPacketInfo.SourcePhysicalAddress))
            {
                return false;
            }
            if (!rule.packetInfo.DestinationPhysicalAddress.Equals(PhysicalAddress.None) && !rule.packetInfo.DestinationPhysicalAddress.Equals(checkedPacketInfo.DestinationPhysicalAddress))
            {
                return false;
            }

            if (!rule.packetInfo.SourceIpAddress.Equals(IPAddress.Any) && !rule.packetInfo.SourceIpAddress.Equals(checkedPacketInfo.SourceIpAddress))
            {
                return false;
            }

            if (!rule.packetInfo.DestinationIpAddress.Equals(IPAddress.Any) && !rule.packetInfo.DestinationIpAddress.Equals(checkedPacketInfo.DestinationIpAddress))
            {
                return false;
            }

            if (rule.packetInfo.Protocol != Protocol.ANY && rule.packetInfo.Protocol != checkedPacketInfo.Protocol)
            {
                return false;
            }

            if (rule.packetInfo.Protocol == Protocol.ICMP && checkedPacketInfo.Protocol == Protocol.ICMP)
            {
                if (rule.packetInfo.IcmpType != ICMPType.ANY && rule.packetInfo.IcmpType != checkedPacketInfo.IcmpType)
                {
                    return false;
                }
            }
            else
            {
                //Check L4 Protocol - TCP, UDP
                if (rule.packetInfo.Protocol != Protocol.ANY && rule.packetInfo.Protocol != checkedPacketInfo.Protocol)
                {
                    return false;
                }

                if (rule.packetInfo.SourcePort != 0 && rule.packetInfo.SourcePort != checkedPacketInfo.SourcePort)
                {
                    return false;
                }

                if (rule.packetInfo.DestinationPort != 0 && rule.packetInfo.DestinationPort != checkedPacketInfo.DestinationPort)
                {
                    return false;
                }
            }

            // All criteria match
            return true;
        }


        private static PacketInfo getPacketInfo<T>(T packet)
        {
            PacketInfo packetInfo = new();
            Packet p;
            
            if (packet is RawCapture raw)
            {
                p = Packet.ParsePacket(raw.LinkLayerType, raw.Data);
            } 
            else if (packet is PacketForQueue pkQ)
            {
                p = pkQ.ethPacket;
            }
            else
            {
                throw new InvalidDataException("Invalid packet type");
            }

            Packet data = p;
            while (data != null)
            {
                switch (data)
                {
                    case EthernetPacket:
                        var ethPacket = data.Extract<EthernetPacket>(); ;
                        packetInfo.SourcePhysicalAddress = ethPacket.SourceHardwareAddress;
                        packetInfo.DestinationPhysicalAddress = ethPacket.DestinationHardwareAddress;
                        break;

                    case IPv4Packet:
                        var iPacket = data.Extract<IPv4Packet>();
                        packetInfo.SourceIpAddress = iPacket.SourceAddress;
                        packetInfo.DestinationIpAddress = iPacket.DestinationAddress;
                        break;

                    case TransportPacket:
                        var tuPacket = data.Extract<TransportPacket>();
                        packetInfo.SourcePort = tuPacket.SourcePort;
                        packetInfo.DestinationPort = tuPacket.DestinationPort;
                        if(data is TcpPacket) 
                            packetInfo.Protocol = Protocol.TCP; 
                        else if (data is UdpPacket) 
                            packetInfo.Protocol = Protocol.UDP;

                        break;

                    case IcmpV4Packet: //case IcmpV6Packet:
                        var icmpPacket = (IcmpV4Packet)data;
                        int icmpCode = icmpPacket.HeaderData[0];

                        packetInfo.Protocol = Protocol.ICMP;
                        if (Enum.IsDefined(typeof(ICMPType), icmpCode))
                            packetInfo.IcmpType = (ICMPType)icmpCode;
                        else
                            packetInfo.IcmpType = ICMPType.ANY;


                        break;

                }
                data = data.PayloadPacket;
            }

            return packetInfo;
        }

       
    }


    class ACLRule
    {
        //[Browsable(false)]
        public ACLRuleDirection AclRuleDirection { get; set; }
        public ACLAllowance Allowance { get; set; } = ACLAllowance.DENY;
        public ILiveDevice? device { get; set; } = null;

        public long noMatches { get; set; } = 0;


       

       [Browsable(false)] // To hide packetinfo property from dataGridView
        public PacketInfo packetInfo { get; set; }

        [Browsable(true)]
        public IPAddress SourceIpAddress
        {
            get { return packetInfo.SourceIpAddress; }
            set { packetInfo.SourceIpAddress = value; }
        }
        

        [Browsable(true)]
        public IPAddress DestinationIpAddress
        {
            get { return packetInfo.DestinationIpAddress; }
            set { packetInfo.DestinationIpAddress = value; }
        }

        [Browsable(true)]
        public PhysicalAddress SourcePhysicalAddress
        {
            get { return packetInfo.SourcePhysicalAddress; }
            set { packetInfo.SourcePhysicalAddress = value; }
        }

        [Browsable(true)]
        public PhysicalAddress DestinationPhysicalAddress
        {
            get { return packetInfo.DestinationPhysicalAddress; }
            set { packetInfo.DestinationPhysicalAddress = value; }
        }


        [Browsable(true)]
        public ushort SourcePort
        {
            get { return packetInfo.SourcePort; }
            set { packetInfo.SourcePort = value; }
        }

        [Browsable(true)]
        public ushort DestinationPort
        {
            get { return packetInfo.DestinationPort; }
            set { packetInfo.DestinationPort = value; }
        }

        [Browsable(true)]
        public Protocol Protocol
        {
            get { return packetInfo.Protocol; }
            set { packetInfo.Protocol = value; }
        }

        [Browsable(true)]
        public ICMPType IcmpType
        {
            get { return packetInfo.IcmpType; }
            set { packetInfo.IcmpType = value; }
        }


        public ACLRule()
        {

        }
        public ACLRule(ACLRuleDirection direction, PacketInfo packet)
        {
            this.AclRuleDirection = direction;
            this.packetInfo = packet;
        }

        public ACLRule(ACLRuleDirection aclRuleDirection, ACLAllowance allowance, ILiveDevice? device, PacketInfo packetInfo)
        {
            AclRuleDirection = aclRuleDirection;
            Allowance = allowance;
            this.device = device;
            this.packetInfo = packetInfo;
        }

        public override string ToString()
        {
            return "[ACL - Match] " + ToStringRuleDescription();
        }

        public string ToStringRuleDescription()
        {
            string AddressInfo = packetInfo.ToString();
            string direction = AclRuleDirection.ToString();
            string proto = (packetInfo.Protocol.Equals(Protocol.ANY)) ? "ANY" : packetInfo.Protocol.ToString();
            string allowance = Allowance.ToString();
            string dev = (device == null) ? "ANY" : device.Description;


            if (proto.Equals("ICMP"))
            {
                proto = proto + $" ({IcmpType})";
            }

            return string.Format($"[{allowance}] | {AddressInfo} [Protocol {proto}] | [{direction}] on [{dev}]");
        }
    }

    class PacketInfo
    {
        public IPAddress SourceIpAddress { get; set; } = IPAddress.Any;
        public IPAddress DestinationIpAddress { get; set; } = IPAddress.Any;

        public PhysicalAddress SourcePhysicalAddress { get; set; } = PhysicalAddress.None;
        public PhysicalAddress DestinationPhysicalAddress { get; set; } = PhysicalAddress.None;

       // [Browsable(false)]
        public ushort SourcePort { get; set; } = 0;
        public ushort DestinationPort { get; set; } = 0;

        public Protocol Protocol { get; set; } = Protocol.ANY;

        public ICMPType IcmpType { get; set; } = ICMPType.ANY;

        [Browsable(false)]
        public ILiveDevice? InDevice { get; set; }
        [Browsable(false)]
        public ILiveDevice? OutDevice { get; set; }

        public PacketInfo()
        {

        }
        public PacketInfo(PhysicalAddress sourcePhysicalAddress, PhysicalAddress destinationPhysicalAddress, IPAddress sourceIpAddress, IPAddress destinationIpAddress, ushort sourcePort, ushort destinationPort, Protocol protocol, ICMPType icmpType)
        {
            SourceIpAddress = sourceIpAddress;
            DestinationIpAddress = destinationIpAddress;
            SourcePhysicalAddress = sourcePhysicalAddress;
            DestinationPhysicalAddress = destinationPhysicalAddress;
            DestinationPort = destinationPort;
            SourcePort = sourcePort;
            Protocol = protocol;
            IcmpType = icmpType;
        }

        public override string ToString()
        {
            string srcMAC = (SourcePhysicalAddress.Equals(PhysicalAddress.None))
                ? "ANY Src. MAC"
                : EndDeviceRecord.FormatMAC(SourcePhysicalAddress);
            string dstMAC = (DestinationPhysicalAddress.Equals(PhysicalAddress.None))
                ? "ANY Dst. MAC"
                : EndDeviceRecord.FormatMAC(DestinationPhysicalAddress);
            string srcIp = (SourceIpAddress.Equals(IPAddress.Any) || SourceIpAddress.Equals(IPAddress.None)) ? "ANY Src. IP" : $"{SourceIpAddress}";
            string port = (DestinationPort == 0) ? "ANY" : DestinationPort.ToString();
            string dstIp = (DestinationIpAddress.Equals(IPAddress.Any) || DestinationIpAddress.Equals(IPAddress.None)) ? "ANY Dst. IP" : $"{DestinationIpAddress}";


            return string.Format(
                $"[{srcMAC}] -> [{dstMAC}] | [{srcIp}] -> [{dstIp}] [port {port}]");

        }
    }
}
