using PSIP_SW_Switch;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSIP_SW_Switch
{
    static class Statistics
    {
        static Form1 gui = Application.OpenForms.OfType<Form1>().FirstOrDefault();

        static string[] rowNames = new string[9] { "Ethernet II", "IP", "ARP", "TCP", "UDP", "ICMP", "HTTP", "HTTPS", "Total" };
        static string[] colNames = new string[2] { "IN", "OUT" };

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




    }
}
