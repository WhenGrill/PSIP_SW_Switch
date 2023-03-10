using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSIP_SW_Switch
{
    static class MACAddressTable
    {

        public static object? MacAddressTableLock;
        public static BindingList<EndDeviceRecord>? MacAddressTable;

        public static int timerValue = 30;
        public static object? TimerLock;

        public static void Init()
        {
            GUIController.MacGUI.StopTimer();
            MacAddressTable = new();
            MacAddressTableLock = new object();
            TimerLock = new object();
            GUIController.MacGUI.InitNumeric();
            GUIController.MacGUI.Init();
            GUIController.MacGUI.StartTimer();
        }

        public static void StopTimer()
        {
            GUIController.MacGUI.StopTimer();
        }

        public static void Clear()
        {
            Init();
        }
        public static void AddRecord(PacketForQueue packet)
        {
            if (packet.ethPacket.DestinationHardwareAddress.Equals(PhysicalAddress.Parse("ff:ff:ff:ff:ff:ff")))
            {
                return;
            }

            if (InterfaceController.d1.MacAddress.Equals(packet.ethPacket.SourceHardwareAddress)
                ||
                InterfaceController.d2.MacAddress.Equals(packet.ethPacket.SourceHardwareAddress))
            {
                return;
            }
            
            if(IsInTable(packet, addRecord: true))
            {

                return;
            }

            lock (TimerLock)
            {
                var endDeviceInfo =
                    new EndDeviceRecord(packet, InterfaceController.deviceMap[packet.device],
                        timerValue);

                GUIController.MacGUI.AddRecord(endDeviceInfo);
            }
        }

        public static bool IsInTable(PacketForQueue packet, bool addRecord = false)
        {
            var mac = addRecord ? packet.ethPacket.SourceHardwareAddress : packet.ethPacket.DestinationHardwareAddress;
            lock (MacAddressTableLock)
            {
                foreach (var record in MacAddressTable)
                {
                    if (record.MacAddress.Equals(mac))
                    {
                        if (record.Port != InterfaceController.deviceMap[packet.device])
                        {
                            // Device moved from one port to another
                            MacAddressTable[MacAddressTable.IndexOf(record)].Port = record.Port;
                            SysLog.Log(SysLogSeverity.Alert, $"[MAC] Device {EndDeviceRecord.FormatMAC(record.MacAddress)} moved from port {InterfaceController.deviceMap[packet.device]} to port {record.Port}");
                        }

                        if (addRecord)
                        {
                            record.Timer = timerValue;
                            GUIController.MacGUI.Refresh();
                        }
                        return true;
                    }
                }

                if (addRecord)
                {
                    SysLog.Log(SysLogSeverity.Informational,
                        $"[MAC] New device [{EndDeviceRecord.FormatMAC(packet.ethPacket.SourceHardwareAddress)}] on port [{InterfaceController.deviceMap[packet.device]}]");
                }
            }
            return false;
        }

        public static void UpdateRecordExpirations()
        {
            //GUIController.MacGUI.SuspendLayout();

            lock (MacAddressTableLock)
            {
                foreach (var record in MacAddressTable)
                {
                    record.Timer--;
                }
            }

            lock (MacAddressTableLock)
            {
                var expiredRecords = MacAddressTable.Where(item => item.Timer <= 0).ToList();
                foreach (var expiredRecord in expiredRecords)
                {
                    MacAddressTable.Remove(expiredRecord);
                }
            }
            //GUIController.MacGUI.ResumeLayout();
            GUIController.MacGUI.Refresh();
        }


    }
}
