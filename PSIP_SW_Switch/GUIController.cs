using SharpPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace PSIP_SW_Switch
{
    static class GUIController
    {
        public static MainWindow gui = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

        public static class AclGUI
        {
            private static DataGridViewTextBoxColumn deviceColumn;
            private static DataGridViewTextBoxColumn aclRuleTypeColumn;
            private static DataGridViewTextBoxColumn allowanceColumn;
            private static DataGridViewTextBoxColumn protocolColumn;

            private static DataGridViewTextBoxColumn sourcePhysicalAddressColumn;
            private static DataGridViewTextBoxColumn destPhysicalAddressColumn;
            private static DataGridViewTextBoxColumn sourceIpColumn;
            private static DataGridViewTextBoxColumn destIpColumn;
            private static DataGridViewTextBoxColumn sourcePortColumn;
            private static DataGridViewTextBoxColumn destPortColumn;

            private static DataGridViewButtonColumn ruleDeleteBtnColumn;


            public static void Init()
            {
                InitComboBoxes();
                InitDataGridView();
            }

            public static void InitComboBoxes()
            {
                gui.comboBoxACLAllowance.DataSource = Enum.GetValues(typeof(ACLAllowance));
                gui.comboBoxACLProtocol.DataSource = Enum.GetValues(typeof(Protocol));
                gui.comboBoxACLDirection.DataSource = Enum.GetValues(typeof(ACLRuleDirection));
                gui.comboBoxICMPType.DataSource = Enum.GetValues(typeof(ICMPType));

                gui.comboBoxACLDevice.Items.Add("1");
                gui.comboBoxACLDevice.Items.Add("2");
                gui.comboBoxACLDevice.Items.Add("ANY");
                gui.comboBoxACLDevice.SelectedIndex = 0;
            }
            public static void InitDataGridView()
            {
                // TODO Chnage to observable collection
                // TODO also make sure that MAC address is formatted
                gui.dataGridViewACL.DataSource = null;
                Refresh();

                deviceColumn = new DataGridViewTextBoxColumn();
                deviceColumn.DataPropertyName = "device";
                deviceColumn.Name = deviceColumn.DataPropertyName;
                deviceColumn.HeaderText = "Interface";
                //deviceColumn.Items.AddRange("1", "2", "ANY");

                aclRuleTypeColumn = new DataGridViewTextBoxColumn();
                aclRuleTypeColumn.DataPropertyName = "AclRuleType";
                aclRuleTypeColumn.HeaderText = "Rule Type";
                aclRuleTypeColumn.Name = aclRuleTypeColumn.DataPropertyName;
                //aclRuleTypeColumn.DataSource = Enum.GetValues(typeof(ACLRuleDirection));

                allowanceColumn = new DataGridViewTextBoxColumn();
                allowanceColumn.DataPropertyName = "Allowance";
                allowanceColumn.HeaderText = "Allowance";
                allowanceColumn.Name = allowanceColumn.DataPropertyName;
                //allowanceColumn.DataSource = Enum.GetValues(typeof(ACLAllowance));

                sourcePhysicalAddressColumn = new DataGridViewTextBoxColumn();
                sourcePhysicalAddressColumn.DataPropertyName = "packetInfo.SourcePhysicalAddress";
                sourcePhysicalAddressColumn.HeaderText = "Src. MAC";
                sourcePhysicalAddressColumn.Name = sourcePhysicalAddressColumn.DataPropertyName;

                destPhysicalAddressColumn = new DataGridViewTextBoxColumn();
                destPhysicalAddressColumn.DataPropertyName = "packetInfo.DestinationPhysicalAddress";
                destPhysicalAddressColumn.HeaderText = "Dst. MAC";
                destPhysicalAddressColumn.Name = destPhysicalAddressColumn.DataPropertyName;

                sourceIpColumn = new DataGridViewTextBoxColumn();
                sourceIpColumn.DataPropertyName = "packetInfo.SourceIpAddress";
                sourceIpColumn.HeaderText = "Src. IP";
                sourceIpColumn.Name = sourceIpColumn.DataPropertyName;

                destIpColumn = new DataGridViewTextBoxColumn();
                destIpColumn.DataPropertyName = "packetInfo.DestinationIpAddress";
                destIpColumn.HeaderText = "Dst. IP";
                destIpColumn.Name = destIpColumn.DataPropertyName;

                sourcePortColumn = new DataGridViewTextBoxColumn();
                sourcePortColumn.DataPropertyName = "packetInfo.SourcePort";
                sourcePortColumn.HeaderText = "Src. Port";
                sourcePortColumn.Name = sourcePortColumn.DataPropertyName;

                destPortColumn = new DataGridViewTextBoxColumn();
                destPortColumn.DataPropertyName = "packetInfo.DestinationPort";
                destPortColumn.HeaderText = "Dst. Port";
                destPortColumn.Name = destPortColumn.DataPropertyName;

                protocolColumn = new DataGridViewTextBoxColumn();
                protocolColumn.DataPropertyName = "packetInfo.Protocol";
                protocolColumn.HeaderText = "Protocol";
                protocolColumn.Name = protocolColumn.DataPropertyName;
                //protocolColumn.DataSource = Enum.GetValues(typeof(Protocol));

                ruleDeleteBtnColumn = new DataGridViewButtonColumn();
                ruleDeleteBtnColumn.HeaderText = "Delete?";
                ruleDeleteBtnColumn.Text = "Delete";
                ruleDeleteBtnColumn.Name = "buttonACLRuleDelete";
                ruleDeleteBtnColumn.UseColumnTextForButtonValue = true;


                // add columns to the DataGridView
                gui.dataGridViewACL.Columns.Add(deviceColumn);
                gui.dataGridViewACL.Columns.Add(aclRuleTypeColumn);
                gui.dataGridViewACL.Columns.Add(allowanceColumn);
                gui.dataGridViewACL.Columns.Add(sourcePhysicalAddressColumn);
                gui.dataGridViewACL.Columns.Add(destPhysicalAddressColumn);
                gui.dataGridViewACL.Columns.Add(sourceIpColumn);
                //gui.dataGridViewACL.Columns.Add(sourcePortColumn);
                gui.dataGridViewACL.Columns.Add(destIpColumn);
                gui.dataGridViewACL.Columns.Add(destPortColumn);
                gui.dataGridViewACL.Columns.Add(protocolColumn);
                gui.dataGridViewACL.Columns.Add(ruleDeleteBtnColumn);

                // TODO DO Matches!!!!!!

                // Set up the data binding between the ObservableCollection and the DataGridView
                gui.dataGridViewACL.DataSource = ACL.AclRules;

                // Add an event handler to the CellFormatting event of the DataGridView
                // This will allow us to format the MAC Address column
                gui.dataGridViewACL.CellFormatting += onCellFormating;
                gui.dataGridViewACL.CellContentClick += onCellContentClick;
                Refresh();
            }

            public static void Refresh()
            {
                if (gui.dataGridViewACL.InvokeRequired)
                {
                    gui.dataGridViewACL.Invoke(() => gui.dataGridViewACL.Refresh());
                }
                else
                {
                    gui.dataGridViewACL.Refresh();
                }
            }

            public static (bool, ACLRule?) AddRule()
            {
                try
                {
                    ILiveDevice? device = null;
                    if (gui.comboBoxACLDevice.SelectedItem.Equals("1"))
                        device = InterfaceController.d1;
                    else if(gui.comboBoxACLDevice.SelectedItem.Equals("2"))
                        device = InterfaceController.d2;
                        
                    ACLAllowance allowance = (ACLAllowance)gui.comboBoxACLAllowance.SelectedValue;
                    ACLRuleDirection direction = (ACLRuleDirection)gui.comboBoxACLAllowance.SelectedValue;
                    Protocol protocol = (Protocol)gui.comboBoxACLProtocol.SelectedValue;
                    ushort Port = ushort.Parse(gui.numericUpDownACLDstPort.Text);


                    PhysicalAddress srcMAC;
                    PhysicalAddress dstMAC;
                    IPAddress srcIP;
                    IPAddress dstIP;
                    ICMPType icmpType = ICMPType.ANY;
                    

                    if (gui.textBoxACLSrcMAC.Text == "")
                    {
                        srcMAC = PhysicalAddress.None;
                    }
                    else
                    {

                        if (!PhysicalAddress.TryParse(gui.textBoxACLSrcMAC.Text, out srcMAC))
                        {
                            throw new InvalidDataException("Source MAC address in bad format");
                        }
                    }

                    if (gui.textBoxACLCLientMAC.Text == "")
                    {
                        dstMAC = PhysicalAddress.None;
                    }
                    else
                    {
                        if (!PhysicalAddress.TryParse(gui.textBoxACLCLientMAC.Text, out dstMAC))
                        {
                            throw new InvalidDataException("Destination MAC address in bad format");
                        }
                    }

                    if (gui.textBoxACLSrvIP.Text == "")
                    {
                        srcIP = IPAddress.Any;
                    }
                    else
                    {

                        if (!IPAddress.TryParse(gui.textBoxACLSrvIP.Text, out srcIP))
                        {
                            throw new InvalidDataException("Source IP address in bad format");
                        }
                    }

                    if (gui.textBoxACLClientIP.Text == "")
                    {
                        dstIP = IPAddress.Any;
                    }
                    else
                    {
                        if (!IPAddress.TryParse(gui.textBoxACLClientIP.Text, out dstIP))
                        {
                            throw new InvalidDataException("Destination IP address in bad format");
                        }
                    }

                    if (gui.comboBoxICMPType.Enabled)
                    {
                        icmpType = (ICMPType)gui.comboBoxICMPType.SelectedIndex;
                    }

                    PacketInfo packetTemplate = new PacketInfo(srcMAC, dstMAC, srcIP, dstIP,  Port, protocol, icmpType);
                    ACLRule rule = new ACLRule(direction, allowance, device, packetTemplate);
                    lock (ACL.AclRulesListLock)
                    {
                        if (gui.dataGridViewACL.InvokeRequired)
                        {
                            gui.dataGridViewACL.Invoke(() => ACL.AclRules.Add(rule));
                        }
                        else
                        {
                            ACL.AclRules.Add(rule);
                        }
                    }
                    return (true, rule);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "ACE - Wrong format", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return (false, null);
                }
            }

            private static void onCellFormating(object sender, DataGridViewCellFormattingEventArgs e)
            {
                // Check if we're formatting the MAC Address column
                if(e.ColumnIndex == gui.dataGridViewACL.Columns["device"].Index){
                    if (e.Value == null || e.Value == "ANY")
                        e.Value = "ANY";
                    else
                    {
                        var dev = (ILiveDevice)e.Value;
                        e.Value = (dev == InterfaceController.d1) ? "1" : "2";
                    }

                    e.FormattingApplied = true;
                } else if (e.ColumnIndex == gui.dataGridViewACL.Columns["packetInfo.SourcePhysicalAddress"].Index
                           ||
                           e.ColumnIndex == gui.dataGridViewACL.Columns["packetInfo.DestinationPhysicalAddress"].Index)
                {
                    if(e.Value == null)
                        return;

                    // Get the MAC address value from the EndDeviceRecord object
                    PhysicalAddress macAddress = (PhysicalAddress)e.Value;

                    // Format the MAC address using the formatMAC function
                    string formattedMACAddress = EndDeviceRecord.FormatMAC(macAddress);

                    // Set the formatted value in the cell
                    e.Value = formattedMACAddress == "" ? "ANY" : formattedMACAddress;
                    e.FormattingApplied = true;
                } else if (e.ColumnIndex == gui.dataGridViewACL.Columns["packetInfo.SourceIpAddress"].Index
                           ||
                           e.ColumnIndex == gui.dataGridViewACL.Columns["packetInfo.DestinationIpAddress"].Index)
                {
                    if (e.Value == null)
                        return;

                    IPAddress ip = (IPAddress)(e.Value);

                    e.Value = ip.Equals(IPAddress.Any) ? "ANY" : ip.ToString();
                    e.FormattingApplied = true;
                }
            }

            private static void onCellContentClick(object s, DataGridViewCellEventArgs e)
            {
                if (e.ColumnIndex == gui.dataGridViewACL.Columns["buttonACLRuleDelete"].Index)
                {
                    DialogResult delete = MessageBox.Show("Are you sure you want to delete this rule?",
                        "ACE Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (delete == DialogResult.Yes)
                    {
                        RemoveAt(e.RowIndex);
                        Refresh();
                    }

                }
            }

            public static void RemoveAt(int index)
            {
                lock (ACL.AclRulesListLock)
                {
                    if (gui.dataGridViewACL.InvokeRequired)
                    {
                        gui.dataGridViewACL.Invoke(() => { ACL.AclRules.RemoveAt(index); });
                    }
                    else
                    {
                        ACL.AclRules.RemoveAt(index);
                    }
                }
            }  

        }

        public static class MacGUI
        {
            public static void InitNumeric()
            {
                gui.numericUpDownMACAddressTableTimerValue.Value = 30;
                gui.numericUpDownMACAddressTableTimerValue.Minimum = 0;
                gui.numericUpDownMACAddressTableTimerValue.Maximum = int.MaxValue;
            }
            public static void Init()
            {
                gui.dataGridViewMACAddressTable.DataSource = null;
                Refresh();

                // Add a column to the DataGridView for the formatted MAC address
                DataGridViewColumn macAddressColumn = new DataGridViewTextBoxColumn();
                macAddressColumn.Name = "MAC Address";
                macAddressColumn.DataPropertyName = "MacAddress"; // bind to the MACAddress property
                gui.dataGridViewMACAddressTable.Columns.Add(macAddressColumn);

                foreach (var prop in typeof(EndDeviceRecord).GetProperties())
                {
                    if (prop.Name != "MacAddress")
                    {
                        DataGridViewColumn column = new DataGridViewTextBoxColumn();
                        column.Name = prop.Name;
                        column.DataPropertyName = prop.Name; // bind to the property
                        gui.dataGridViewMACAddressTable.Columns.Add(column);
                    }
                }

                // Set up the data binding between the ObservableCollection and the DataGridView
                gui.dataGridViewMACAddressTable.DataSource = MACAddressTable.MacAddressTable;

                // Add an event handler to the CellFormatting event of the DataGridView
                // This will allow us to format the MAC Address column
                gui.dataGridViewMACAddressTable.CellFormatting += onCellFormating;
                Refresh();
            }

            public static void AddRecord(EndDeviceRecord rec)
            {
                lock (MACAddressTable.MacAddressTableLock)
                {
                    if (GUIController.gui.dataGridViewMACAddressTable.InvokeRequired)
                    {
                        GUIController.gui.dataGridViewMACAddressTable.BeginInvoke(new Action(() =>
                        {
                            // This code will be executed on the UI thread
                            MACAddressTable.MacAddressTable.Add(rec);
                        }));
                    }
                    else
                    {
                        // This code is already running on the UI thread
                        MACAddressTable.MacAddressTable.Add(rec);
                    }

                }
            }

            public static void StopTimer()
            {
                gui.MacTableTimer.Enabled = false;
            }

            public static void StartTimer()
            {
                gui.MacTableTimer.Interval = 1000;
                gui.MacTableTimer.Enabled = true;
            }

            public static void Refresh()
            {
                if (gui.dataGridViewMACAddressTable.InvokeRequired)
                {
                    gui.dataGridViewMACAddressTable.Invoke(new Action(() =>
                    {
                        gui.dataGridViewMACAddressTable.Refresh();
                    }));
                }
                else
                {
                    gui.dataGridViewMACAddressTable.Refresh();
                }
            }

            public static void ResumeLayout()
            {
                if (gui.dataGridViewMACAddressTable.InvokeRequired)
                {
                    gui.dataGridViewMACAddressTable.Invoke(new Action(() =>
                    {
                        gui.dataGridViewMACAddressTable.ResumeLayout();
                    }));
                } else
                    gui.dataGridViewMACAddressTable.ResumeLayout();
            }

            public static void SuspendLayout()
            {
                if (gui.dataGridViewMACAddressTable.InvokeRequired)
                {
                    gui.dataGridViewMACAddressTable.Invoke(new Action(() =>
                    {
                        gui.dataGridViewMACAddressTable.SuspendLayout();
                    }));
                }
                else
                    gui.dataGridViewMACAddressTable.SuspendLayout();
            }

            private static void onCellFormating(object sender, DataGridViewCellFormattingEventArgs e)
            {
                // Check if we're formatting the MAC Address column
                if (e.ColumnIndex == gui.dataGridViewMACAddressTable.Columns["MAC Address"].Index && e.Value != null)
                {
                    // Get the MAC address value from the EndDeviceRecord object
                    PhysicalAddress macAddress = (PhysicalAddress)e.Value;

                    // Format the MAC address using the formatMAC function
                    string formattedMACAddress = EndDeviceRecord.FormatMAC(macAddress);

                    // Set the formatted value in the cell
                    e.Value = formattedMACAddress;
                    e.FormattingApplied = true;
                }
            }
        }
    }
}