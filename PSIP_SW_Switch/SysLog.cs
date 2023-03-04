using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using PacketDotNet;
using SharpPcap;

namespace PSIP_SW_Switch
{
    public enum SysLogSeverity {INFO = 6, NOTICE = 5, WARNING = 4, ALERT = 1, ERROR = 3}




    static class SysLog
    {
        public static bool Enabled = false;

        public static IPAddress ServerIpAddress = IPAddress.Parse("127.0.0.1");
        public static IPAddress ClientIpAddress = IPAddress.Parse("127.0.0.1");
        public static PhysicalAddress ClientPhysicalAddress = PhysicalAddress.Parse("FF:FF:FF:FF:FF:FF");
        public static PhysicalAddress ServerPhysicalAddress = PhysicalAddress.Parse("FF:FF:FF:FF:FF:FF");

        public static ILiveDevice sender;

        public static ushort ClientPort = 514;
        public static ushort ServerPort = 514;


        static void InitSysLogClient(IPAddress srv, IPAddress client, ushort cPort, ushort sPort, ILiveDevice device)
        {
            ServerIpAddress = srv;
            ClientIpAddress = client;
            ClientPort = cPort;
            ServerPort = sPort;
            ClientPhysicalAddress = device.MacAddress;
            Enabled = true;
        }



        public static void Log(SysLogSeverity Severity, string Message)
        {
            if(!Enabled)
                return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Message);
            Console.ResetColor();
            // TODO Add Packet to Queue
            SysLogPacket sysLogPacket = new SysLogPacket(Severity, Message);

            PacketForQueue pkQ = new PacketForQueue(sender, sysLogPacket);

        }

    }

    public class SysLogPacket
    {
        private readonly UdpPacket _udpPacket;

        public SysLogPacket(SysLogSeverity severity, string message)
        {
            byte facility = 16; // LOCAL 01
            byte sev = (byte)severity;
            byte header = (byte)((facility << 3) | sev);

            byte[] dataBytes = Enumerable.Repeat(header, 1).Concat(Encoding.ASCII.GetBytes(message)).ToArray();

            // Create the UDP packet
            _udpPacket = new UdpPacket(SysLog.ServerPort,SysLog.ClientPort)
            {
                PayloadData = dataBytes
            };
        }

        public Packet GetPacket()
        {
            // Create the IP packet
            var ipPacket = new IPv4Packet(
                SysLog.ClientIpAddress,
                SysLog.ServerIpAddress)
            {
                Protocol = ProtocolType.Udp,
                PayloadPacket = _udpPacket
            };

            // Create the Ethernet packet
            var ethernetPacket = new EthernetPacket(
                SysLog.ClientPhysicalAddress,
                SysLog.ServerPhysicalAddress,
                                        EthernetType.IPv4) // TODO is it eth type ipv4??
            {
                PayloadPacket = ipPacket,
            };

            return ethernetPacket;
        }
    }
}
