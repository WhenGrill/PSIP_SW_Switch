using System.Net;
using System.Windows.Forms;

namespace PSIP_SW_Switch
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            InterfaceController.Init();
            Statistics.Init();
            ACL.Init();
            buttonSwitchEnable.Enabled = false;
            buttonSwitchDisable.Enabled = false;

            if (numericUpDownACLDstPort.Value == 0)
            {
                numericUpDownACLDstPort.Text = "ANY";
            }

            comboBoxACLAllowance.SelectedIndex = 1;
            comboBoxACLProtocol.SelectedIndex = 3;
            comboBoxICMPType.SelectedIndex = Enum.GetValues(typeof(ICMPType)).Length - 1;

            buttonACLAddRule.Enabled = false;
            buttonACLDeleteAll.Enabled = false;
            checkBoxACLEnabled.Enabled = false;

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
            MACAddressTable.Init();


            buttonSwitchEnable.Enabled = false;
            buttonSwitchDisable.Enabled = true;
            comboBoxInterfaceList1.Enabled = false;
            comboBoxInterfaceList2.Enabled = false;
            buttonRefreshInterfacesLists.Enabled = false;
            buttonACLAddRule.Enabled = true;
            buttonACLDeleteAll.Enabled = true;
            checkBoxACLEnabled.Enabled = true;

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
            MACAddressTable.StopTimer();
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

        private void buttonMACAddressTableClear_Click(object sender, EventArgs e)
        {
           MACAddressTable.Clear();
        }

        private void buttonStatResetAll_Click(object sender, EventArgs e)
        {
            Statistics.ResetStatsInt(1);
            Statistics.ResetStatsInt(2);
        }

        private void MacTableTimer_Tick(object sender, EventArgs e)
        {
            MACAddressTable.UpdateRecordExpirations();
        }

        private void buttonSetMACTimer_Click(object sender, EventArgs e)
        {
            lock (MACAddressTable.TimerLock)
            {
                MACAddressTable.timerValue = Convert.ToInt32(Math.Round(numericUpDownMACAddressTableTimerValue.Value, 0));
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            InterfaceController.InterfaceCaptureStop();
            MACAddressTable.StopTimer();
        }

        private void buttonSysLogConfigure_Click(object sender, EventArgs e)
        {

            try
            {
                IPAddress srvIP = IPAddress.Parse(textBoxSysLogServerIP.Text);
                IPAddress clientIP = IPAddress.Parse(textBoxSysLogClientIP.Text);
                ushort srvPort = ushort.Parse(textBoxSysLogServerPort.Text);
                ushort clientPort = ushort.Parse(textBoxSysLogClientPort.Text);

                SysLog.ServerIpAddress = srvIP;
                SysLog.ClientIpAddress = clientIP;
                SysLog.ServerPort = srvPort;
                SysLog.ClientPort = clientPort;

                SysLog.sender = (radioButtonSysLogInt1.Checked) ? InterfaceController.d1 : InterfaceController.d2;
            }
            catch
            {
                MessageBox.Show(
                    "Configuration validation warning. Check formats of IPs and Ports. Configuration not set!",
                    "SysLog Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void radioButtonSysLogInt1_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonSysLogInt2.Checked = !radioButtonSysLogInt1.Checked;
        }

        private void radioButtonSysLogInt2_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonSysLogInt2.Checked = !radioButtonSysLogInt1.Checked;
        }

        private void checkBoxSysLogEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SysLog.Enabled = checkBoxSysLogEnabled.Checked;
        }

        private void CableDetectionTimer_Tick(object sender, EventArgs e)
        {
            var d1ElapsedTime = DateTime.Now - InterfaceController.d1LastPacketArrival;
            var d2ElapsedTime = DateTime.Now - InterfaceController.d2LastPacketArrival;

            if ((decimal)d1ElapsedTime.TotalSeconds > numericUpDownCableDetectionSeconds.Value)
            {
                InterfaceController.d1CableDisconnected = true;
                SysLog.Log(SysLogSeverity.Critical, "[INTERFACE] [1] Cable Disconnected!");
                labeld1CableConnected.Text = "DISCONNECTED";
                labeld1CableConnected.ForeColor = Color.Red;
            }
            else
            {
                if (InterfaceController.d1CableDisconnected)
                {
                    InterfaceController.d1CableDisconnected = false;
                    SysLog.Log(SysLogSeverity.Informational, "[INTERFACE] [1] Cable Connected!");
                    labeld1CableConnected.Text = "CONNECTED";
                    labeld1CableConnected.ForeColor = Color.ForestGreen;
                }
            }

            if ((decimal)d2ElapsedTime.TotalSeconds > numericUpDownCableDetectionSeconds.Value)
            {
                InterfaceController.d2CableDisconnected = true;
                SysLog.Log(SysLogSeverity.Critical, "[INTERFACE] [2] Cable Disconnected!");
                labeld2CableConnected.Text = "DISCONNECTED";
                labeld2CableConnected.ForeColor = Color.Red;
            }
            else
            {
                if (InterfaceController.d2CableDisconnected)
                {
                    InterfaceController.d2CableDisconnected = false;
                    SysLog.Log(SysLogSeverity.Informational, "[INTERFACE] [2] Cable Connected!");
                    labeld2CableConnected.Text = "CONNECTED";
                    labeld2CableConnected.ForeColor = Color.ForestGreen;
                }
            }
        }

        private void comboBoxACLProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Protocol)comboBoxACLProtocol.SelectedItem == Protocol.TCP || (Protocol)comboBoxACLProtocol.SelectedItem == Protocol.UDP || (Protocol)comboBoxACLProtocol.SelectedItem == Protocol.ANY)
            {
                comboBoxICMPType.Enabled = false;
            }
            else
            {
                comboBoxICMPType.Enabled = true;
            }
        }

        private void buttonACLAddRule_Click(object sender, EventArgs e)
        {
            ACL.AddRule();

            /*if (!ACL.Enabled)
                MessageBox.Show("Rule has been added but because ACL processing is Disabled It won't have any effect.",
                    "ACE Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
        }

        private void checkBoxACLEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxACLEnabled.Checked)
            {
                SysLog.Log(SysLogSeverity.Alert, "[ACL] [UP]: ACL Processing turned on.");
                
            }
            else
            {
                SysLog.Log(SysLogSeverity.Alert, "[ACL] [DOWN]: ACL Processing has been turned off");
            }

            ACL.Enabled = checkBoxACLEnabled.Checked;
        }

        private void buttonCableStatusSetSeconds_Click(object sender, EventArgs e)
        {
        }

        private void numericUpDownACLDstPort_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownACLDstPort.Value > 0)
            {
                numericUpDownACLDstPort.Text = numericUpDownACLDstPort.Value.ToString();
            } else if (numericUpDownACLDstPort.Value == 0)
            {
                numericUpDownACLDstPort.Text = "ANY";
            }
        }
    }
}