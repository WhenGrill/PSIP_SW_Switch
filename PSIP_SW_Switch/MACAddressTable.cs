using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIP_SW_Switch
{
    static class MACAddressTable
    {
        private static object? _macAddressDataTableLock;
        private static object? _macAddressRowsLock;
        private static object? _macAddressTableLock;

        private static DataTable? _macAddressDataTable;
        private static List<DataRow>? _macAddressRows;
        private static List<EndDeviceInfo>? _macAddressTable;

        public static void ClearMACAddressTable()
        {
            InitMACAddressTable();
        }
        public static void UpdateMACAddressTable(PacketForQueue packet)
        {
            // if (cap.LinkLayerType != LinkLayers.Ethernet) // TODO co s tymto
            //     return;

            var srcMac = packet.ethPacket.SourceHardwareAddress.ToString();

            if (InterfaceController.d1.MacAddress.Equals(packet.ethPacket.SourceHardwareAddress) || InterfaceController.d2.MacAddress.Equals(packet.ethPacket.SourceHardwareAddress) || isInTable(packet))
            {
                return;
            }

            var endDeviceInfo = new EndDeviceInfo(packet, InterfaceController.deviceMap[packet.device], 1800);

            lock (_macAddressTableLock)
            {
                _macAddressTable.Add(endDeviceInfo);
            }

            UpdateGUIMACAddressTable(endDeviceInfo);
        }

        private static void UpdateGUIMACAddressTable(EndDeviceInfo dev)
        {
            DataRow row = _macAddressDataTable.NewRow();

            row["MAC Address"] = EndDeviceInfo.FormatMAC(dev.MacAddress.ToString());
            row["Port"] = dev.Port;
            row["Timer"] = dev.Timer;

            lock (_macAddressDataTableLock)
            {
                _macAddressDataTable.Rows.Add(row);
            }

            if (InterfaceController.gui.dataGridViewMACAddressTable.InvokeRequired)
            {
                InterfaceController.gui.dataGridViewMACAddressTable.Invoke(new Action(() => {InterfaceController.gui.dataGridViewMACAddressTable.Refresh(); })); // TODO System.NullReferenceException: 'Object reference not set to an instance of an object.'

            }
        }

        public static void InitMACAddressTable()
        {
            _macAddressDataTable = new DataTable("macAddressTable");
            _macAddressRows = new();
            _macAddressTable = new();

            _macAddressDataTableLock = new();
            _macAddressRowsLock = new();
            _macAddressTableLock = new();

            DataColumn columnMAC = new DataColumn("MAC Address");
            DataColumn columnPort = new DataColumn("Port");
            DataColumn columnTimer = new DataColumn("Timer");


            //Add the Created Columns to the Datatable

            _macAddressDataTable.Columns.Add(columnMAC);
            _macAddressDataTable.Columns.Add(columnPort);
            _macAddressDataTable.Columns.Add(columnTimer);

            InterfaceController.gui.dataGridViewMACAddressTable.DataSource = _macAddressDataTable;
        }

        public static bool isInTable(PacketForQueue packet)
        {
            foreach (var record in _macAddressTable)
            {
                if (record.MacAddress.Equals(packet.ethPacket.DestinationHardwareAddress) && record.Port == InterfaceController.deviceMap[packet.device])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
