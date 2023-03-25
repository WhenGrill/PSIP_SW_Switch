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
            private static DataGridViewTextBoxColumn IcmpTypeColumn;

            private static DataGridViewTextBoxColumn sourcePhysicalAddressColumn;
            private static DataGridViewTextBoxColumn destPhysicalAddressColumn;
            private static DataGridViewTextBoxColumn sourceIpColumn;
            private static DataGridViewTextBoxColumn destIpColumn;
            private static DataGridViewTextBoxColumn sourcePortColumn;
            private static DataGridViewTextBoxColumn destPortColumn;
            private static DataGridViewTextBoxColumn noMatchesColumn;

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
                gui.dataGridViewACL.DataSource = null;
                gui.dataGridViewACL.AllowUserToAddRows = false;
                gui.dataGridViewACL.AllowUserToDeleteRows = false;
                gui.dataGridViewACL.ReadOnly = true;
                gui.dataGridViewACL.EditMode = DataGridViewEditMode.EditProgrammatically;
                gui.dataGridViewACL.AutoGenerateColumns = false;

                Refresh();

                deviceColumn = new DataGridViewTextBoxColumn();
                deviceColumn.DataPropertyName = "device";
                deviceColumn.Name = deviceColumn.DataPropertyName;
                deviceColumn.HeaderText = "Interface";
                deviceColumn.DisplayIndex = 0;
                //deviceColumn.Items.AddRange("1", "2", "ANY");

                aclRuleTypeColumn = new DataGridViewTextBoxColumn();
                aclRuleTypeColumn.DataPropertyName = "AclRuleDirection";
                aclRuleTypeColumn.HeaderText = "Rule Type";
                aclRuleTypeColumn.Name = aclRuleTypeColumn.DataPropertyName;
                aclRuleTypeColumn.DisplayIndex = 1;
                //aclRuleTypeColumn.DataSource = Enum.GetValues(typeof(ACLRuleDirection));

                allowanceColumn = new DataGridViewTextBoxColumn();
                allowanceColumn.DataPropertyName = "Allowance";
                allowanceColumn.HeaderText = "Allowance";
                allowanceColumn.Name = allowanceColumn.DataPropertyName;
                allowanceColumn.DisplayIndex = 2;
                //allowanceColumn.DataSource = Enum.GetValues(typeof(ACLAllowance));

                sourcePhysicalAddressColumn = new DataGridViewTextBoxColumn();
                sourcePhysicalAddressColumn.DataPropertyName = "SourcePhysicalAddress";
                sourcePhysicalAddressColumn.HeaderText = "Src. MAC";
                sourcePhysicalAddressColumn.Name = sourcePhysicalAddressColumn.DataPropertyName;
                sourcePhysicalAddressColumn.DisplayIndex = 3;

                destPhysicalAddressColumn = new DataGridViewTextBoxColumn();
                destPhysicalAddressColumn.DataPropertyName = "DestinationPhysicalAddress";
                destPhysicalAddressColumn.HeaderText = "Dst. MAC";
                destPhysicalAddressColumn.Name = destPhysicalAddressColumn.DataPropertyName;
                destPhysicalAddressColumn.DisplayIndex = 4;

                sourceIpColumn = new DataGridViewTextBoxColumn();
                sourceIpColumn.DataPropertyName = "SourceIpAddress";
                sourceIpColumn.HeaderText = "Src. IP";
                sourceIpColumn.Name = sourceIpColumn.DataPropertyName;
                sourceIpColumn.DisplayIndex = 5;

                destIpColumn = new DataGridViewTextBoxColumn();
                destIpColumn.DataPropertyName = "DestinationIpAddress";
                destIpColumn.HeaderText = "Dst. IP";
                destIpColumn.Name = destIpColumn.DataPropertyName;
                destIpColumn.DisplayIndex = 6;

                sourcePortColumn = new DataGridViewTextBoxColumn();
                sourcePortColumn.DataPropertyName = "SourcePort";
                sourcePortColumn.HeaderText = "Src. Port";
                sourcePortColumn.Name = sourcePortColumn.DataPropertyName;
                sourcePortColumn.DisplayIndex = 7;

                destPortColumn = new DataGridViewTextBoxColumn();
                destPortColumn.DataPropertyName = "DestinationPort";
                destPortColumn.HeaderText = "Dst. Port";
                destPortColumn.Name = destPortColumn.DataPropertyName;
                destPortColumn.DisplayIndex = 8;

                protocolColumn = new DataGridViewTextBoxColumn();
                protocolColumn.DataPropertyName = "Protocol";
                protocolColumn.HeaderText = "Protocol";
                protocolColumn.Name = protocolColumn.DataPropertyName;
                protocolColumn.DisplayIndex = 9;

                IcmpTypeColumn = new DataGridViewTextBoxColumn();
                IcmpTypeColumn.DataPropertyName = "IcmpType";
                IcmpTypeColumn.HeaderText = "ICMP Type";
                IcmpTypeColumn.Name = IcmpTypeColumn.DataPropertyName;
                IcmpTypeColumn.DisplayIndex = 10;

                //protocolColumn.DataSource = Enum.GetValues(typeof(Protocol));

                noMatchesColumn = new DataGridViewTextBoxColumn();
                noMatchesColumn.DataPropertyName = "noMatches";
                noMatchesColumn.HeaderText = "Matched";
                noMatchesColumn.Name = noMatchesColumn.DataPropertyName;
                noMatchesColumn.DisplayIndex = 11;

                ruleDeleteBtnColumn = new DataGridViewButtonColumn();
                ruleDeleteBtnColumn.HeaderText = "Delete?";
                ruleDeleteBtnColumn.Text = "Delete";
                ruleDeleteBtnColumn.Name = "buttonACLRuleDelete";
                ruleDeleteBtnColumn.UseColumnTextForButtonValue = true;
                ruleDeleteBtnColumn.DisplayIndex = 12;

    


                // add columns to the DataGridView
                gui.dataGridViewACL.Columns.Add(deviceColumn);
                gui.dataGridViewACL.Columns.Add(aclRuleTypeColumn);
                gui.dataGridViewACL.Columns.Add(allowanceColumn);
                gui.dataGridViewACL.Columns.Add(sourcePhysicalAddressColumn);
                gui.dataGridViewACL.Columns.Add(destPhysicalAddressColumn);
                gui.dataGridViewACL.Columns.Add(sourceIpColumn);
                gui.dataGridViewACL.Columns.Add(destIpColumn);
                gui.dataGridViewACL.Columns.Add(sourcePortColumn);
                gui.dataGridViewACL.Columns.Add(destPortColumn);
                gui.dataGridViewACL.Columns.Add(protocolColumn);
                gui.dataGridViewACL.Columns.Add(ruleDeleteBtnColumn);
                gui.dataGridViewACL.Columns.Add(IcmpTypeColumn);
                gui.dataGridViewACL.Columns.Add(noMatchesColumn);

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
                    gui.dataGridViewACL.BeginInvoke(() => gui.dataGridViewACL.Refresh());
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
                    ACLRuleDirection direction = (ACLRuleDirection)gui.comboBoxACLDirection.SelectedValue;
                    Protocol protocol = (Protocol)gui.comboBoxACLProtocol.SelectedValue;
                    ushort destinationPort = ushort.Parse(gui.numericUpDownACLDstPort.Text);
                    ushort sourcePort = ushort.Parse(gui.numericUpDownACLSrcPort.Text);


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
                        icmpType = (ICMPType)gui.comboBoxICMPType.SelectedValue;
                    }

                    PacketInfo packetTemplate = new PacketInfo(srcMAC, dstMAC, srcIP, dstIP,  sourcePort, destinationPort, protocol, icmpType);
                    ACLRule rule = new ACLRule(direction, allowance, device, packetTemplate);
                    lock (ACL.AclRulesListLock)
                    {
                        if (gui.dataGridViewACL.InvokeRequired)
                        {
                            gui.dataGridViewACL.BeginInvoke(() => ACL.AclRules.Add(rule));
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

            public static void DeleteAllRules()
            {
                if (MessageBox.Show("Are you sure you want to delete ALL rules / ACEs?", "Remove all rules",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lock (ACL.AclRulesListLock)
                    {
                        if (gui.dataGridViewACL.InvokeRequired)
                        {
                            gui.dataGridViewACL.BeginInvoke(() => ACL.AclRules.Clear());
                        }
                        else
                        {
                            ACL.AclRules.Clear();
                        }
                    }
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
                } else if (e.ColumnIndex == gui.dataGridViewACL.Columns["SourcePhysicalAddress"].Index
                           ||
                           e.ColumnIndex == gui.dataGridViewACL.Columns["DestinationPhysicalAddress"].Index)
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
                } else if (e.ColumnIndex == gui.dataGridViewACL.Columns["SourceIpAddress"].Index
                           ||
                           e.ColumnIndex == gui.dataGridViewACL.Columns["DestinationIpAddress"].Index)
                {
                    if (e.Value == null)
                        return;

                    IPAddress ip = (IPAddress)(e.Value);

                    e.Value = ip.Equals(IPAddress.Any) ? "ANY" : ip.ToString();
                    e.FormattingApplied = true;
                } else if (e.ColumnIndex == gui.dataGridViewACL.Columns["DestinationPort"].Index
                           ||
                           e.ColumnIndex == gui.dataGridViewACL.Columns["SourcePort"].Index)
                {
                    if (e.Value == null)
                        return;

                    ushort prt = (ushort)(e.Value);

                    e.Value = (prt == 0) ? "ANY" : prt.ToString();
                    e.FormattingApplied = true;
                }
                else if (e.ColumnIndex == gui.dataGridViewACL.Columns["IcmpType"].Index)
                {
                    if (e.Value == null)
                        return;

                    var proto = (Protocol)gui.dataGridViewACL.Rows[e.RowIndex].Cells["Protocol"].Value;
                    if (proto != Protocol.ICMP)
                    {
                        e.Value = "";
                    }
                    else
                    {
                        ICMPType iType = (ICMPType)(e.Value);
                        e.Value = (iType == ICMPType.ANY) ? "ANY" : iType.ToString();
                    }

                    e.FormattingApplied = true;
                } else if (e.ColumnIndex == gui.dataGridViewACL.Columns["noMatches"].Index)
                {
                    if (e.Value == null)
                        return;

                    long matches = (long)(e.Value);
                    e.Value = matches.ToString();
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
            private static DataGridViewTextBoxColumn tableMacAddressColumn;
            private static DataGridViewTextBoxColumn tablePortColumn;
            private static DataGridViewTextBoxColumn tableTimerColumn;
            public static void InitNumeric()
            {
                gui.numericUpDownMACAddressTableTimerValue.Value = 30;
                gui.numericUpDownMACAddressTableTimerValue.Minimum = 0;
                gui.numericUpDownMACAddressTableTimerValue.Maximum = int.MaxValue;
            }
            public static void Init()
            {
                gui.dataGridViewMACAddressTable.DataSource = null;
                gui.dataGridViewMACAddressTable.AllowUserToAddRows = false;
                gui.dataGridViewMACAddressTable.AllowUserToDeleteRows = false;
                gui.dataGridViewMACAddressTable.ReadOnly = true;
                gui.dataGridViewMACAddressTable.EditMode = DataGridViewEditMode.EditProgrammatically;
                gui.dataGridViewMACAddressTable.AutoGenerateColumns = false;
                gui.dataGridViewMACAddressTable.
                Refresh();

                tableMacAddressColumn = new DataGridViewTextBoxColumn();
                tableMacAddressColumn.DataPropertyName = "MacAddress";
                tableMacAddressColumn.Name = tableMacAddressColumn.DataPropertyName;
                tableMacAddressColumn.HeaderText = "MAC Address";
                tableMacAddressColumn.DisplayIndex = 0;
                tableMacAddressColumn.Width = 180;
                tableMacAddressColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                tablePortColumn = new DataGridViewTextBoxColumn();
                tablePortColumn.DataPropertyName = "Port";
                tablePortColumn.Name = tablePortColumn.DataPropertyName;
                tablePortColumn.HeaderText = "Port Number";
                tablePortColumn.DisplayIndex = 1;
                tablePortColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                tableTimerColumn = new DataGridViewTextBoxColumn();
                tableTimerColumn.DataPropertyName = "Timer";
                tableTimerColumn.Name = tableTimerColumn.DataPropertyName;
                tableTimerColumn.HeaderText = "Timer [s]";
                tableTimerColumn.DisplayIndex = 2;
                tableTimerColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                gui.dataGridViewMACAddressTable.Columns.Add(tableMacAddressColumn);
                gui.dataGridViewMACAddressTable.Columns.Add(tablePortColumn);
                gui.dataGridViewMACAddressTable.Columns.Add(tableTimerColumn);

                // Set up the data binding between the ObservableCollection and the DataGridView
                gui.dataGridViewMACAddressTable.DataSource = MACAddressTable.MacAddressTable;
                //gui.dataGridViewMACAddressTable.DataMember = string.Empty;

                // Add an event handler to the CellFormatting event of the DataGridView
                // This will allow us to format the MAC Address column
                gui.dataGridViewMACAddressTable.CellFormatting += onCellFormating;

                Refresh();
            }

            public static void AddRecord(EndDeviceRecord rec)
            {
                if (gui.dataGridViewMACAddressTable.InvokeRequired)
                    {
                        gui.dataGridViewMACAddressTable.Invoke(new Action(() =>
                        {
                            lock (MACAddressTable.MacAddressTableLock)
                            {
                                // This code will be executed on the UI thread
                                MACAddressTable.MacAddressTable.Add(rec);
                            }
                        }));
                    }
                    else
                    {
                    // This code is already running on the UI thread
                        lock (MACAddressTable.MacAddressTableLock)
                        {
                            MACAddressTable.MacAddressTable.Add(rec);
                        }
                    }
            }

            public static void DeleteAll()
            {
                if (gui.dataGridViewMACAddressTable.InvokeRequired)
                    {
                        gui.dataGridViewMACAddressTable.BeginInvoke(() =>
                            {
                                lock (MACAddressTable.MacAddressTableLock)
                                {
                                    MACAddressTable.MacAddressTable.Clear();
                                }
                            }
                        );
                    }
                    else
                    {
                        lock (MACAddressTable.MacAddressTableLock)
                        {
                            MACAddressTable.MacAddressTable.Clear();

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
                    gui.dataGridViewMACAddressTable.BeginInvoke(new Action(() =>
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
                if (e.ColumnIndex == gui.dataGridViewMACAddressTable.Columns["MacAddress"].Index && e.Value != null)
                {
                    // Get the MAC address value from the EndDeviceRecord object
                    PhysicalAddress macAddress = PhysicalAddress.Parse(e.Value.ToString()); // TODO Unable to cast pri clear tabulke aj pri vytiahnuti kabla a vypnuti a azanuti SW

                    // Format the MAC address using the formatMAC function
                    string formattedMACAddress = EndDeviceRecord.FormatMAC(macAddress);

                    // Set the formatted value in the cell
                    e.Value = formattedMACAddress;
                    e.FormattingApplied = true;
                } else if (e.ColumnIndex == gui.dataGridViewMACAddressTable.Columns["Timer"].Index && e.Value != null)
                {
                    int timerval = int.Parse(e.Value.ToString());
                    int newVal = 0;

                    lock (MACAddressTable.TimerLock)
                    {
                      newVal = MACAddressTable.timerValue - timerval;
                    }

                    e.Value = newVal.ToString();
                    e.FormattingApplied = true;

                } 
            }
        }
    }
}