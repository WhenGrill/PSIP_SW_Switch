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

        private static Queue<SharpPcap.RawCapture> int1Packets;
        private static Queue<SharpPcap.RawCapture> int2Packets;

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
            var d1 = devices[gui.comboBoxInterfaceList1.SelectedIndex];
            var d2 = devices[gui.comboBoxInterfaceList2.SelectedIndex];

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
            var d1 = devices[gui.comboBoxInterfaceList1.SelectedIndex];
            var d2 = devices[gui.comboBoxInterfaceList2.SelectedIndex];
            try
            {
                d1.StopCapture();
                d2.StopCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error closing network interface device", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                d1.Close();
                d2.Close();
            }

        }


        private static void device_OnPacketArrival(object sender, PacketCapture e)
        {
            // print out periodic statistics about this device
           /* var Now = DateTime.Now; // cache 'DateTime.Now' for minor reduction in cpu overhead
            var interval = Now - LastStatisticsOutput;
            if (interval > LastStatisticsInterval)
            {
                Console.WriteLine("device_OnPacketArrival: " + e.Device.Statistics);
                LastStatisticsOutput = Now;
            }

            // lock QueueLock to prevent multiple threads accessing PacketQueue at
            // the same time
            lock (QueueLock)
            {
                PacketQueue.Add(e.GetPacket());
            }*/
           ILiveDevice device = (ILiveDevice)sender;

           var cap = e.GetPacket();
           var p = PacketDotNet.Packet.ParsePacket(cap.LinkLayerType, cap.Data);
           Console.WriteLine("[[INT {0}]] {1}", device.Description, p.ToString());
           // TODO Statistics based oni interface


        }

    }

    static class NetDataManipulator
    {

    }
}
