using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using PacketDotNet;
using SharpPcap;

namespace PSIP_SW_Switch
{
    public enum SysLogFacility : int
    {
        Kernel = 0,
        User = 1,
        Mail = 2,
        System = 3,
        Security = 4,
        Syslogd = 5,
        Printer = 6,
        News = 7,
        Uucp = 8,
        Clock = 9,
        Security2 = 10,
        Ftp = 11,
        Ntp = 12,
        Audit = 13,
        Alert = 14,
        Clock2 = 15,
        Local0 = 16,
        Local1 = 17,
        Local2 = 18,
        Local3 = 19,
        Local4 = 20,
        Local5 = 21,
        Local6 = 22,
        Local7 = 23,
    }

    public enum SysLogSeverity : int
    {
        Emergency = 0,
        Alert = 1,
        Critical = 2,
        Error = 3,
        Warning = 4,
        Notice = 5,
        Informational = 6,
        Debug = 7,
    }




    static class SysLog
    {
        public static bool Enabled = true;

        public static IPAddress ServerIpAddress = IPAddress.Parse("192.168.1.5");
        public static IPAddress ClientIpAddress = IPAddress.Parse("192.168.1.48");
        public static PhysicalAddress ServerPhysicalAddress = PhysicalAddress.Parse("FF:FF:FF:FF:FF:FF");

        public static ILiveDevice sender = InterfaceController.d1;

        public static ushort ClientPort = 514;
        public static ushort ServerPort = 514;


        static void InitSysLogClient(IPAddress srv, IPAddress client, ushort cPort, ushort sPort, ILiveDevice device)
        {
            ServerIpAddress = srv;
            ClientIpAddress = client;
            ClientPort = cPort;
            ServerPort = sPort;
            Enabled = true;
            sender = device;
        }



        public static void Log(SysLogSeverity Severity, string Message)
        {
            if(!Enabled)
                return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Message);
            Console.ResetColor();
            // TODO Add Packet to Queue
            DateTime timestamp = DateTime.Now;
            string msg = "[" + timestamp.ToString("yyyy/MM/dd HH:mm:ss") + "]" + Message;
            SysLogPacket sysLogPacket = new SysLogPacket(Severity, msg);

            PacketForQueue pkQ = new PacketForQueue(sender, sysLogPacket);

            sender.SendPacket(pkQ.ethPacket);

        }

    }

    public class SysLogPacket
    {
        private readonly UdpPacket _udpPacket;

        public SysLogPacket(SysLogSeverity severity, string message)
        {
            byte facility = 16; // LOCAL 01
            string priority = $"<{(byte)((facility << 3) | ((byte)severity))}>";

            byte[] dataBytes = Encoding.UTF8.GetBytes(priority).Concat(Encoding.UTF8.GetBytes(message)).ToArray();
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
                SysLog.sender.MacAddress,
                SysLog.ServerPhysicalAddress,
                                        EthernetType.IPv4) // TODO is it eth type ipv4??
            {
                PayloadPacket = ipPacket,
            };

            return ethernetPacket;
        }
    }
}
