using PSIP_SW_Switch;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using System.Collections;
using System.Reflection;
using SharpPcap;
using System.Net.Sockets;

namespace PSIP_SW_Switch
{
    static class Statistics
    {
        static Form1 gui = Application.OpenForms.OfType<Form1>().FirstOrDefault();

        static string[] rowNames = new string[9] { "Ethernet II", "IP", "ARP", "TCP", "UDP", "ICMP", "HTTP", "HTTPS", "Total" };
        static string[] colNames = new string[2] { "IN", "OUT" };

        static Dictionary<Type, int> dataTableMapping = new Dictionary<Type, int>()
            {
                {typeof(IPv4Packet), 1},
                {typeof(IPv6Packet), 1},
                {typeof(ArpPacket), 2},
                {typeof(TcpPacket), 3},
                {typeof(UdpPacket), 4},
                {typeof(IcmpV4Packet), 5},
                {typeof(IcmpV6Packet), 5},
//{typeof(TcpPacket), 6},
 //           {typeof(TcpPacket), 7},

            };

        public static DataTable int1Stats;
        public static DataTable int2Stats;

        public static void InitGUI()
        {
            int1Stats = new DataTable("int1Stats");
            int2Stats = new DataTable("int2Stats");
            DataColumn i1cIN = new DataColumn("IN");
            DataColumn i1cOUT = new DataColumn("OUT");
            DataColumn i2cIN = new DataColumn("IN");
            DataColumn i2cOUT = new DataColumn("OUT");


            //Add the Created Columns to the Datatable

            int1Stats.Columns.Add(i1cIN);
            int1Stats.Columns.Add(i1cOUT);
            int2Stats.Columns.Add(i2cIN);
            int2Stats.Columns.Add(i2cOUT);

            //Create 3 rows

            DataRow[] int1Rows = new DataRow[9];
            DataRow[] int2Rows = new DataRow[9];




            for (int i = 0; i < rowNames.Length; i++)
            {
                int1Rows[i] = int1Stats.NewRow();
                int2Rows[i] = int2Stats.NewRow();

                for (int j = 0; j < colNames.Length; j++)
                {
                    int1Rows[i][colNames[j]] = 0;
                    int2Rows[i][colNames[j]] = 0;
                }
                int1Stats.Rows.Add(int1Rows[i]);
                int2Stats.Rows.Add(int2Rows[i]);

            }

            gui.dataGridViewInt1Stats.DataSource = int1Stats;
            gui.dataGridViewInt2Stats.DataSource = int2Stats;

            for (int i = 0; i < rowNames.Length; i++)
            {
                gui.dataGridViewInt1Stats.Rows[i].HeaderCell.Value = rowNames[i];
                gui.dataGridViewInt2Stats.Rows[i].HeaderCell.Value = rowNames[i];
            }

            foreach (DataGridViewColumn column in gui.dataGridViewInt1Stats.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in gui.dataGridViewInt2Stats.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }


        public static void ResetStatsInt(int intNum)
        {
            var dataTableRef = (intNum == 1) ? int1Stats : int2Stats;
            for (int rowIndex = 0; rowIndex < rowNames.Length; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colNames.Length; colIndex++)
                {
                    dataTableRef.Rows[rowIndex].SetField(colIndex, 0);
                }
            }
        }


        public static void AddStat(int intNum, SharpPcap.RawCapture capture)
        {
            var dataTableRef = (intNum == 1) ? int1Stats : int2Stats;

            if (capture.LinkLayerType == LinkLayers.Ethernet)
            {
                int ethIndex = Array.FindIndex(rowNames, m => m == "Ethernet II");
                if (ethIndex < 0)
                {
                    MessageBox.Show("Error finfing ETH II", "My Error", MessageBoxButtons.OK);
                    return;
                }
                dataTableRef.Rows[ethIndex].SetField("IN", int.Parse(dataTableRef.Rows[ethIndex]["IN"].ToString()) + 1);
            }

            var p = PacketDotNet.Packet.ParsePacket(capture.LinkLayerType, capture.Data);

            List<PacketDotNet.Packet> packetsPayloads = new List<Packet>();

            var tmp = p;
            while (tmp.PayloadPacket != null)
            {
                packetsPayloads.Add(tmp.PayloadPacket);
                tmp = tmp.PayloadPacket;
            }


            foreach (var pkt in packetsPayloads)
            {
                switch (pkt)
                {
                    case PacketDotNet.IPv4Packet:
                    case PacketDotNet.IPv6Packet:
                        dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.IPv4Packet)]].SetField("IN", int.Parse(dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.IPv4Packet)]]["IN"].ToString()) + 1);
                        break;

                    case PacketDotNet.ArpPacket:
                        dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.ArpPacket)]].SetField("IN", int.Parse(dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.ArpPacket)]]["IN"].ToString()) + 1);
                        break;
                    case PacketDotNet.TcpPacket:
                        dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.TcpPacket)]].SetField("IN", int.Parse(dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.TcpPacket)]]["IN"].ToString()) + 1);

                        var tcp = (TcpPacket)(pkt);

                        if (tcp.DestinationPort == 443)
                        {
                            dataTableRef.Rows[7].SetField("IN", int.Parse(dataTableRef.Rows[7]["IN"].ToString()) + 1);
                        } else if (tcp.DestinationPort == 80)
                        {
                            dataTableRef.Rows[6].SetField("IN", int.Parse(dataTableRef.Rows[6]["IN"].ToString()) + 1);
                        }

                        break;
                    case PacketDotNet.UdpPacket:
                        dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.UdpPacket)]].SetField("IN", int.Parse(dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.UdpPacket)]]["IN"].ToString()) + 1);
                        break;
                    case PacketDotNet.IcmpV4Packet:
                    case PacketDotNet.IcmpV6Packet:
                        dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.IcmpV4Packet)]].SetField("IN", int.Parse(dataTableRef.Rows[dataTableMapping[typeof(PacketDotNet.IcmpV4Packet)]]["IN"].ToString()) + 1);
                        break;

                }
            }

            // Add +1 To Total
            dataTableRef.Rows[rowNames.Length-1].SetField("IN", int.Parse(dataTableRef.Rows[rowNames.Length - 1]["IN"].ToString()) + 1);

        }

    }
}
