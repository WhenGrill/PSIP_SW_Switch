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
        private static MainWindow? _gui;

        private static Queue<PacketForQueue>? _capturedQueue;
        private static object? _capturedQueueLock;

        private static ILiveDevice? _d1;
        private static ILiveDevice? _d2;

        static CaptureDeviceList _devices = CaptureDeviceList.Instance;

        private static DataTable? _macAddressDataTable;
        private static List<DataRow>? _macAddressRows;
        private static List<EndDeviceInfo>? _macAddressTable;

        private static Dictionary<ILiveDevice, uint>? _deviceMap;

        private static bool _networkInterfaceSenderThreadStop;
        private static Thread? _networkInterfaceSenderThread;

        private static object? _macAddressDataTableLock;
        private static object? _macAddressRowsLock;
        private static object? _macAddressTableLock;


        public static void InitInterfaceController()
        {
            _gui = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();
            if (_gui == null)
            {
                MessageBox.Show("Cannot get GUI Instance", "GUI Instance Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }
            GUIRefreshInterfaces();
            InitMACAddressTable();
        }

        public static void GUIRefreshInterfaces()
        {
            _gui?.comboBoxInterfaceList1.Items.Clear();
            _gui?.comboBoxInterfaceList2.Items.Clear();
            // TODO clear combobox text after refresh interface
            // TODO sort them !

            _devices.Refresh();

            foreach (var dev in _devices)
            {
                _gui?.comboBoxInterfaceList1.Items.Add(dev.Description);
                _gui?.comboBoxInterfaceList2.Items.Add(dev.Description);
            }
        }

        private static void InitNetworkInterfaceSenderThread()
        {
            _networkInterfaceSenderThreadStop = false;
            _capturedQueueLock = new();
            _capturedQueue = new Queue<PacketForQueue>();

            _networkInterfaceSenderThread = new Thread(
                    () => Thread_NetworkInterfaceSender(ref _capturedQueue, ref _capturedQueueLock, ref _networkInterfaceSenderThreadStop))
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
            _d1 = _devices[_gui.comboBoxInterfaceList1.SelectedIndex];
            _d2 = _devices[_gui.comboBoxInterfaceList2.SelectedIndex];

            _deviceMap = new();

            _deviceMap[_d1] = 1;
            _deviceMap[_d2] = 2;

            try
            {
                InitNetworkInterfaceSenderThread();
                StartNetworkInterfaceCapture(_d1);
                StartNetworkInterfaceCapture(_d2);
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
                _d1.StopCapture();
                _d2.StopCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error closing network interface device", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Events
        private static void device_OnPacketArrival(object sender, PacketCapture e)
        {
            try
            {
                ILiveDevice device = (ILiveDevice)sender;
                RawCapture cap = e.GetPacket();
                PacketForQueue p;
                lock (_capturedQueueLock)
                {
                    p = new PacketForQueue(device, cap);
                    _capturedQueue.Enqueue(p);
                }
                Statistics.UpdateStatistics((device == _d1) ? 1 : 2, p, true);
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
            device.OnPacketArrival += device_OnPacketArrival;
            device.StartCapture();
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

            var eth = (EthernetPacket)cap.GetPacket();


            var srcMac = eth.SourceHardwareAddress.ToString();

            if (_d1.MacAddress.Equals(eth.SourceHardwareAddress) || _d2.MacAddress.Equals(eth.SourceHardwareAddress))
            {
                return;
            }

            if(isInTable(srcMac, _deviceMap[device]))
                return;

            var dev = new EndDeviceInfo(srcMac, _deviceMap[device], 1800);
            lock (_macAddressTableLock)
            {
                _macAddressTable.Add(dev);
            }

            UpdateGUIMACAddressTable(dev);
        }

        private static void UpdateGUIMACAddressTable(EndDeviceInfo dev)
        {
            DataRow row = _macAddressDataTable.NewRow();

            row["MAC Address"] = EndDeviceInfo.FormatMAC(dev.MacAddress);
            row["Port"] = dev.Port;
            row["Timer"] = dev.Timer;

            lock (_macAddressDataTableLock)
            {
                _macAddressDataTable.Rows.Add(row);
            }

            if (_gui.dataGridViewMACAddressTable.InvokeRequired)
            {
                _gui.dataGridViewMACAddressTable.Invoke(new Action(() => { _gui.dataGridViewMACAddressTable.Refresh(); })); // TODO System.NullReferenceException: 'Object reference not set to an instance of an object.'

            }
        }

        public static void InitMACAddressTable()
        {
            _macAddressDataTable = new DataTable("macAddressTable");
            _macAddressRows = new List<DataRow>();
            _macAddressTable = new List<EndDeviceInfo>();
            
            DataColumn columnMAC = new DataColumn("MAC Address");
            DataColumn columnPort = new DataColumn("Port");
            DataColumn columnTimer = new DataColumn("Timer");


            //Add the Created Columns to the Datatable

            _macAddressDataTable.Columns.Add(columnMAC);
            _macAddressDataTable.Columns.Add(columnPort);
            _macAddressDataTable.Columns.Add(columnTimer);

            _gui.dataGridViewMACAddressTable.DataSource = _macAddressDataTable;
        }

        public static bool isInTable(string mac, uint port)
        {
            foreach (var record in _macAddressTable)
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
        
        private static void Thread_NetworkInterfaceSender(ref Queue<PacketForQueue> PacketQueue, ref object QueueLock, ref bool BackgroundThreadStop)
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
                    foreach (var qPacket in processQueue)
                    {
                        try
                        {
                            ILiveDevice senderDevice = (qPacket.device == _d1) ? _d2 : ((qPacket.device == _d2) ? _d1 : null);
                            if (senderDevice == null)
                            {
                                throw new Exception("Invalid device");
                            }
                            senderDevice.SendPacket(qPacket.ethPacket);
                            Statistics.UpdateStatistics( (senderDevice == _d1) ? 1 : 2 , qPacket, false);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "General Packet Send Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (!BackgroundThreadStop)
                {
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
    public class EndDeviceInfo
    {
        public string MacAddress { get; set; }
        public uint Port { get; set; }
        public uint Timer { get; set; }

        public EndDeviceInfo(string macAddress, uint port, uint timer)
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
