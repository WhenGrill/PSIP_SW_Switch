using System.Data;
using System.Net;

namespace PSIP_SW_Switch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InterfaceController.GUIRefreshInterfaces();
            Statistics.InitGUI();
            buttonSwitchEnable.Enabled = false;
            buttonSwitchDisable.Enabled = false;
        }

        private void buttonRefreshInterfacesLists_Click(object sender, EventArgs e)
        {
            InterfaceController.GUIRefreshInterfaces();
        }

        private void buttonSwitchEnable_Click(object sender, EventArgs e)
        {
            
            if (this.comboBoxInterfaceList1.SelectedItem == null || this.comboBoxInterfaceList2.SelectedItem == null)
            {
                return;
            }

            InterfaceController.InterfaceCaptureStart();

            buttonSwitchEnable.Enabled = false;
            buttonSwitchDisable.Enabled = true;
            comboBoxInterfaceList1.Enabled = false;
            comboBoxInterfaceList2.Enabled = false;
            buttonRefreshInterfacesLists.Enabled = false;

            labelInt1Stat.Text = labelInt1Stat.Text + " - " + comboBoxInterfaceList1.SelectedItem.ToString();
            labelInt2Stat.Text = labelInt2Stat.Text + " - " + comboBoxInterfaceList2.SelectedItem.ToString();
        }
        private void comboBoxInterfaceList1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxInterfaceList2.SelectedItem != null && comboBoxInterfaceList2.SelectedItem != comboBoxInterfaceList1.SelectedItem)
            {
                buttonSwitchEnable.Enabled = true;
            }
            else
            {
                buttonSwitchEnable.Enabled = false;
            }
        }

        private void comboBoxInterfaceList2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxInterfaceList1.SelectedItem != null && comboBoxInterfaceList2.SelectedItem != comboBoxInterfaceList1.SelectedItem)
            {
                buttonSwitchEnable.Enabled = true;
            }
            else
            {
                buttonSwitchEnable.Enabled = false;
            }
        }

        private void buttonSwitchDisable_Click(object sender, EventArgs e)
        {
            InterfaceController.InterfaceCaptureStop();
            comboBoxInterfaceList1.Enabled = true;
            comboBoxInterfaceList2.Enabled = true;
            buttonSwitchDisable.Enabled = false;
            buttonSwitchEnable.Enabled = true;
            buttonRefreshInterfacesLists.Enabled = true;
            labelInt1Stat.Text = "Interface 1";
            labelInt2Stat.Text = "Interface 2";
        }

        private void buttonInt1StatReset_Click(object sender, EventArgs e)
        {
            Statistics.ResetStatsInt(1);
        }

        private void buttonInt2StatReset_Click(object sender, EventArgs e)
        {
            Statistics.ResetStatsInt(2);
        }
    }
}