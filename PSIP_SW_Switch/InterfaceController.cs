using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIP_SW_Switch
{
    static class InterfaceController
    {
        static Form1 gui = Application.OpenForms.OfType<Form1>().FirstOrDefault();

        private static Queue<SharpPcap.RawCapture> QueueInt1 = new Queue<RawCapture>();
        private static Queue<SharpPcap.RawCapture> QueueInt2 = new Queue<RawCapture>();
        private static object QueueInt1Lock = new object();
        private static object QueueInt2Lock = new object();

        private static ILiveDevice d1;
        private static ILiveDevice d2;

        static SharpPcap.CaptureDeviceList devices = SharpPcap.CaptureDeviceList.Instance;


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
            d1 = devices[gui.comboBoxInterfaceList1.SelectedIndex];
            d2 = devices[gui.comboBoxInterfaceList2.SelectedIndex];

            d1.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
            d2.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
            try
            {
                d1.Open();
                d2.Open();

                d1.StartCapture();
                d2.StartCapture();
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
                d1.StopCapture();
                d2.StopCapture();
                d1.Close();
                d2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error closing network interface device", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Events
        private static void device_OnPacketArrival(object sender, PacketCapture e)
        {
            ILiveDevice device = (ILiveDevice)sender;
            var cap = e.GetPacket();

            if (device == d1)
            {
                // Add Packet to queue
                lock (QueueInt1Lock)
                {
                    QueueInt1.Enqueue(cap);
                }

                // Update Statistics for Interface 1
                Statistics.AddStat(1, cap);

                //TODO Process MAC Address Table


            }
            else if (device == d2)
            {
                // Add Packet to queue
                lock (QueueInt2Lock)
                {
                    QueueInt2.Enqueue(cap);
                }

                // Update Statistics for Interface 2
                Statistics.AddStat(2, cap);

                //TODO Process MAC Address Table
            }
            else
            {
                MessageBox.Show("Cannot capture packet: " + e.GetPacket().ToString(), "Error capturing packet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
