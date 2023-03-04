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

    enum Protocol
    {
        TCP,
        UDP,
        ICMP,
        ANY
    }

    static class ACL
    {
        public static BindingList<ACLRule> AclRules = new();

        public static bool Enabled = false;
        public static object AclRulesListLock = new();

        public static void InitACL()
        {
            GUIController.AclGui.InitACLDataGridview();
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
                // SysLog.Log(SysLogSeverity.WARNING, "[ACL] Packet permitted by default");
                return true;
            }

            PacketInfo checkedPacket = getPacketInfo(packet);

            foreach (var rule in AclRules)
            {
                if (IsMatch(rule, checkedPacket, device))
                {
                    if (rule.Allowance == ACLAllowance.ALLOW)
                    {
                        SysLog.Log(SysLogSeverity.INFO, rule.ToString());
                        return true;
                    }
                    
                    SysLog.Log(SysLogSeverity.WARNING, rule.ToString());
                    return false;
                    
                }
            }

            //ACLRule defDeny = new ACLRule(ACLRuleType.INBOUND, checkedPacket, device);
            SysLog.Log(SysLogSeverity.WARNING, "[ACL] Packet denied by default");
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
            if (rule.packetInfo.SourcePhysicalAddress != null && !rule.packetInfo.SourcePhysicalAddress.Equals(checkedPacketInfo.SourcePhysicalAddress))
            {
                return false;
            }
            if (rule.packetInfo.DestinationPhysicalAddress != null && !rule.packetInfo.DestinationPhysicalAddress.Equals(checkedPacketInfo.DestinationPhysicalAddress))
            {
                return false;
            }

            if (rule.packetInfo.SourceIpAddress != null && !rule.packetInfo.SourceIpAddress.Equals(checkedPacketInfo.SourceIpAddress))
            {
                return false;
            }

            if (rule.packetInfo.DestinationIpAddress != null && !rule.packetInfo.DestinationIpAddress.Equals(checkedPacketInfo.DestinationIpAddress))
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
                if (rule.packetInfo.Protocol != null && rule.packetInfo.Protocol != checkedPacketInfo.Protocol)
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

            while (p != null)
            {
                Packet data = p.PayloadPacket;
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

                    case IcmpV4Packet:
                    case IcmpV6Packet:
                        packetInfo.Protocol = Protocol.ICMP;
                        break;

                }
                p = data.PayloadPacket;
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

        public ACLRule(ACLRuleType type, PacketInfo packet, ILiveDevice? device)
        {
            this.AclRuleType = type;
            this.packetInfo = packet;
            this.device = device;
        }

        public override string ToString()
        {
            DateTime timestamp = DateTime.Now;
            return string.Format("[{0}] [ACL] [{1}] | [{2}] -> [{3}] && [{4}:{5}] -> [{6}:{7}] [Protocol {8}] | [{9}] on [{10}]",
                timestamp.ToString("yyyy/MM/dd HH:mm:ss"),
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
        public IPAddress? SourceIpAddress { get; set; }
        public IPAddress? DestinationIpAddress { get; set; }

        public PhysicalAddress? SourcePhysicalAddress { get; set; }
        public PhysicalAddress? DestinationPhysicalAddress { get; set; }

        public int? SourcePort { get; set; }
        public int? DestinationPort { get; set; }

        public Protocol? Protocol { get; set; }

        public override string ToString()
        {
            string ip = (SourceIpAddress == null) ? "" : $"[{SourceIpAddress}:{SourcePort}] -> [{DestinationIpAddress}:{DestinationPort}]";
            return string.Format(
                $"[{EndDeviceInfo.FormatMAC(SourcePhysicalAddress)}] -> [{EndDeviceInfo.FormatMAC(DestinationPhysicalAddress)}] " + ip);

        }
    }
}
