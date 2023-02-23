namespace PSIP_SW_Switch
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelIntName1 = new System.Windows.Forms.Label();
            this.groupBoxSwitchSettings = new System.Windows.Forms.GroupBox();
            this.buttonSwitchDisable = new System.Windows.Forms.Button();
            this.buttonSwitchEnable = new System.Windows.Forms.Button();
            this.groupBoxNetInts = new System.Windows.Forms.GroupBox();
            this.labelIntName2 = new System.Windows.Forms.Label();
            this.comboBoxInterfaceList1 = new System.Windows.Forms.ComboBox();
            this.buttonRefreshInterfacesLists = new System.Windows.Forms.Button();
            this.comboBoxInterfaceList2 = new System.Windows.Forms.ComboBox();
            this.dataGridViewMACAddressTable = new System.Windows.Forms.DataGridView();
            this.groupBoxMACAddressTable = new System.Windows.Forms.GroupBox();
            this.labelMACAddressTableTimerValue = new System.Windows.Forms.Label();
            this.numericUpDownMACAddressTableTimerValue = new System.Windows.Forms.NumericUpDown();
            this.buttonMACAddressTableClear = new System.Windows.Forms.Button();
            this.dataGridViewInt1Stats = new System.Windows.Forms.DataGridView();
            this.dataGridViewInt2Stats = new System.Windows.Forms.DataGridView();
            this.groupBoxStats = new System.Windows.Forms.GroupBox();
            this.buttonInt2StatReset = new System.Windows.Forms.Button();
            this.buttonInt1StatReset = new System.Windows.Forms.Button();
            this.labelInt2Stat = new System.Windows.Forms.Label();
            this.labelInt1Stat = new System.Windows.Forms.Label();
            this.timerMACAddressTable = new System.Windows.Forms.Timer(this.components);
            this.groupBoxSwitchSettings.SuspendLayout();
            this.groupBoxNetInts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMACAddressTable)).BeginInit();
            this.groupBoxMACAddressTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMACAddressTableTimerValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInt1Stats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInt2Stats)).BeginInit();
            this.groupBoxStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelIntName1
            // 
            this.labelIntName1.AutoSize = true;
            this.labelIntName1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelIntName1.Location = new System.Drawing.Point(6, 34);
            this.labelIntName1.Name = "labelIntName1";
            this.labelIntName1.Size = new System.Drawing.Size(41, 20);
            this.labelIntName1.TabIndex = 3;
            this.labelIntName1.Text = "Int. 1";
            // 
            // groupBoxSwitchSettings
            // 
            this.groupBoxSwitchSettings.Controls.Add(this.buttonSwitchDisable);
            this.groupBoxSwitchSettings.Controls.Add(this.buttonSwitchEnable);
            this.groupBoxSwitchSettings.Controls.Add(this.groupBoxNetInts);
            this.groupBoxSwitchSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxSwitchSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSwitchSettings.Name = "groupBoxSwitchSettings";
            this.groupBoxSwitchSettings.Size = new System.Drawing.Size(522, 187);
            this.groupBoxSwitchSettings.TabIndex = 4;
            this.groupBoxSwitchSettings.TabStop = false;
            this.groupBoxSwitchSettings.Text = "Switch Settings";
            // 
            // buttonSwitchDisable
            // 
            this.buttonSwitchDisable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSwitchDisable.ForeColor = System.Drawing.Color.Red;
            this.buttonSwitchDisable.Location = new System.Drawing.Point(389, 114);
            this.buttonSwitchDisable.Name = "buttonSwitchDisable";
            this.buttonSwitchDisable.Size = new System.Drawing.Size(127, 62);
            this.buttonSwitchDisable.TabIndex = 9;
            this.buttonSwitchDisable.Text = "Disable / Stop Switch";
            this.buttonSwitchDisable.UseVisualStyleBackColor = true;
            this.buttonSwitchDisable.Click += new System.EventHandler(this.buttonSwitchDisable_Click);
            // 
            // buttonSwitchEnable
            // 
            this.buttonSwitchEnable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSwitchEnable.ForeColor = System.Drawing.Color.ForestGreen;
            this.buttonSwitchEnable.Location = new System.Drawing.Point(389, 46);
            this.buttonSwitchEnable.Name = "buttonSwitchEnable";
            this.buttonSwitchEnable.Size = new System.Drawing.Size(127, 62);
            this.buttonSwitchEnable.TabIndex = 8;
            this.buttonSwitchEnable.Text = "Enable / Start Switch";
            this.buttonSwitchEnable.UseVisualStyleBackColor = true;
            this.buttonSwitchEnable.Click += new System.EventHandler(this.buttonSwitchEnable_Click);
            // 
            // groupBoxNetInts
            // 
            this.groupBoxNetInts.Controls.Add(this.labelIntName2);
            this.groupBoxNetInts.Controls.Add(this.comboBoxInterfaceList1);
            this.groupBoxNetInts.Controls.Add(this.buttonRefreshInterfacesLists);
            this.groupBoxNetInts.Controls.Add(this.labelIntName1);
            this.groupBoxNetInts.Controls.Add(this.comboBoxInterfaceList2);
            this.groupBoxNetInts.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxNetInts.Location = new System.Drawing.Point(6, 33);
            this.groupBoxNetInts.Name = "groupBoxNetInts";
            this.groupBoxNetInts.Size = new System.Drawing.Size(377, 143);
            this.groupBoxNetInts.TabIndex = 6;
            this.groupBoxNetInts.TabStop = false;
            this.groupBoxNetInts.Text = "Network Interfaces";
            // 
            // labelIntName2
            // 
            this.labelIntName2.AutoSize = true;
            this.labelIntName2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelIntName2.Location = new System.Drawing.Point(6, 71);
            this.labelIntName2.Name = "labelIntName2";
            this.labelIntName2.Size = new System.Drawing.Size(41, 20);
            this.labelIntName2.TabIndex = 6;
            this.labelIntName2.Text = "Int. 2";
            // 
            // comboBoxInterfaceList1
            // 
            this.comboBoxInterfaceList1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxInterfaceList1.FormattingEnabled = true;
            this.comboBoxInterfaceList1.Location = new System.Drawing.Point(53, 29);
            this.comboBoxInterfaceList1.Name = "comboBoxInterfaceList1";
            this.comboBoxInterfaceList1.Size = new System.Drawing.Size(318, 25);
            this.comboBoxInterfaceList1.TabIndex = 5;
            this.comboBoxInterfaceList1.SelectedValueChanged += new System.EventHandler(this.comboBoxInterfaceList1_SelectedValueChanged);
            // 
            // buttonRefreshInterfacesLists
            // 
            this.buttonRefreshInterfacesLists.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRefreshInterfacesLists.Location = new System.Drawing.Point(53, 103);
            this.buttonRefreshInterfacesLists.Name = "buttonRefreshInterfacesLists";
            this.buttonRefreshInterfacesLists.Size = new System.Drawing.Size(318, 29);
            this.buttonRefreshInterfacesLists.TabIndex = 7;
            this.buttonRefreshInterfacesLists.Text = "Refresh Interfaces lists";
            this.buttonRefreshInterfacesLists.UseMnemonic = false;
            this.buttonRefreshInterfacesLists.UseVisualStyleBackColor = true;
            this.buttonRefreshInterfacesLists.Click += new System.EventHandler(this.buttonRefreshInterfacesLists_Click);
            // 
            // comboBoxInterfaceList2
            // 
            this.comboBoxInterfaceList2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxInterfaceList2.FormattingEnabled = true;
            this.comboBoxInterfaceList2.Location = new System.Drawing.Point(53, 66);
            this.comboBoxInterfaceList2.Name = "comboBoxInterfaceList2";
            this.comboBoxInterfaceList2.Size = new System.Drawing.Size(318, 25);
            this.comboBoxInterfaceList2.TabIndex = 4;
            this.comboBoxInterfaceList2.SelectedValueChanged += new System.EventHandler(this.comboBoxInterfaceList2_SelectedValueChanged);
            // 
            // dataGridViewMACAddressTable
            // 
            this.dataGridViewMACAddressTable.AllowUserToAddRows = false;
            this.dataGridViewMACAddressTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMACAddressTable.Location = new System.Drawing.Point(16, 28);
            this.dataGridViewMACAddressTable.Name = "dataGridViewMACAddressTable";
            this.dataGridViewMACAddressTable.RowHeadersWidth = 51;
            this.dataGridViewMACAddressTable.RowTemplate.Height = 29;
            this.dataGridViewMACAddressTable.Size = new System.Drawing.Size(494, 234);
            this.dataGridViewMACAddressTable.TabIndex = 5;
            // 
            // groupBoxMACAddressTable
            // 
            this.groupBoxMACAddressTable.Controls.Add(this.labelMACAddressTableTimerValue);
            this.groupBoxMACAddressTable.Controls.Add(this.numericUpDownMACAddressTableTimerValue);
            this.groupBoxMACAddressTable.Controls.Add(this.buttonMACAddressTableClear);
            this.groupBoxMACAddressTable.Controls.Add(this.dataGridViewMACAddressTable);
            this.groupBoxMACAddressTable.Location = new System.Drawing.Point(540, 12);
            this.groupBoxMACAddressTable.Name = "groupBoxMACAddressTable";
            this.groupBoxMACAddressTable.Size = new System.Drawing.Size(520, 307);
            this.groupBoxMACAddressTable.TabIndex = 6;
            this.groupBoxMACAddressTable.TabStop = false;
            this.groupBoxMACAddressTable.Text = "MAC Address Table";
            // 
            // labelMACAddressTableTimerValue
            // 
            this.labelMACAddressTableTimerValue.AutoSize = true;
            this.labelMACAddressTableTimerValue.Location = new System.Drawing.Point(234, 272);
            this.labelMACAddressTableTimerValue.Name = "labelMACAddressTableTimerValue";
            this.labelMACAddressTableTimerValue.Size = new System.Drawing.Size(94, 20);
            this.labelMACAddressTableTimerValue.TabIndex = 7;
            this.labelMACAddressTableTimerValue.Text = "Timer Value: ";
            // 
            // numericUpDownMACAddressTableTimerValue
            // 
            this.numericUpDownMACAddressTableTimerValue.Location = new System.Drawing.Point(360, 270);
            this.numericUpDownMACAddressTableTimerValue.Name = "numericUpDownMACAddressTableTimerValue";
            this.numericUpDownMACAddressTableTimerValue.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownMACAddressTableTimerValue.TabIndex = 7;
            // 
            // buttonMACAddressTableClear
            // 
            this.buttonMACAddressTableClear.Location = new System.Drawing.Point(16, 268);
            this.buttonMACAddressTableClear.Name = "buttonMACAddressTableClear";
            this.buttonMACAddressTableClear.Size = new System.Drawing.Size(135, 29);
            this.buttonMACAddressTableClear.TabIndex = 6;
            this.buttonMACAddressTableClear.Text = "Clear";
            this.buttonMACAddressTableClear.UseVisualStyleBackColor = true;
            this.buttonMACAddressTableClear.Click += new System.EventHandler(this.buttonMACAddressTableClear_Click);
            // 
            // dataGridViewInt1Stats
            // 
            this.dataGridViewInt1Stats.AllowUserToAddRows = false;
            this.dataGridViewInt1Stats.AllowUserToDeleteRows = false;
            this.dataGridViewInt1Stats.AllowUserToResizeRows = false;
            this.dataGridViewInt1Stats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInt1Stats.Location = new System.Drawing.Point(12, 48);
            this.dataGridViewInt1Stats.MultiSelect = false;
            this.dataGridViewInt1Stats.Name = "dataGridViewInt1Stats";
            this.dataGridViewInt1Stats.ReadOnly = true;
            this.dataGridViewInt1Stats.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewInt1Stats.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewInt1Stats.RowTemplate.Height = 29;
            this.dataGridViewInt1Stats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewInt1Stats.ShowCellErrors = false;
            this.dataGridViewInt1Stats.ShowCellToolTips = false;
            this.dataGridViewInt1Stats.ShowEditingIcon = false;
            this.dataGridViewInt1Stats.ShowRowErrors = false;
            this.dataGridViewInt1Stats.Size = new System.Drawing.Size(440, 295);
            this.dataGridViewInt1Stats.TabIndex = 7;
            this.dataGridViewInt1Stats.TabStop = false;
            // 
            // dataGridViewInt2Stats
            // 
            this.dataGridViewInt2Stats.AllowUserToAddRows = false;
            this.dataGridViewInt2Stats.AllowUserToDeleteRows = false;
            this.dataGridViewInt2Stats.AllowUserToResizeRows = false;
            this.dataGridViewInt2Stats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInt2Stats.Location = new System.Drawing.Point(601, 48);
            this.dataGridViewInt2Stats.MultiSelect = false;
            this.dataGridViewInt2Stats.Name = "dataGridViewInt2Stats";
            this.dataGridViewInt2Stats.ReadOnly = true;
            this.dataGridViewInt2Stats.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewInt2Stats.RowTemplate.Height = 29;
            this.dataGridViewInt2Stats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewInt2Stats.ShowCellErrors = false;
            this.dataGridViewInt2Stats.ShowCellToolTips = false;
            this.dataGridViewInt2Stats.ShowEditingIcon = false;
            this.dataGridViewInt2Stats.ShowRowErrors = false;
            this.dataGridViewInt2Stats.Size = new System.Drawing.Size(440, 295);
            this.dataGridViewInt2Stats.TabIndex = 8;
            this.dataGridViewInt2Stats.TabStop = false;
            // 
            // groupBoxStats
            // 
            this.groupBoxStats.Controls.Add(this.buttonInt2StatReset);
            this.groupBoxStats.Controls.Add(this.buttonInt1StatReset);
            this.groupBoxStats.Controls.Add(this.labelInt2Stat);
            this.groupBoxStats.Controls.Add(this.labelInt1Stat);
            this.groupBoxStats.Controls.Add(this.dataGridViewInt2Stats);
            this.groupBoxStats.Controls.Add(this.dataGridViewInt1Stats);
            this.groupBoxStats.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxStats.Location = new System.Drawing.Point(12, 325);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(1053, 359);
            this.groupBoxStats.TabIndex = 9;
            this.groupBoxStats.TabStop = false;
            this.groupBoxStats.Text = "Statistics";
            // 
            // buttonInt2StatReset
            // 
            this.buttonInt2StatReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonInt2StatReset.Location = new System.Drawing.Point(463, 91);
            this.buttonInt2StatReset.Name = "buttonInt2StatReset";
            this.buttonInt2StatReset.Size = new System.Drawing.Size(132, 29);
            this.buttonInt2StatReset.TabIndex = 12;
            this.buttonInt2StatReset.Text = "Reset Stats Int. 2";
            this.buttonInt2StatReset.UseVisualStyleBackColor = true;
            this.buttonInt2StatReset.Click += new System.EventHandler(this.buttonInt2StatReset_Click);
            // 
            // buttonInt1StatReset
            // 
            this.buttonInt1StatReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonInt1StatReset.Location = new System.Drawing.Point(463, 56);
            this.buttonInt1StatReset.Name = "buttonInt1StatReset";
            this.buttonInt1StatReset.Size = new System.Drawing.Size(132, 29);
            this.buttonInt1StatReset.TabIndex = 11;
            this.buttonInt1StatReset.Text = "Reset Stats Int. 1";
            this.buttonInt1StatReset.UseVisualStyleBackColor = true;
            this.buttonInt1StatReset.Click += new System.EventHandler(this.buttonInt1StatReset_Click);
            // 
            // labelInt2Stat
            // 
            this.labelInt2Stat.AutoSize = true;
            this.labelInt2Stat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInt2Stat.Location = new System.Drawing.Point(601, 25);
            this.labelInt2Stat.Name = "labelInt2Stat";
            this.labelInt2Stat.Size = new System.Drawing.Size(79, 20);
            this.labelInt2Stat.TabIndex = 10;
            this.labelInt2Stat.Text = "Interface 2";
            // 
            // labelInt1Stat
            // 
            this.labelInt1Stat.AutoSize = true;
            this.labelInt1Stat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInt1Stat.Location = new System.Drawing.Point(12, 25);
            this.labelInt1Stat.Name = "labelInt1Stat";
            this.labelInt1Stat.Size = new System.Drawing.Size(79, 20);
            this.labelInt1Stat.TabIndex = 9;
            this.labelInt1Stat.Text = "Interface 1";
            // 
            // timerMACAddressTable
            // 
            this.timerMACAddressTable.Tick += new System.EventHandler(this.timerMACAddressTable_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 691);
            this.Controls.Add(this.groupBoxStats);
            this.Controls.Add(this.groupBoxMACAddressTable);
            this.Controls.Add(this.groupBoxSwitchSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PSIP - Multilayer Software Switch";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxSwitchSettings.ResumeLayout(false);
            this.groupBoxNetInts.ResumeLayout(false);
            this.groupBoxNetInts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMACAddressTable)).EndInit();
            this.groupBoxMACAddressTable.ResumeLayout(false);
            this.groupBoxMACAddressTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMACAddressTableTimerValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInt1Stats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInt2Stats)).EndInit();
            this.groupBoxStats.ResumeLayout(false);
            this.groupBoxStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label labelIntName1;
        private GroupBox groupBoxSwitchSettings;
        private GroupBox groupBoxNetInts;
        private Label labelIntName2;
        private Button buttonSwitchEnable;
        private Button buttonRefreshInterfacesLists;
        private Button buttonSwitchDisable;
        private GroupBox groupBoxMACAddressTable;
        private Label labelMACAddressTableTimerValue;
        private NumericUpDown numericUpDownMACAddressTableTimerValue;
        private Button buttonMACAddressTableClear;
        private GroupBox groupBoxStats;
        public ComboBox comboBoxInterfaceList1;
        public ComboBox comboBoxInterfaceList2;
        public DataGridView dataGridViewMACAddressTable;
        public DataGridView dataGridViewInt1Stats;
        public DataGridView dataGridViewInt2Stats;
        private Button buttonInt1StatReset;
        private Label labelInt2Stat;
        private Label labelInt1Stat;
        private Button buttonInt2StatReset;
        public System.Windows.Forms.Timer timerMACAddressTable;
    }
}