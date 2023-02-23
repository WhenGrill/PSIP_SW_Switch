using SharpPcap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDotNet;
using System.Net.Mail;

namespace PSIP_SW_Switch
{
    static class InterfaceController
    {
        static Form1 gui = Application.OpenForms.OfType<Form1>().FirstOrDefault();

        private static Queue<SharpPcap.RawCapture> QueueInt1 = new Queue<RawCapture>();
        private static Queue<SharpPcap.RawCapture> QueueInt2 = new Queue<RawCapture>();

        private static Queue<PacketForQueue> capturedQueue = new Queue<PacketForQueue>();
        private static object QueueLock = new object();

        private static object QueueInt1Lock = new object();
        private static object QueueInt2Lock = new object();

        private static ILiveDevice d1;
        private static ILiveDevice d2;

        static SharpPcap.CaptureDeviceList devices = SharpPcap.CaptureDeviceList.Instance;

        private static DataTable macAddressDataTable;
        private static List<DataRow> macAddressRows;
        private static List<DeviceInfo> macAddressTable;

        private static Dictionary<ILiveDevice, uint> devMap = new Dictionary<ILiveDevice, uint>();

        private static bool bckThreadInt1Stop = false;
        private static bool bckThreadInt2Stop = false;

        private static Thread injectionThreadInt1;
        private static Thread injectionThreadInt2;
        private static Thread captureThreadInt1;
        private static Thread captureThreadInt2;

        private static object macAddressDataTableLock = new object();
        private static object macAddressRowsLock = new object();
        private static object macAddressTableLock = new object();




        public static void GUIRefreshInterfaces()
        {
            gui.comboBoxInterfaceList1.Items.Clear();
            gui.comboBoxInterfaceList2.Items.Clear();

            devices.Refresh();

            foreach (var dev in devices)
            {
                gui.comboBoxInterfaceList1.Items.Add(dev.Description);
                gui.comboBoxInterfaceList2.Items.Add(dev.Description);
            }
        }

        public static void InterfaceCaptureStart()
        {
            InitMACAddressTable();
            d1 = devices[gui.comboBoxInterfaceList1.SelectedIndex];
            d2 = devices[gui.comboBoxInterfaceList2.SelectedIndex];



            //d1.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
            //d2.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);

            devMap[d1] = 1;
            devMap[d2] = 2;

            QueueInt1 = new Queue<RawCapture>();
            QueueInt2 = new Queue<RawCapture>();

            capturedQueue = new Queue<PacketForQueue>();

            try
            {
                bckThreadInt1Stop = false;
                //injectionThreadInt1 = new Thread(() => PacketSenderThread(ref QueueInt1, ref QueueInt1Lock, ref QueueInt2, ref QueueInt2Lock, ref bckThreadInt1Stop));
                injectionThreadInt1 = new Thread(() => PacketSenderThreadNEW(ref capturedQueue, ref QueueLock,ref bckThreadInt1Stop));
                //injectionThreadInt2 = new Thread(() => PacketSenderThread(d2, ref QueueInt2, ref QueueInt2Lock, ref bckThreadInt2Stop));

                //injectionThreadInt1.IsBackground = true;
                //injectionThreadInt2.IsBackground = true;

                //captureThreadInt1 = new Thread(() => captureThread(d1));
                //captureThreadInt2 = new Thread(() => captureThread(d2));

                //captureThreadInt1.IsBackground = true;
                //captureThreadInt2.IsBackground = true;

                injectionThreadInt1.Name = "Injection Interface 1";
                //injectionThreadInt2.Name = "Injection Interface 2";

                //captureThreadInt1.Name = "Capture Thread 1";
                //captureThreadInt2.Name = "Capture Thread 2";

                injectionThreadInt1.Start();
                //injectionThreadInt2.Start();

                //captureThreadInt1.Start();
                //captureThreadInt2.Start();
                StartNetworkInterfaceCapture(d1);
                StartNetworkInterfaceCapture(d2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error opening device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // TODO Catch excpetion to internal logger
                return;
            }

        }

        public static void InterfaceCaptureStop()
        {
            try
            {
                Console.WriteLine("Stopping Threads");
                bckThreadInt1Stop = true;
                //bckThreadInt2Stop = true;
                injectionThreadInt1.Join();
                //injectionThreadInt2.Join();

                d1.StopCapture();
                d2.StopCapture();

                //captureThreadInt1.Join();
                //captureThreadInt2.Join();
                Console.WriteLine("Done");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error closing network interface device", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Events
        /*
        private static void device_OnPacketArrival(object sender, PacketCapture e)
        {
            try
            {
                ILiveDevice device = (ILiveDevice)sender;
                var cap = e.GetPacket();

                if (device == d1)
                {
                    // Add Packet to queue
                    lock (QueueInt1Lock)
                    {
                        Console.WriteLine("[1] Enq...");
                        QueueInt1.Enqueue(cap);
                    }

                    // Update Statistics for Interface 1
                    Statistics.UpdateStatistics(1, cap, true);

                    //TODO Process MAC Address Table



                }
                else if (device == d2)
                {
                    // Add Packet to queue
                    lock (QueueInt2Lock)
                    {
                        Console.WriteLine("[2] Enquied");
                        QueueInt2.Enqueue(cap);
                    }

                    // Update Statistics for Interface 2
                    Statistics.UpdateStatistics(2, cap, true);

                    //TODO Process MAC Address Table
                }

                UpdateMACAddressTable(cap, device);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Cannot capture packet: " + ex.Message, "Error capturing packet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }*/

        private static void device_OnPacketArrivalNEW(object sender, PacketCapture e)
        {
            try
            {
                ILiveDevice device = (ILiveDevice)sender;
                RawCapture cap = e.GetPacket();
                PacketForQueue p;
                lock (QueueLock)
                {
                    //Console.WriteLine("[1] Enqued");
                    p = new PacketForQueue(device, cap);
                    capturedQueue.Enqueue(p);
                }
                Statistics.UpdateStatistics((device == d1) ? 1 : 2, p, true);
                UpdateMACAddressTable(cap, device);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Cannot capture packet: " + ex.Message, "Error capturing packet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        private static void StartNetworkInterfaceCapture(ILiveDevice device)
        {
            device.Open( DeviceModes.MaxResponsiveness | DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous);
            // device.OnPacketArrival += device_OnPacketArrival;
            device.OnPacketArrival += device_OnPacketArrivalNEW;
            device.StartCapture();
            //device.Capture(); // TODO Pri zmene siete - pripojeni WiFi - System.ArgumentOutOfRangeException: 'Specified argument was out of the range of valid values.'
            // Aj pri max responsivness randomne je to out of range wtf

        }


        #region MACAddressTable

        public static void ClearMACAddressTable()
        {
            InitMACAddressTable();
        }
        private static void UpdateMACAddressTable(RawCapture cap, ILiveDevice device)
        {
            if (cap.LinkLayerType != LinkLayers.Ethernet) // TODO co s tymto
                return;

            var eth = (PacketDotNet.EthernetPacket)cap.GetPacket();


            var srcMac = eth.SourceHardwareAddress.ToString();

            if (d1.MacAddress.Equals(eth.SourceHardwareAddress) || d2.MacAddress.Equals(eth.SourceHardwareAddress))
            {
                return;
            }

            if(isInTable(srcMac, devMap[device]))
                return;

            var dev = new DeviceInfo(srcMac, devMap[device], 1800);
            lock (macAddressTableLock)
            {
                macAddressTable.Add(dev);
            }

            UpdateGUIMACAddressTable(dev);
        }

        private static void UpdateGUIMACAddressTable(DeviceInfo dev)
        {
            DataRow row = macAddressDataTable.NewRow();

            row["MAC Address"] = DeviceInfo.FormatMAC(dev.MacAddress);
            row["Port"] = dev.Port;
            row["Timer"] = dev.Timer;

            lock (macAddressDataTableLock)
            {
                macAddressDataTable.Rows.Add(row);
            }

            if (gui.dataGridViewMACAddressTable.InvokeRequired)
            {
                gui.dataGridViewMACAddressTable.Invoke(new Action(() => { gui.dataGridViewMACAddressTable.Refresh(); })); // TODO System.NullReferenceException: 'Object reference not set to an instance of an object.'

            }
        }

        public static void InitMACAddressTable()
        {
            macAddressDataTable = new DataTable("macAddressTable");
            macAddressRows = new List<DataRow>();
            macAddressTable = new List<DeviceInfo>();
            
            DataColumn columnMAC = new DataColumn("MAC Address");
            DataColumn columnPort = new DataColumn("Port");
            DataColumn columnTimer = new DataColumn("Timer");


            //Add the Created Columns to the Datatable

            macAddressDataTable.Columns.Add(columnMAC);
            macAddressDataTable.Columns.Add(columnPort);
            macAddressDataTable.Columns.Add(columnTimer);

            gui.dataGridViewMACAddressTable.DataSource = macAddressDataTable;
        }

        public static bool isInTable(string mac, uint port)
        {
            foreach (var record in macAddressTable)
            {
                if (record.MacAddress == mac && record.Port == port)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion


        #region PacketProcessing
        /*
        private static void PacketSenderThread(ILiveDevice device, ref Queue<RawCapture> PacketQueueInt1, ref object QueueLockInt1, ref Queue<RawCapture> PacketQueueInt2, ref object QueueLockInt2, ref bool BackgroundThreadStop)
        {
            while (!BackgroundThreadStop)
                {
                    Queue<RawCapture> processQueueInt1 = null;
                    Queue<RawCapture> processQueueInt2 = null;

                lock (QueueLockInt1)
                    {
                        if (PacketQueueInt1.Count > 0)
                        {
                            processQueueInt1 = new Queue<RawCapture>(PacketQueueInt1);
                            PacketQueueInt1.Clear();
                        }
                    }

                lock (QueueLockInt2)
                {
                    if (PacketQueueInt2.Count > 0)
                    {
                        processQueueInt2 = new Queue<RawCapture>(PacketQueueInt2);
                        PacketQueueInt2.Clear();
                    }
                }

                    if (processQueueInt1 != null)
                    {
                        Console.WriteLine("{0}: ourQueue.Count is {1}", System.Threading.Thread.CurrentThread.Name, processQueueInt1.Count);

                        foreach (var packet in processQueueInt1)
                        {
                            Packet p = Packet.ParsePacket(packet.LinkLayerType, packet.Data);

                            if (typeof(NullPacket) == p.GetType())
                            {
                                Console.WriteLine("Null packet :O"); // TODO what to do??
                                continue;
                            }

                            var eth = (EthernetPacket)p;

                            int intNum = (device == d1) ? 1 : 2;

                            try
                            {
                          //  if (isInTable(eth.DestinationHardwareAddress.ToString(),
                           //          (uint)intNum)) // Check if Dest MAC address is in table
                           // {
                           //     device.SendPacket(p);
                           //     Statistics.UpdateStatistics(intNum, packet, false);
                           // }
                           // else
                            //{
                            d2.SendPacket(eth); // TODO SharpPcap.PcapException: 'Can't send packet: send error: PacketSendPacket failed: The I/O operation failed because network media is disconnected or wireless access point is out of range.  
                            //d1.SendPacket(eth);
                            //Statistics.UpdateStatistics(1, packet, false);
                            Statistics.UpdateStatistics(2, packet, false);
                            //   }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "General Packet Send Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                if (processQueueInt2 != null)
                {
                    Console.WriteLine("{0}: ourQueue.Count is {1}", System.Threading.Thread.CurrentThread.Name, processQueueInt2.Count);

                    foreach (var packet in processQueueInt2)
                    {
                        Packet p = Packet.ParsePacket(packet.LinkLayerType, packet.Data);

                        if (typeof(NullPacket) == p.GetType())
                        {
                            Console.WriteLine("Null packet :O"); // TODO what to do??
                            continue;
                        }

                        var eth = (EthernetPacket)p;

                        int intNum = (device == d1) ? 1 : 2;

                        try
                        {
                           // if (isInTable(eth.DestinationHardwareAddress.ToString(),
                           //          (uint)intNum)) // Check if Dest MAC address is in table
                           // {
                           //     device.SendPacket(p);
                           //     Statistics.UpdateStatistics(intNum, packet, false);
                           // }
                           // else
                           // {
                                //d2.SendPacket(eth); // TODO SharpPcap.PcapException: 'Can't send packet: send error: PacketSendPacket failed: The I/O operation failed because network media is disconnected or wireless access point is out of range.  
                                d1.SendPacket(eth);
                                Statistics.UpdateStatistics(1, packet, false);
                                //Statistics.UpdateStatistics(2, packet, false);
                           // }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "General Packet Send Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }


                if (!BackgroundThreadStop)
                    {
                        Console.WriteLine("Sleeping...");
                        System.Threading.Thread.Sleep(100);
                    }
                }

                /*while (!BackgroundThreadStop)
                {
                    bool shouldSleep = true;
    
                    lock (QueueLock)
                    {
                        if (PacketQueue.Count != 0)
                        {
                            shouldSleep = false;
                        }
                    }
    
                    if (shouldSleep)
                    {
                        Console.WriteLine("Sleeping...");
                        System.Threading.Thread.Sleep(250);
                    }
                    else // should process the queue
                    {
                        Queue<RawCapture> ourQueue = null;
                        lock (QueueLock)
                        {
                            // swap queues, giving the capture callback a new one
                            ourQueue = new Queue<RawCapture>(PacketQueue);
                            PacketQueue.Clear();  //  = new Queue<RawCapture>();
                        }
    
                        Console.WriteLine("{0}: ourQueue.Count is {1}", System.Threading.Thread.CurrentThread.Name, ourQueue.Count);
    
                        foreach (var packet in ourQueue)
                        {
                            // TODO System.InvalidCastException: 'Unable to cast object of type 'PacketDotNet.NullPacket' to type 'PacketDotNet.EthernetPacket'.'
    
                            Packet p = Packet.ParsePacket(packet.LinkLayerType, packet.Data);
    
                            if (typeof(NullPacket) == p.GetType())
                            {
                                Console.WriteLine("Null packet :O"); // TODO what to do??
                                continue;
                            }
    
                            var eth = (EthernetPacket)p;
    
                            int intNum = (device == d1) ? 1 : 2;
    
                            if (isInTable(eth.DestinationHardwareAddress.ToString(), (uint)intNum)) // Check if Dest MAC address is in table
                            {
                                device.SendPacket(p);
                                Statistics.UpdateStatistics(intNum, packet, false);
                            }
                            else
                            {
                                d1.SendPacket(p);
                                d2.SendPacket(p); // TODO SharpPcap.PcapException: 'Can't send packet: send error: PacketSendPacket failed: The I/O operation failed because network media is disconnected or wireless access point is out of range.  
                                Statistics.UpdateStatistics(1, packet, false);
                                Statistics.UpdateStatistics(2, packet, false);
                            }
    
                        }
                    }
                }

        }*/


        private static void PacketSenderThreadNEW(ref Queue<PacketForQueue> PacketQueue, ref object QueueLock, ref bool BackgroundThreadStop)
        {
            while (!BackgroundThreadStop)
            {
                Queue<PacketForQueue> processQueue = null;

                lock (QueueLock)
                {
                    if (PacketQueue.Count > 0)
                    {
                        processQueue = new Queue<PacketForQueue>(PacketQueue);
                        PacketQueue.Clear();
                    }
                }

                if (processQueue != null)
                {
                    //Console.WriteLine("{0}: ourQueue.Count is {1}", System.Threading.Thread.CurrentThread.Name, processQueue.Count);

                    foreach (var qPacket in processQueue)
                    {
                        try
                        {
                            ILiveDevice senderDevice = (qPacket.device == d1) ? d2 : ((qPacket.device == d2) ? d1 : null);
                            if (senderDevice == null)
                            {
                                throw new Exception("Invalid device");
                            }
                            senderDevice.SendPacket(qPacket.ethPacket);
                            Statistics.UpdateStatistics( (senderDevice == d1) ? 1 : 2 , qPacket, false);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "General Packet Send Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (!BackgroundThreadStop)
                {
                    //Console.WriteLine("Sleeping...");
                    System.Threading.Thread.Sleep(10);
                }
            }
        }
        #endregion

    }

    public struct PacketForQueue
    {
        public ILiveDevice device { get; set; }
        public EthernetPacket ethPacket { get; set; }

        public PacketForQueue(ILiveDevice device, RawCapture capture)
        {
            this.device = device;
            this.ethPacket = (EthernetPacket)Packet.ParsePacket(capture.LinkLayerType, capture.Data);
        }

    }
    public class DeviceInfo
    {
        public string MacAddress { get; set; }
        public uint Port { get; set; }
        public uint Timer { get; set; }

        public DeviceInfo(string macAddress, uint port, uint timer)
        {
            MacAddress = macAddress;
            Port = port;
            Timer = timer;
        }

        public static string FormatMAC(string mac)
        {
            string macAddress = mac.ToUpper();
            string formattedMacAddress = "";
            for (int i = 0; i < macAddress.Length; i += 2)
            {
                formattedMacAddress += macAddress.Substring(i, 2);
                if (i < macAddress.Length - 2)
                {
                    formattedMacAddress += ":";
                }
            }
            return formattedMacAddress;
        }
    }
}
