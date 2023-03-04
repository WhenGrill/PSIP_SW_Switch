using SharpPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSIP_SW_Switch
{
    static class GUIController
    {
        public static MainWindow gui = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

        public static class AclGui
        {
            private static DataGridViewComboBoxColumn deviceColumn;
            private static DataGridViewComboBoxColumn aclRuleTypeColumn;
            private static DataGridViewComboBoxColumn allowanceColumn;
            private static DataGridViewComboBoxColumn protocolColumn;

            private static DataGridViewTextBoxColumn sourcePhysicalAddressColumn;
            private static DataGridViewTextBoxColumn destPhysicalAddressColumn;
            private static DataGridViewTextBoxColumn sourceIpColumn;
            private static DataGridViewTextBoxColumn destIpColumn;
            private static DataGridViewTextBoxColumn sourcePortColumn;
            private static DataGridViewTextBoxColumn destPortColumn;

            public static void InitACLDataGridview()
            {
                // create columns for the DataGridView

                deviceColumn = new DataGridViewComboBoxColumn();
                deviceColumn.DataPropertyName = "device";
                deviceColumn.HeaderText = "Interface";
                deviceColumn.Items.AddRange("1", "2", "ANY");

                aclRuleTypeColumn = new DataGridViewComboBoxColumn();
                aclRuleTypeColumn.DataPropertyName = "AclRuleType";
                aclRuleTypeColumn.HeaderText = "Rule Type";
                aclRuleTypeColumn.DataSource = Enum.GetValues(typeof(ACLRuleType));

                allowanceColumn = new DataGridViewComboBoxColumn();
                allowanceColumn.DataPropertyName = "Allowance";
                allowanceColumn.HeaderText = "Allowance";
                allowanceColumn.DataSource = Enum.GetValues(typeof(ACLAllowance));

                sourcePhysicalAddressColumn = new DataGridViewTextBoxColumn();
                sourcePhysicalAddressColumn.DataPropertyName = "packetInfo.SourcePhysicalAddress";
                sourcePhysicalAddressColumn.HeaderText = "Src. MAC";

                destPhysicalAddressColumn = new DataGridViewTextBoxColumn();
                destPhysicalAddressColumn.DataPropertyName = "packetInfo.DestinationPhysicalAddress";
                destPhysicalAddressColumn.HeaderText = "Dst. MAC";


                sourceIpColumn = new DataGridViewTextBoxColumn();
                sourceIpColumn.DataPropertyName = "packetInfo.SourceIpAddress";
                sourceIpColumn.HeaderText = "Src. IP";

                destIpColumn = new DataGridViewTextBoxColumn();
                destIpColumn.DataPropertyName = "packetInfo.DestinationIpAddress";
                destIpColumn.HeaderText = "Dst. IP";

                sourcePortColumn = new DataGridViewTextBoxColumn();
                sourcePortColumn.DataPropertyName = "packetInfo.SourcePort";
                sourcePortColumn.HeaderText = "Src. Port";

                destPortColumn = new DataGridViewTextBoxColumn();
                destPortColumn.DataPropertyName = "packetInfo.DestinationPort";
                destPortColumn.HeaderText = "Dst. Port";

                protocolColumn = new DataGridViewComboBoxColumn();
                protocolColumn.DataPropertyName = "packetInfo.Protocol";
                protocolColumn.HeaderText = "Protocol";
                protocolColumn.DataSource = Enum.GetValues(typeof(Protocol));

                gui.dataGridViewACL.AutoGenerateColumns = true;

                // add columns to the DataGridView
                gui.dataGridViewACL.Columns.Add(deviceColumn);
                gui.dataGridViewACL.Columns.Add(aclRuleTypeColumn);
                gui.dataGridViewACL.Columns.Add(allowanceColumn);
                gui.dataGridViewACL.Columns.Add(sourcePhysicalAddressColumn);
                gui.dataGridViewACL.Columns.Add(destPhysicalAddressColumn);
                gui.dataGridViewACL.Columns.Add(sourceIpColumn);
                gui.dataGridViewACL.Columns.Add(sourcePortColumn);
                gui.dataGridViewACL.Columns.Add(destIpColumn);
                gui.dataGridViewACL.Columns.Add(destPortColumn);
                gui.dataGridViewACL.Columns.Add(protocolColumn);
                

                // Add the columns for each property
                /*gui.dataGridViewACL.Columns.Add("AclRuleType", "ACL Rule Type");
                DataGridViewComboBoxColumn allowColumn = new DataGridViewComboBoxColumn();
                allowColumn.Name = "Allowance";
                allowColumn.HeaderText = "Allowance";
                allowColumn.DataSource = Enum.GetValues(typeof(ACLAllowance));
                gui.dataGridViewACL.Columns.Add(allowColumn);
                DataGridViewComboBoxColumn protocolColumn = new DataGridViewComboBoxColumn();
                protocolColumn.Name = "Protocol";
                protocolColumn.HeaderText = "Protocol";
                protocolColumn.DataSource = Enum.GetValues(typeof(Protocol));
                gui.dataGridViewACL.Columns.Add(protocolColumn);
                gui.dataGridViewACL.Columns.Add("device", "Device");
                gui.dataGridViewACL.Columns.Add("packetInfo.SourceIpAddress", "Source IP Address");
                gui.dataGridViewACL.Columns.Add("packetInfo.DestinationIpAddress", "Destination IP Address");
                gui.dataGridViewACL.Columns.Add("packetInfo.SourcePhysicalAddress", "Source Physical Address");
                gui.dataGridViewACL.Columns.Add("packetInfo.DestinationPhysicalAddress", "Destination Physical Address");
                gui.dataGridViewACL.Columns.Add("packetInfo.SourcePort", "Source Port");
                gui.dataGridViewACL.Columns.Add("packetInfo.DestinationPort", "Destination Port");*/

                gui.dataGridViewACL.DataSource = ACL.AclRules;

                // Enable row header drag-and-drop to allow the user to reorder rows
                gui.dataGridViewACL.AllowUserToOrderColumns = true;
                gui.dataGridViewACL.AllowUserToAddRows = true;
                gui.dataGridViewACL.AllowUserToDeleteRows = true; // TODO Event to handle delete

                gui.dataGridViewACL.CellBeginEdit += dataGridViewACL_onCellBeginEdit;
                //gui.dataGridViewACL.RowsAdded += dataGridViewACL_onRowsAdded;
                gui.dataGridViewACL.CellValidating += dataGridViewACL_onCellValidating;
                gui.dataGridViewACL.RowsRemoved += dataGridViewACL_onRowsRemoved;
                gui.dataGridViewACL.RowHeaderMouseClick += dataGridViewACL_onRowHeaderMouseClick;

                ACLRule testRule = new ACLRule(ACLRuleType.INBOUND, new PacketInfo(), null);
                ACLRule testRule1 = new ACLRule(ACLRuleType.INBOUND, new PacketInfo(), null);
                ACLRule testRule2 = new ACLRule(ACLRuleType.INBOUND, new PacketInfo(), null);
                ACL.AclRules.Add(testRule);
                ACL.AclRules.Add(testRule1);
                ACL.AclRules.Add(testRule2);

            }

            #region DataGridViewACL_Events
            private static void dataGridViewACL_onCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
            {
                if (e.ColumnIndex == sourcePortColumn.Index || e.ColumnIndex == destPortColumn.Index)
                {
                    var row = gui.dataGridViewACL.Rows[e.RowIndex];
                    var protocol = (Protocol)row.Cells[protocolColumn.Index].Value;
                    if (protocol == Protocol.ICMP)
                    {
                        e.Cancel = true;
                        gui.dataGridViewACL.CurrentCell = null;
                        gui.dataGridViewACL.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    }
                    else
                    {
                        gui.dataGridViewACL.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                    }
                }
            }

            /*private static void dataGridViewACL_onRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
            {
                DataGridViewRow row = gui.dataGridViewACL.Rows[e.RowIndex];
                ACLRuleType aclRuleType = (ACLRuleType)row.Cells["AclRuleType"].Value;
                ACLAllowance allowance = (ACLAllowance)row.Cells["Allowance"].Value;
                ILiveDevice? device = null;

                switch (row.Cells["Device"].Value.ToString())
                {
                    case "1":
                        device = InterfaceController.d1;
                        break;
                    case "2":
                        device = InterfaceController.d2;
                        break;
                    case "ANY":
                        device = null;
                        break;
                }

                PacketInfo packetInfo = new PacketInfo
                {
                    SourceIpAddress = (IPAddress?)row.Cells["SourceIpAddress"].Value,
                    DestinationIpAddress = (IPAddress?)row.Cells["DestinationIpAddress"].Value,
                    SourcePhysicalAddress = (PhysicalAddress?)row.Cells["SourcePhysicalAddress"].Value,
                    DestinationPhysicalAddress = (PhysicalAddress?)row.Cells["DestinationPhysicalAddress"].Value,
                    SourcePort = (int?)row.Cells["SourcePort"].Value,
                    DestinationPort = (int?)row.Cells["DestinationPort"].Value,
                    Protocol = (Protocol?)row.Cells["Protocol"].Value
                };
                ACLRule aclRule = new ACLRule(aclRuleType, packetInfo, device);

                lock (ACL.AclRulesListLock)
                {
                    // TODO check ci je to tu potrebne
                    ACL.AclRules.Add(aclRule);
                    gui.dataGridViewACL.DataSource = null;
                    gui.dataGridViewACL.DataSource = ACL.AclRules;
                }
            }*/

            private static void dataGridViewACL_onCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
            {
                DataGridViewCell cell = gui.dataGridViewACL.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Check if the column is AclRuleType
                if (cell.OwningColumn.Name == "AclRuleType")
                {
                    // Check if the value is null or empty
                    if (string.IsNullOrEmpty(e.FormattedValue?.ToString()))
                    {
                        e.Cancel = true;
                        gui.dataGridViewACL.Rows[e.RowIndex].ErrorText = "Rule Type cannot be empty";
                    }
                    else
                    {
                        gui.dataGridViewACL.Rows[e.RowIndex].ErrorText = "";
                    }
                }
                else if (cell.OwningColumn.Name == "Allowance")
                {
                    // Check if the value is null or empty
                    if (string.IsNullOrEmpty(e.FormattedValue?.ToString()))
                    {
                        e.Cancel = true;
                        gui.dataGridViewACL.Rows[e.RowIndex].ErrorText = "Allowance cannot be empty";
                    }
                    else
                    {
                        gui.dataGridViewACL.Rows[e.RowIndex].ErrorText = "";
                    }
                }
            }

            private static void dataGridViewACL_onRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
            {
                var s = (DataGridView)sender;
                s.Refresh();
                // Get the removed row
                DataGridViewRow row = gui.dataGridViewACL.Rows[e.RowIndex];

                // Get the corresponding ACLRule object
                ACLRule aclRule = (ACLRule)row.DataBoundItem;

                // Remove the ACLRule object from the bound list
                lock (ACL.AclRulesListLock)
                {
                    // TODO check if fine
                    ACL.AclRules.Remove(aclRule);
                    gui.dataGridViewACL.DataSource = null;
                    gui.dataGridViewACL.DataSource = ACL.AclRules;
                }
            }

            [Description("Handle the RowHeaderMouseClick event to update the binding list when rows are reordered")]
            private static void dataGridViewACL_onRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
            {
                // Get the selected ACLRule object
                var item = ACL.AclRules[e.RowIndex];

                lock (ACL.AclRulesListLock)
                {
                    // Remove it from the binding list
                    ACL.AclRules.RemoveAt(e.RowIndex);

                    // Insert it at the new position
                    ACL.AclRules.Insert(e.RowIndex, item);

                    // Rebind the DataGridView to the updated binding list
                    gui.dataGridViewACL.DataSource = null;
                    gui.dataGridViewACL.DataSource = ACL.AclRules;
                }
            }

            #endregion

        }

        

    }
}
