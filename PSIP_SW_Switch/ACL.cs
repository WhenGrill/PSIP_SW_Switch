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

    enum ACLRuleType
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

        public static bool Enabled = true;
        public static object AclRulesListLock = new();

        public static void InitACL()
        {
            //GUIController.AclGui.InitACLDataGridview();
        }
        public static void EnableACL()
        {
            Enabled = true;
        }

        public static void DisableACL()
        {
            Enabled = false;
        }

        public static bool IsAllowed<T>(T packet, ILiveDevice device = null)
        {
            if (AclRules.Count == 0 || Enabled == false)
            {
                //SysLog.Log(SysLogSeverity.Warning, "[ACL] Packet permitted by default");
                return true;
            }

            PacketInfo checkedPacket = getPacketInfo(packet);

            foreach (var rule in AclRules)
            {
                if (IsMatch(rule, checkedPacket, device))
                {
                    if (rule.Allowance == ACLAllowance.ALLOW)
                    {
                        SysLog.Log(SysLogSeverity.Informational, rule.ToString());
                        return true;
                    }
                    
                    SysLog.Log(SysLogSeverity.Warning, rule.ToString());
                    return false;
                    
                }
            }

            //ACLRule defDeny = new ACLRule(ACLRuleType.INBOUND, checkedPacket, device);
            //SysLog.Log(SysLogSeverity.Warning, "[ACL] Packet denied by default");
            return false; // default deny
        }

        private static bool IsMatch(ACLRule rule, PacketInfo checkedPacketInfo, ILiveDevice? device)
        {
            // Check if the rule applies to the specified device
            if (device != null && rule.device != null && device != rule.device)
            {
                return false;
            }

            // Check if the rule applies to the correct direction
            if (device != null && rule.AclRuleType != ACLRuleType.INBOUND)
            {
                return false;
            }
            if (device == null && rule.AclRuleType != ACLRuleType.OUTBOUND)
            {
                return false;
            }

            //Check explicitly ICMP because there are no ports
            bool wasICMP = false;

            if (rule.packetInfo.Protocol == Protocol.ICMP && checkedPacketInfo.Protocol != Protocol.ICMP)
            {
                return false;
            }

            if (rule.packetInfo.Protocol == Protocol.ICMP && checkedPacketInfo.Protocol == Protocol.ICMP)
            {
                wasICMP = true;
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

            if(!wasICMP)
            {
                if (rule.packetInfo.SourcePort != 0 && rule.packetInfo.SourcePort != checkedPacketInfo.SourcePort)
                {
                    return false;
                }

                if (rule.packetInfo.DestinationPort != 0 && rule.packetInfo.DestinationPort != checkedPacketInfo.DestinationPort)
                {
                    return false;
                }

                //Check L4 Protocol - TCP, UDP
                if (rule.packetInfo.Protocol != Protocol.ANY && rule.packetInfo.Protocol != checkedPacketInfo.Protocol)
                {
                    return false;
                }
            }
            else // ICMP
            {
                if (rule.packetInfo.IcmpType != ICMPType.ANY && rule.packetInfo.IcmpType != checkedPacketInfo.IcmpType)
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

            Packet data = p.PayloadPacket;
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
                        if (Enum.IsDefined(typeof(IcmpV4Packet), icmpCode))
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
        public ACLRuleType AclRuleType { get; set; }
        public ACLAllowance Allowance { get; set; } = ACLAllowance.DENY;
        public ILiveDevice? device { get; set; }

        [Browsable(false)] // To hide packetinfo property from dataGridView
        public PacketInfo packetInfo { get; set; }

        public ACLRule()
        {

        }
        public ACLRule(ACLRuleType type, PacketInfo packet, ILiveDevice? device)
        {
            this.AclRuleType = type;
            this.packetInfo = packet;
            this.device = device;
        }

        public override string ToString()
        {
            
            return string.Format("[ACL] [{1}] | [{2}] -> [{3}] && [{4}:{5}] -> [{6}:{7}] [Protocol {8}] | [{9}] on [{10}]",
                null,
                AclRuleType,
                (packetInfo.SourcePhysicalAddress == null) ? "ANY" : EndDeviceInfo.FormatMAC(packetInfo.SourcePhysicalAddress),
                (packetInfo.DestinationPhysicalAddress == null) ? "ANY" : EndDeviceInfo.FormatMAC(packetInfo.DestinationPhysicalAddress),
                (packetInfo.SourceIpAddress == null) ? "ANY" : packetInfo.SourcePhysicalAddress.ToString(),
                (packetInfo.SourcePort == 0) ? "ANY" : packetInfo.SourcePort,
                (packetInfo.DestinationIpAddress == null) ? "ANY" : packetInfo.DestinationIpAddress.ToString(),
                (packetInfo.DestinationPort == 0) ? "ANY" : packetInfo.DestinationPort,
                (packetInfo.Protocol == null) ? "NONE" : packetInfo.Protocol,
                Allowance,
                device.Description);
        }
    }

    class PacketInfo
    {
        public IPAddress SourceIpAddress { get; set; } = IPAddress.Any;
        public IPAddress DestinationIpAddress { get; set; } = IPAddress.Any;

        public PhysicalAddress SourcePhysicalAddress { get; set; } = PhysicalAddress.None;
        public PhysicalAddress DestinationPhysicalAddress { get; set; } = PhysicalAddress.None;

        public int SourcePort { get; set; } = 0;
        public int DestinationPort { get; set; } = 0;

        public Protocol Protocol { get; set; } = Protocol.ANY;

        public ICMPType IcmpType { get; set; } = ICMPType.ANY;

        public PacketInfo()
        {

        }

        public override string ToString()
        {
            string ip = (SourceIpAddress == null) ? "" : $"[{SourceIpAddress}:{SourcePort}] -> [{DestinationIpAddress}:{DestinationPort}]";
            return string.Format(
                $"[{EndDeviceInfo.FormatMAC(SourcePhysicalAddress)}] -> [{EndDeviceInfo.FormatMAC(DestinationPhysicalAddress)}] " + ip);

        }
    }
}
