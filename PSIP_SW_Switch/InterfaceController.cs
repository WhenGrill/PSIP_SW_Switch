using SharpPcap;
using System.Data;
using System.Net.NetworkInformation;
using PacketDotNet;
using System.Net.NetworkInformation;

namespace PSIP_SW_Switch
{
    static class InterfaceController
    {
        public static MainWindow? gui;

        public static Queue<PacketForQueue>? CapturedQueue;
        public static object? CapturedQueueLock;

        public static ILiveDevice? d1;
        public static ILiveDevice? d2;

        static CaptureDeviceList _devices = CaptureDeviceList.Instance;
        private static List<ILiveDevice>? _openedDevices;



        public static Dictionary<ILiveDevice, int>? deviceMap;

        private static bool _networkInterfaceSenderThreadStop;
        private static Thread? _networkInterfaceSenderThread;


        public static void InitInterfaceController()
        {
            gui = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();
            if (gui == null)
            {
                MessageBox.Show("Cannot get GUI Instance", "GUI Instance Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }
            GUIRefreshInterfaces();
            MACAddressTable.InitMACAddressTable();
        }

        public static void GUIRefreshInterfaces()
        {
            gui?.comboBoxInterfaceList1.Items.Clear();
            gui?.comboBoxInterfaceList2.Items.Clear();
            // TODO clear combobox text after refresh interface
            // TODO sort them !

            _devices.Refresh();

            foreach (var dev in _devices)
            {
                gui?.comboBoxInterfaceList1.Items.Add(dev.Description);
                gui?.comboBoxInterfaceList2.Items.Add(dev.Description);
            }
        }

        private static void InitNetworkInterfaceSenderThread()
        {
            _networkInterfaceSenderThreadStop = false;
            CapturedQueueLock = new();
            CapturedQueue = new Queue<PacketForQueue>();

            _networkInterfaceSenderThread = new Thread(
                    () => Thread_NetworkInterfaceSender(ref CapturedQueue, ref CapturedQueueLock, ref _networkInterfaceSenderThreadStop))
                { Name = "Packet Sender Thread" };
            try
            {
                _networkInterfaceSenderThread.Start();
            }
            catch (Exception? ex)
            {
                MessageBox.Show("Failed to start thread for sending packets", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        public static void InterfaceCaptureStart()
        {
            d1 = _devices[gui.comboBoxInterfaceList1.SelectedIndex];
            d2 = _devices[gui.comboBoxInterfaceList2.SelectedIndex];

            deviceMap = new();

            deviceMap[d1] = 1;
            deviceMap[d2] = 2;

            try
            {
                OpenNetworkInterfaces();
                InitNetworkInterfaceSenderThread();
                StartNetworkInterfaceCapture(d1);
                StartNetworkInterfaceCapture(d2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error starting Switch. Please restart application", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void InterfaceCaptureStop()
        {
            try
            {
                _networkInterfaceSenderThreadStop = true;
                _networkInterfaceSenderThread.Join();
                d1.StopCapture();
                d2.StopCapture();

                CloseNetworkInterfaces();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Events
        private static void device_OnPacketArrival(object sender, PacketCapture e)
        {
            PacketForQueue p;
            try
            {
                ILiveDevice device = (ILiveDevice)sender;
                RawCapture cap = e.GetPacket();

                if (ACL.Enabled  && !ACL.IsAllowed(cap, device)) // ACL Enabled and if not Allowed
                {
                    return;
                }


                lock (CapturedQueueLock)
                {
                    p = new PacketForQueue(device, cap);
                    CapturedQueue.Enqueue(p);
                }
                Statistics.UpdateStatistics((device == d1) ? 1 : 2, p, true);
                //MACAddressTable.UpdateMACAddressTable(p); // TODO Collection modified
            }

            catch (Exception ex)
            {
                MessageBox.Show("Cannot capture packet: " + ex.Message + "\n\n" + "Object:\n\n" + ex.Data + "\n\nSTACK TRACE:\n\n" + ex.StackTrace, "Error capturing packet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private static void OpenNetworkInterfaces()
        {
            _openedDevices = new(_devices);

            foreach (var dev in _devices)
            {
                try
                {
                    dev.Open(DeviceModes.MaxResponsiveness | DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous);
                }
                catch
                {
                    _openedDevices.Remove(dev);
                }
            }
        }

        private static void CloseNetworkInterfaces()
        {
            List<ILiveDevice> notCloseDevices = new();
            foreach (var dev in _openedDevices)
            {
                try
                {
                    dev.Close();
                }
                catch
                {
                    notCloseDevices.Add(dev);
                }
            }

            if (notCloseDevices.Count != 0)
            {
                string interfaces = "";
                foreach (var dev in notCloseDevices)
                {
                    interfaces += "\t - " + dev.Description + "\n";
                }

                MessageBox.Show("Some interfaces failed to close:" + interfaces, "Failed to close interface",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private static void StartNetworkInterfaceCapture(ILiveDevice device)
        {
            //device.Open( DeviceModes.MaxResponsiveness | DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous);
            device.OnPacketArrival += device_OnPacketArrival;
            device.StartCapture();
        }


        


        #region PacketProcessing

        private static void RemoveNonFunctioningDevices(List<ILiveDevice> nonFunctioningDevices)
        {
            foreach (var dev in nonFunctioningDevices)
            {
                dev.Close();
                nonFunctioningDevices.Remove(dev);
            }
        }
        
        private static void Thread_NetworkInterfaceSender(ref Queue<PacketForQueue> PacketQueue, ref object QueueLock, ref bool BackgroundThreadStop)
        {
            Console.WriteLine("Started");
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
                    foreach (var qPacket in processQueue)
                    {
                        try
                        {
                            if (ACL.Enabled && !ACL.IsAllowed(qPacket))
                            {
                                continue;
                            }


                            ILiveDevice senderDevice = (qPacket.device == d1) ? d2 : ((qPacket.device == d2) ? d1 : null);
                            if (senderDevice == null)
                            {
                                throw new Exception("Invalid device");
                            }

                            if (MACAddressTable.isInTable(qPacket))
                            {
                                senderDevice.SendPacket(qPacket.ethPacket);
                            }
                            else
                            {
                                foreach (var dev in _openedDevices.ToList())
                                {
                                    if (qPacket.device == dev)
                                        continue;

                                    try
                                    {
                                        dev.SendPacket(qPacket.ethPacket);
                                    }
                                    catch
                                    {
                                        dev.Close();
                                        _openedDevices.Remove(dev);
                                    }
                                }
                               
                            }

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
                    Thread.Sleep(10);
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

        public PacketForQueue(ILiveDevice device, SysLogPacket packet)
        {
            this.device = device;
            this.ethPacket = (EthernetPacket)packet.GetPacket(); // TODO check if Syslog wont make problems here
        }

    }

    public class EndDeviceInfo
    {
        public PhysicalAddress MacAddress { get; set; }
        public int Port { get; set; }
        public int Timer { get; set; }

        public EndDeviceInfo(PacketForQueue packet, int port,  int timer)
        {
            MacAddress = packet.ethPacket.SourceHardwareAddress;
            Port = port;
            Timer = timer;
        }

        public static string FormatMAC<T>(T mac)
        {
            string macAddress;

            if (mac is PhysicalAddress m)
            {
                macAddress = m.ToString();
            } else if (mac is string s)
            {
                if (s.Length != 12)
                {
                    throw new InvalidDataException("Invalid type of MAC Address");
                }
                macAddress = s;
            }
            else
            {
                throw new InvalidDataException("Invalid type of MAC Address");
            }

            macAddress = macAddress.ToUpper();
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
