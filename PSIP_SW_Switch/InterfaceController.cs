using System.Collections.Specialized;
using System.ComponentModel;
using SharpPcap;
using System.Data;
using System.Net.NetworkInformation;
using PacketDotNet;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Xml;

namespace PSIP_SW_Switch
{
    static class InterfaceController
    {
        public static MainWindow? gui;

        public static Queue<PacketForQueue>? CapturedQueue;
        public static object? CapturedQueueLock;

        public static ILiveDevice? d1;
        public static ILiveDevice? d2;

        public static bool d1CableDisconnected = true;
        public static bool d2CableDisconnected = true;

        public static DateTime d1LastPacketArrival = DateTime.UtcNow;
        public static DateTime d2LastPacketArrival = DateTime.UtcNow;

        public static object PacketArrivalLock = new ();

        static CaptureDeviceList _devices = CaptureDeviceList.Instance;
        private static List<ILiveDevice>? _openedDevices;


        public static Dictionary<ILiveDevice, int>? deviceMap;

        private static bool _networkInterfaceSenderThreadStop;
        private static Thread? _networkInterfaceSenderThread;

        private static bool Started = false;


        public static void Init()
        {
            gui = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();
            if (gui == null)
            {
                MessageBox.Show("Cannot get GUI Instance", "GUI Instance Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }
            GUIRefreshInterfaces();
        }

        public static void GUIRefreshInterfaces()
        {
            gui?.comboBoxInterfaceList1.Items.Clear();
            gui?.comboBoxInterfaceList2.Items.Clear();

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
                Started = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error starting Switch. Please restart application", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void InterfaceCaptureStop()
        {
            if (!Started)
                return;

            try
            {
                _networkInterfaceSenderThreadStop = true;
                _networkInterfaceSenderThread.Join();
                d1.StopCapture();
                d2.StopCapture();

                CloseNetworkInterfaces();
                Started = false;
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

                if(device == d1)
                    lock(PacketArrivalLock)
                        d1LastPacketArrival = DateTime.UtcNow;
                else if(device == d2)
                    lock(PacketArrivalLock)
                        d2LastPacketArrival = DateTime.UtcNow;


                RawCapture cap = e.GetPacket();

                if (ACL.Enabled  && !ACL.IsAllowed(cap, ACLRuleDirection.INBOUND, device)) // ACL Enabled and if not Allowed
                {
                    return;
                }


                lock (CapturedQueueLock)
                {
                    p = new PacketForQueue(device, cap);
                    CapturedQueue.Enqueue(p);
                }
                Statistics.UpdateStatistics((device == d1) ? 1 : 2, p, true);

                MACAddressTable.AddRecord(p);
                
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
                    dev.Open(DeviceModes.MaxResponsiveness | DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous); // DeviceModes.NoCaptureRemote);
                    // TODO hash tabulka ak sa zacykli komunikaica. nepreposielma to co som uz spracovala lebo nezachytavam uz to co som poslal resp. zachytil.
                    // TODO alebo mode iba IN.
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

        public static void CloseNetworkInterface(ILiveDevice dev)
        {
            dev.Close();
        }

        public static void OpenNetworkInterface(ILiveDevice dev)
        {
            try
            {
                dev.Open(DeviceModes.MaxResponsiveness | DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous);
                //dev.Open(DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous);
            }
            catch
            {
                throw new IOException($"Unable to open main interface {dev.Description}");
            }
        }

        public static void StartNetworkInterfaceCapture(ILiveDevice device)
        {
            //device.Open( DeviceModes.MaxResponsiveness | DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous);
            device.OnPacketArrival += device_OnPacketArrival;
            device.StartCapture();

            if(!Started)
                device.Close();
        }


        


        #region PacketProcessing

        private static void RemoveNonFunctioningDevices(List<ILiveDevice> nonFunctioningDevices)
        {
            foreach (var dev in nonFunctioningDevices)
            {
                dev.Close();
                _openedDevices.Remove(dev);
            }
        }
        
        private static void Thread_NetworkInterfaceSender(ref Queue<PacketForQueue> PacketQueue, ref object QueueLock, ref bool BackgroundThreadStop)
        {
            //Console.WriteLine("Started");
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
                            ILiveDevice senderDevice = (qPacket.device == d1) ? d2 : ((qPacket.device == d2) ? d1 : null);


                            if (senderDevice == null)
                            {
                                throw new Exception("Invalid device");
                            }
                            
                            if((senderDevice == d1 && d1CableDisconnected) || (senderDevice == d2 && d2CableDisconnected))
                                    break; // TODO Break alebo continue?
                            

                            if (ACL.Enabled && !ACL.IsAllowed(qPacket, ACLRuleDirection.OUTBOUND, senderDevice))
                            {
                                continue;
                            }

                            bool inMacTable = false;



                            lock (MACAddressTable.MacAddressTableLock)
                            {
                                inMacTable = MACAddressTable.IsInTable(qPacket, addRecord: false);
                            }

                            if (inMacTable)
                            {
                                //Console.WriteLine(
                                //    $"[MAC] {EndDeviceRecord.FormatMAC(qPacket.ethPacket.DestinationHardwareAddress)} in table, sending by port {deviceMap[senderDevice]}");
                                senderDevice.SendPacket(qPacket.ethPacket);
                            }
                            else
                            {
                                //Console.WriteLine($"[MAC] {EndDeviceRecord.FormatMAC(qPacket.ethPacket.DestinationHardwareAddress)} NOT IN TABLE, sending by ALL");

                                List<ILiveDevice> nonFunctioningDevices = new();
                                foreach (var dev in _openedDevices)
                                {
                                    if (qPacket.device == dev)
                                        continue;

                                    try
                                    {
                                        dev.SendPacket(qPacket.ethPacket);
                                    }
                                    catch
                                    {
                                        if(dev != d1 || dev != d2)
                                            nonFunctioningDevices.Add(dev);
                                    }
                                }

                                RemoveNonFunctioningDevices(nonFunctioningDevices);
                            }

                            Statistics.UpdateStatistics(deviceMap[senderDevice], qPacket, false);

                        }
                        catch (Exception ex)
                        {
                            continue;
                            // MessageBox.Show(ex.Message, "General Packet Send Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.ethPacket = (EthernetPacket)packet.GetPacket();
        }

    }
}
