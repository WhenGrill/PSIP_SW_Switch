namespace PSIP_SW_Switch
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.labelIntName1 = new System.Windows.Forms.Label();
            this.groupBoxSwitchSettings = new System.Windows.Forms.GroupBox();
            this.groupBoxACL = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownACLSrcPort = new System.Windows.Forms.NumericUpDown();
            this.checkBoxACLEnabled = new System.Windows.Forms.CheckBox();
            this.buttonACLDeleteAll = new System.Windows.Forms.Button();
            this.comboBoxICMPType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonACLAddRule = new System.Windows.Forms.Button();
            this.comboBoxACLDirection = new System.Windows.Forms.ComboBox();
            this.comboBoxACLDevice = new System.Windows.Forms.ComboBox();
            this.comboBoxACLAllowance = new System.Windows.Forms.ComboBox();
            this.numericUpDownACLDstPort = new System.Windows.Forms.NumericUpDown();
            this.comboBoxACLProtocol = new System.Windows.Forms.ComboBox();
            this.textBoxACLClientIP = new System.Windows.Forms.TextBox();
            this.textBoxACLSrcMAC = new System.Windows.Forms.TextBox();
            this.textBoxACLSrvIP = new System.Windows.Forms.TextBox();
            this.textBoxACLCLientMAC = new System.Windows.Forms.TextBox();
            this.groupBoxCableStatus = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.labeld2CableConnected = new System.Windows.Forms.Label();
            this.labelInterfaceCableConnected2 = new System.Windows.Forms.Label();
            this.labelInterfaceCableConnected1 = new System.Windows.Forms.Label();
            this.numericUpDownCableDetectionSeconds = new System.Windows.Forms.NumericUpDown();
            this.labeld1CableConnected = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButtonSysLogInt2 = new System.Windows.Forms.RadioButton();
            this.radioButtonSysLogInt1 = new System.Windows.Forms.RadioButton();
            this.checkBoxSysLogEnabled = new System.Windows.Forms.CheckBox();
            this.buttonSysLogConfigure = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSysLogServerPort = new System.Windows.Forms.TextBox();
            this.textBoxSysLogClientPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSysLogClientIP = new System.Windows.Forms.TextBox();
            this.textBoxSysLogServerIP = new System.Windows.Forms.TextBox();
            this.buttonSwitchDisable = new System.Windows.Forms.Button();
            this.buttonSwitchEnable = new System.Windows.Forms.Button();
            this.groupBoxNetInts = new System.Windows.Forms.GroupBox();
            this.labelIntName2 = new System.Windows.Forms.Label();
            this.comboBoxInterfaceList1 = new System.Windows.Forms.ComboBox();
            this.buttonRefreshInterfacesLists = new System.Windows.Forms.Button();
            this.comboBoxInterfaceList2 = new System.Windows.Forms.ComboBox();
            this.dataGridViewMACAddressTable = new System.Windows.Forms.DataGridView();
            this.groupBoxMACAddressTable = new System.Windows.Forms.GroupBox();
            this.buttonSetMACTimer = new System.Windows.Forms.Button();
            this.labelMACAddressTableTimerValue = new System.Windows.Forms.Label();
            this.numericUpDownMACAddressTableTimerValue = new System.Windows.Forms.NumericUpDown();
            this.buttonMACAddressTableClear = new System.Windows.Forms.Button();
            this.dataGridViewInt1Stats = new System.Windows.Forms.DataGridView();
            this.dataGridViewInt2Stats = new System.Windows.Forms.DataGridView();
            this.groupBoxStats = new System.Windows.Forms.GroupBox();
            this.buttonStatResetAll = new System.Windows.Forms.Button();
            this.buttonInt2StatReset = new System.Windows.Forms.Button();
            this.buttonInt1StatReset = new System.Windows.Forms.Button();
            this.labelInt2Stat = new System.Windows.Forms.Label();
            this.labelInt1Stat = new System.Windows.Forms.Label();
            this.dataGridViewACL = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MacTableTimer = new System.Windows.Forms.Timer(this.components);
            this.CableDetectionTimer = new System.Windows.Forms.Timer(this.components);
            this.ACLTableUpdater = new System.Windows.Forms.Timer(this.components);
            this.MACTableGUITimer = new System.Windows.Forms.Timer(this.components);
            this.groupBoxSwitchSettings.SuspendLayout();
            this.groupBoxACL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownACLSrcPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownACLDstPort)).BeginInit();
            this.groupBoxCableStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCableDetectionSeconds)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxNetInts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMACAddressTable)).BeginInit();
            this.groupBoxMACAddressTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMACAddressTableTimerValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInt1Stats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInt2Stats)).BeginInit();
            this.groupBoxStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewACL)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelIntName1
            // 
            this.labelIntName1.AutoSize = true;
            this.labelIntName1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelIntName1.Location = new System.Drawing.Point(6, 34);
            this.labelIntName1.Name = "labelIntName1";
            this.labelIntName1.Size = new System.Drawing.Size(82, 20);
            this.labelIntName1.TabIndex = 3;
            this.labelIntName1.Text = "Interface 1:";
            // 
            // groupBoxSwitchSettings
            // 
            this.groupBoxSwitchSettings.Controls.Add(this.groupBoxACL);
            this.groupBoxSwitchSettings.Controls.Add(this.groupBoxCableStatus);
            this.groupBoxSwitchSettings.Controls.Add(this.groupBox1);
            this.groupBoxSwitchSettings.Controls.Add(this.buttonSwitchDisable);
            this.groupBoxSwitchSettings.Controls.Add(this.buttonSwitchEnable);
            this.groupBoxSwitchSettings.Controls.Add(this.groupBoxNetInts);
            this.groupBoxSwitchSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxSwitchSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSwitchSettings.Name = "groupBoxSwitchSettings";
            this.groupBoxSwitchSettings.Size = new System.Drawing.Size(1531, 307);
            this.groupBoxSwitchSettings.TabIndex = 4;
            this.groupBoxSwitchSettings.TabStop = false;
            this.groupBoxSwitchSettings.Text = "Switch Settings";
            // 
            // groupBoxACL
            // 
            this.groupBoxACL.Controls.Add(this.label17);
            this.groupBoxACL.Controls.Add(this.numericUpDownACLSrcPort);
            this.groupBoxACL.Controls.Add(this.checkBoxACLEnabled);
            this.groupBoxACL.Controls.Add(this.buttonACLDeleteAll);
            this.groupBoxACL.Controls.Add(this.comboBoxICMPType);
            this.groupBoxACL.Controls.Add(this.label15);
            this.groupBoxACL.Controls.Add(this.label14);
            this.groupBoxACL.Controls.Add(this.label13);
            this.groupBoxACL.Controls.Add(this.label12);
            this.groupBoxACL.Controls.Add(this.label11);
            this.groupBoxACL.Controls.Add(this.label10);
            this.groupBoxACL.Controls.Add(this.label9);
            this.groupBoxACL.Controls.Add(this.label8);
            this.groupBoxACL.Controls.Add(this.label7);
            this.groupBoxACL.Controls.Add(this.label6);
            this.groupBoxACL.Controls.Add(this.buttonACLAddRule);
            this.groupBoxACL.Controls.Add(this.comboBoxACLDirection);
            this.groupBoxACL.Controls.Add(this.comboBoxACLDevice);
            this.groupBoxACL.Controls.Add(this.comboBoxACLAllowance);
            this.groupBoxACL.Controls.Add(this.numericUpDownACLDstPort);
            this.groupBoxACL.Controls.Add(this.comboBoxACLProtocol);
            this.groupBoxACL.Controls.Add(this.textBoxACLClientIP);
            this.groupBoxACL.Controls.Add(this.textBoxACLSrcMAC);
            this.groupBoxACL.Controls.Add(this.textBoxACLSrvIP);
            this.groupBoxACL.Controls.Add(this.textBoxACLCLientMAC);
            this.groupBoxACL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxACL.Location = new System.Drawing.Point(6, 182);
            this.groupBoxACL.Name = "groupBoxACL";
            this.groupBoxACL.Size = new System.Drawing.Size(1515, 110);
            this.groupBoxACL.TabIndex = 25;
            this.groupBoxACL.TabStop = false;
            this.groupBoxACL.Text = "ACL/ACE Control";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label17.Location = new System.Drawing.Point(939, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 20);
            this.label17.TabIndex = 38;
            this.label17.Text = "Src. Port:";
            // 
            // numericUpDownACLSrcPort
            // 
            this.numericUpDownACLSrcPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownACLSrcPort.Location = new System.Drawing.Point(1012, 22);
            this.numericUpDownACLSrcPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownACLSrcPort.Name = "numericUpDownACLSrcPort";
            this.numericUpDownACLSrcPort.Size = new System.Drawing.Size(151, 27);
            this.numericUpDownACLSrcPort.TabIndex = 37;
            // 
            // checkBoxACLEnabled
            // 
            this.checkBoxACLEnabled.AutoSize = true;
            this.checkBoxACLEnabled.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxACLEnabled.Location = new System.Drawing.Point(1198, 21);
            this.checkBoxACLEnabled.Name = "checkBoxACLEnabled";
            this.checkBoxACLEnabled.Size = new System.Drawing.Size(211, 27);
            this.checkBoxACLEnabled.TabIndex = 36;
            this.checkBoxACLEnabled.Text = "Enable ACL Processing";
            this.checkBoxACLEnabled.UseVisualStyleBackColor = true;
            this.checkBoxACLEnabled.CheckedChanged += new System.EventHandler(this.checkBoxACLEnabled_CheckedChanged);
            // 
            // buttonACLDeleteAll
            // 
            this.buttonACLDeleteAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonACLDeleteAll.ForeColor = System.Drawing.Color.Red;
            this.buttonACLDeleteAll.Location = new System.Drawing.Point(1418, 17);
            this.buttonACLDeleteAll.Name = "buttonACLDeleteAll";
            this.buttonACLDeleteAll.Size = new System.Drawing.Size(95, 45);
            this.buttonACLDeleteAll.TabIndex = 35;
            this.buttonACLDeleteAll.Text = "Delete ALL";
            this.buttonACLDeleteAll.UseVisualStyleBackColor = true;
            this.buttonACLDeleteAll.Click += new System.EventHandler(this.buttonACLDeleteAll_Click);
            // 
            // comboBoxICMPType
            // 
            this.comboBoxICMPType.FormattingEnabled = true;
            this.comboBoxICMPType.Location = new System.Drawing.Point(1258, 59);
            this.comboBoxICMPType.Name = "comboBoxICMPType";
            this.comboBoxICMPType.Size = new System.Drawing.Size(151, 28);
            this.comboBoxICMPType.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(1171, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 20);
            this.label15.TabIndex = 34;
            this.label15.Text = "ICMP Type:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(245, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 20);
            this.label14.TabIndex = 33;
            this.label14.Text = "Protocol:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(245, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 20);
            this.label13.TabIndex = 32;
            this.label13.Text = "Allowance:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(939, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 20);
            this.label12.TabIndex = 31;
            this.label12.Text = "Dst. Port:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(740, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 20);
            this.label11.TabIndex = 30;
            this.label11.Text = "Src. IP:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(738, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 20);
            this.label10.TabIndex = 29;
            this.label10.Text = "Dst. IP:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(460, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 20);
            this.label9.TabIndex = 28;
            this.label9.Text = "Dst. MAC:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(460, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 20);
            this.label8.TabIndex = 27;
            this.label8.Text = "Src. MAC:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(6, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "Direction:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Interface:";
            // 
            // buttonACLAddRule
            // 
            this.buttonACLAddRule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonACLAddRule.Location = new System.Drawing.Point(1418, 62);
            this.buttonACLAddRule.Name = "buttonACLAddRule";
            this.buttonACLAddRule.Size = new System.Drawing.Size(95, 45);
            this.buttonACLAddRule.TabIndex = 22;
            this.buttonACLAddRule.Text = "Add Rule";
            this.buttonACLAddRule.UseVisualStyleBackColor = true;
            this.buttonACLAddRule.Click += new System.EventHandler(this.buttonACLAddRule_Click);
            // 
            // comboBoxACLDirection
            // 
            this.comboBoxACLDirection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxACLDirection.FormattingEnabled = true;
            this.comboBoxACLDirection.Location = new System.Drawing.Point(91, 60);
            this.comboBoxACLDirection.Name = "comboBoxACLDirection";
            this.comboBoxACLDirection.Size = new System.Drawing.Size(125, 28);
            this.comboBoxACLDirection.TabIndex = 12;
            // 
            // comboBoxACLDevice
            // 
            this.comboBoxACLDevice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxACLDevice.FormattingEnabled = true;
            this.comboBoxACLDevice.Location = new System.Drawing.Point(91, 26);
            this.comboBoxACLDevice.Name = "comboBoxACLDevice";
            this.comboBoxACLDevice.Size = new System.Drawing.Size(125, 28);
            this.comboBoxACLDevice.TabIndex = 13;
            // 
            // comboBoxACLAllowance
            // 
            this.comboBoxACLAllowance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxACLAllowance.FormattingEnabled = true;
            this.comboBoxACLAllowance.Location = new System.Drawing.Point(332, 25);
            this.comboBoxACLAllowance.Name = "comboBoxACLAllowance";
            this.comboBoxACLAllowance.Size = new System.Drawing.Size(104, 28);
            this.comboBoxACLAllowance.TabIndex = 14;
            // 
            // numericUpDownACLDstPort
            // 
            this.numericUpDownACLDstPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownACLDstPort.Location = new System.Drawing.Point(1012, 60);
            this.numericUpDownACLDstPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownACLDstPort.Name = "numericUpDownACLDstPort";
            this.numericUpDownACLDstPort.Size = new System.Drawing.Size(151, 27);
            this.numericUpDownACLDstPort.TabIndex = 20;
            this.numericUpDownACLDstPort.ValueChanged += new System.EventHandler(this.numericUpDownACLDstPort_ValueChanged);
            // 
            // comboBoxACLProtocol
            // 
            this.comboBoxACLProtocol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxACLProtocol.FormattingEnabled = true;
            this.comboBoxACLProtocol.Location = new System.Drawing.Point(332, 60);
            this.comboBoxACLProtocol.Name = "comboBoxACLProtocol";
            this.comboBoxACLProtocol.Size = new System.Drawing.Size(104, 28);
            this.comboBoxACLProtocol.TabIndex = 15;
            this.comboBoxACLProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBoxACLProtocol_SelectedIndexChanged);
            // 
            // textBoxACLClientIP
            // 
            this.textBoxACLClientIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxACLClientIP.Location = new System.Drawing.Point(798, 61);
            this.textBoxACLClientIP.Name = "textBoxACLClientIP";
            this.textBoxACLClientIP.PlaceholderText = "Any IP";
            this.textBoxACLClientIP.Size = new System.Drawing.Size(125, 27);
            this.textBoxACLClientIP.TabIndex = 19;
            // 
            // textBoxACLSrcMAC
            // 
            this.textBoxACLSrcMAC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxACLSrcMAC.Location = new System.Drawing.Point(544, 26);
            this.textBoxACLSrcMAC.Name = "textBoxACLSrcMAC";
            this.textBoxACLSrcMAC.PlaceholderText = "Any MAC";
            this.textBoxACLSrcMAC.Size = new System.Drawing.Size(171, 27);
            this.textBoxACLSrcMAC.TabIndex = 16;
            // 
            // textBoxACLSrvIP
            // 
            this.textBoxACLSrvIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxACLSrvIP.Location = new System.Drawing.Point(798, 25);
            this.textBoxACLSrvIP.Name = "textBoxACLSrvIP";
            this.textBoxACLSrvIP.PlaceholderText = "Any IP";
            this.textBoxACLSrvIP.Size = new System.Drawing.Size(125, 27);
            this.textBoxACLSrvIP.TabIndex = 18;
            // 
            // textBoxACLCLientMAC
            // 
            this.textBoxACLCLientMAC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxACLCLientMAC.Location = new System.Drawing.Point(544, 61);
            this.textBoxACLCLientMAC.Name = "textBoxACLCLientMAC";
            this.textBoxACLCLientMAC.PlaceholderText = "Any MAC";
            this.textBoxACLCLientMAC.Size = new System.Drawing.Size(171, 27);
            this.textBoxACLCLientMAC.TabIndex = 17;
            // 
            // groupBoxCableStatus
            // 
            this.groupBoxCableStatus.Controls.Add(this.label16);
            this.groupBoxCableStatus.Controls.Add(this.labeld2CableConnected);
            this.groupBoxCableStatus.Controls.Add(this.labelInterfaceCableConnected2);
            this.groupBoxCableStatus.Controls.Add(this.labelInterfaceCableConnected1);
            this.groupBoxCableStatus.Controls.Add(this.numericUpDownCableDetectionSeconds);
            this.groupBoxCableStatus.Controls.Add(this.labeld1CableConnected);
            this.groupBoxCableStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxCableStatus.Location = new System.Drawing.Point(746, 22);
            this.groupBoxCableStatus.Name = "groupBoxCableStatus";
            this.groupBoxCableStatus.Size = new System.Drawing.Size(347, 154);
            this.groupBoxCableStatus.TabIndex = 16;
            this.groupBoxCableStatus.TabStop = false;
            this.groupBoxCableStatus.Text = "Cable Status";
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(11, 99);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(172, 43);
            this.label16.TabIndex = 27;
            this.label16.Text = "No. seconds to check cable status:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labeld2CableConnected
            // 
            this.labeld2CableConnected.AutoSize = true;
            this.labeld2CableConnected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labeld2CableConnected.ForeColor = System.Drawing.Color.Red;
            this.labeld2CableConnected.Location = new System.Drawing.Point(199, 71);
            this.labeld2CableConnected.Name = "labeld2CableConnected";
            this.labeld2CableConnected.Size = new System.Drawing.Size(122, 20);
            this.labeld2CableConnected.TabIndex = 26;
            this.labeld2CableConnected.Text = "DISCONNECTED";
            // 
            // labelInterfaceCableConnected2
            // 
            this.labelInterfaceCableConnected2.AutoSize = true;
            this.labelInterfaceCableConnected2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInterfaceCableConnected2.Location = new System.Drawing.Point(11, 71);
            this.labelInterfaceCableConnected2.Name = "labelInterfaceCableConnected2";
            this.labelInterfaceCableConnected2.Size = new System.Drawing.Size(172, 20);
            this.labelInterfaceCableConnected2.TabIndex = 25;
            this.labelInterfaceCableConnected2.Text = "Cable Status Interface 2: ";
            // 
            // labelInterfaceCableConnected1
            // 
            this.labelInterfaceCableConnected1.AutoSize = true;
            this.labelInterfaceCableConnected1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInterfaceCableConnected1.Location = new System.Drawing.Point(11, 26);
            this.labelInterfaceCableConnected1.Name = "labelInterfaceCableConnected1";
            this.labelInterfaceCableConnected1.Size = new System.Drawing.Size(172, 20);
            this.labelInterfaceCableConnected1.TabIndex = 23;
            this.labelInterfaceCableConnected1.Text = "Cable Status Interface 1: ";
            // 
            // numericUpDownCableDetectionSeconds
            // 
            this.numericUpDownCableDetectionSeconds.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownCableDetectionSeconds.Location = new System.Drawing.Point(199, 111);
            this.numericUpDownCableDetectionSeconds.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericUpDownCableDetectionSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCableDetectionSeconds.Name = "numericUpDownCableDetectionSeconds";
            this.numericUpDownCableDetectionSeconds.Size = new System.Drawing.Size(122, 27);
            this.numericUpDownCableDetectionSeconds.TabIndex = 23;
            this.numericUpDownCableDetectionSeconds.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // labeld1CableConnected
            // 
            this.labeld1CableConnected.AutoSize = true;
            this.labeld1CableConnected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labeld1CableConnected.ForeColor = System.Drawing.Color.Red;
            this.labeld1CableConnected.Location = new System.Drawing.Point(199, 26);
            this.labeld1CableConnected.Name = "labeld1CableConnected";
            this.labeld1CableConnected.Size = new System.Drawing.Size(122, 20);
            this.labeld1CableConnected.TabIndex = 24;
            this.labeld1CableConnected.Text = "DISCONNECTED";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.radioButtonSysLogInt2);
            this.groupBox1.Controls.Add(this.radioButtonSysLogInt1);
            this.groupBox1.Controls.Add(this.checkBoxSysLogEnabled);
            this.groupBox1.Controls.Add(this.buttonSysLogConfigure);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxSysLogServerPort);
            this.groupBox1.Controls.Add(this.textBoxSysLogClientPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxSysLogClientIP);
            this.groupBox1.Controls.Add(this.textBoxSysLogServerIP);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(1099, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 154);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SysLog";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(336, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Out Int.";
            // 
            // radioButtonSysLogInt2
            // 
            this.radioButtonSysLogInt2.AutoSize = true;
            this.radioButtonSysLogInt2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonSysLogInt2.Location = new System.Drawing.Point(331, 71);
            this.radioButtonSysLogInt2.Name = "radioButtonSysLogInt2";
            this.radioButtonSysLogInt2.Size = new System.Drawing.Size(62, 24);
            this.radioButtonSysLogInt2.TabIndex = 14;
            this.radioButtonSysLogInt2.Text = "Int. 2";
            this.radioButtonSysLogInt2.UseVisualStyleBackColor = true;
            this.radioButtonSysLogInt2.CheckedChanged += new System.EventHandler(this.radioButtonSysLogInt2_CheckedChanged);
            // 
            // radioButtonSysLogInt1
            // 
            this.radioButtonSysLogInt1.AutoSize = true;
            this.radioButtonSysLogInt1.Checked = true;
            this.radioButtonSysLogInt1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonSysLogInt1.Location = new System.Drawing.Point(331, 49);
            this.radioButtonSysLogInt1.Name = "radioButtonSysLogInt1";
            this.radioButtonSysLogInt1.Size = new System.Drawing.Size(62, 24);
            this.radioButtonSysLogInt1.TabIndex = 15;
            this.radioButtonSysLogInt1.TabStop = true;
            this.radioButtonSysLogInt1.Text = "Int. 1";
            this.radioButtonSysLogInt1.UseVisualStyleBackColor = true;
            this.radioButtonSysLogInt1.CheckedChanged += new System.EventHandler(this.radioButtonSysLogInt1_CheckedChanged);
            // 
            // checkBoxSysLogEnabled
            // 
            this.checkBoxSysLogEnabled.AutoSize = true;
            this.checkBoxSysLogEnabled.Location = new System.Drawing.Point(18, 107);
            this.checkBoxSysLogEnabled.Name = "checkBoxSysLogEnabled";
            this.checkBoxSysLogEnabled.Size = new System.Drawing.Size(141, 27);
            this.checkBoxSysLogEnabled.TabIndex = 8;
            this.checkBoxSysLogEnabled.Text = "Enable SysLog";
            this.checkBoxSysLogEnabled.UseVisualStyleBackColor = true;
            this.checkBoxSysLogEnabled.CheckedChanged += new System.EventHandler(this.checkBoxSysLogEnabled_CheckedChanged);
            // 
            // buttonSysLogConfigure
            // 
            this.buttonSysLogConfigure.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSysLogConfigure.Location = new System.Drawing.Point(336, 109);
            this.buttonSysLogConfigure.Name = "buttonSysLogConfigure";
            this.buttonSysLogConfigure.Size = new System.Drawing.Size(50, 29);
            this.buttonSysLogConfigure.TabIndex = 14;
            this.buttonSysLogConfigure.Text = "Set";
            this.buttonSysLogConfigure.UseVisualStyleBackColor = true;
            this.buttonSysLogConfigure.Click += new System.EventHandler(this.buttonSysLogConfigure_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = ":";
            // 
            // textBoxSysLogServerPort
            // 
            this.textBoxSysLogServerPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSysLogServerPort.Location = new System.Drawing.Point(230, 26);
            this.textBoxSysLogServerPort.Name = "textBoxSysLogServerPort";
            this.textBoxSysLogServerPort.PlaceholderText = "Port";
            this.textBoxSysLogServerPort.Size = new System.Drawing.Size(61, 27);
            this.textBoxSysLogServerPort.TabIndex = 5;
            this.textBoxSysLogServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxSysLogClientPort
            // 
            this.textBoxSysLogClientPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSysLogClientPort.Location = new System.Drawing.Point(230, 59);
            this.textBoxSysLogClientPort.Name = "textBoxSysLogClientPort";
            this.textBoxSysLogClientPort.PlaceholderText = "Port";
            this.textBoxSysLogClientPort.Size = new System.Drawing.Size(61, 27);
            this.textBoxSysLogClientPort.TabIndex = 4;
            this.textBoxSysLogClientPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Client";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server";
            // 
            // textBoxSysLogClientIP
            // 
            this.textBoxSysLogClientIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSysLogClientIP.Location = new System.Drawing.Point(71, 59);
            this.textBoxSysLogClientIP.Name = "textBoxSysLogClientIP";
            this.textBoxSysLogClientIP.PlaceholderText = "IP Address";
            this.textBoxSysLogClientIP.Size = new System.Drawing.Size(133, 27);
            this.textBoxSysLogClientIP.TabIndex = 1;
            this.textBoxSysLogClientIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxSysLogServerIP
            // 
            this.textBoxSysLogServerIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSysLogServerIP.Location = new System.Drawing.Point(71, 26);
            this.textBoxSysLogServerIP.Name = "textBoxSysLogServerIP";
            this.textBoxSysLogServerIP.PlaceholderText = "IP Address";
            this.textBoxSysLogServerIP.Size = new System.Drawing.Size(133, 27);
            this.textBoxSysLogServerIP.TabIndex = 0;
            this.textBoxSysLogServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSwitchDisable
            // 
            this.buttonSwitchDisable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSwitchDisable.ForeColor = System.Drawing.Color.Red;
            this.buttonSwitchDisable.Location = new System.Drawing.Point(613, 114);
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
            this.buttonSwitchEnable.Location = new System.Drawing.Point(613, 33);
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
            this.groupBoxNetInts.Size = new System.Drawing.Size(599, 143);
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
            this.labelIntName2.Size = new System.Drawing.Size(82, 20);
            this.labelIntName2.TabIndex = 6;
            this.labelIntName2.Text = "Interface 2:";
            // 
            // comboBoxInterfaceList1
            // 
            this.comboBoxInterfaceList1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxInterfaceList1.FormattingEnabled = true;
            this.comboBoxInterfaceList1.Location = new System.Drawing.Point(91, 29);
            this.comboBoxInterfaceList1.Name = "comboBoxInterfaceList1";
            this.comboBoxInterfaceList1.Size = new System.Drawing.Size(502, 25);
            this.comboBoxInterfaceList1.TabIndex = 5;
            this.comboBoxInterfaceList1.SelectedValueChanged += new System.EventHandler(this.comboBoxInterfaceList1_SelectedValueChanged);
            // 
            // buttonRefreshInterfacesLists
            // 
            this.buttonRefreshInterfacesLists.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRefreshInterfacesLists.Location = new System.Drawing.Point(91, 103);
            this.buttonRefreshInterfacesLists.Name = "buttonRefreshInterfacesLists";
            this.buttonRefreshInterfacesLists.Size = new System.Drawing.Size(502, 29);
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
            this.comboBoxInterfaceList2.Location = new System.Drawing.Point(91, 66);
            this.comboBoxInterfaceList2.Name = "comboBoxInterfaceList2";
            this.comboBoxInterfaceList2.Size = new System.Drawing.Size(502, 25);
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
            this.dataGridViewMACAddressTable.Size = new System.Drawing.Size(517, 284);
            this.dataGridViewMACAddressTable.TabIndex = 5;
            // 
            // groupBoxMACAddressTable
            // 
            this.groupBoxMACAddressTable.Controls.Add(this.buttonSetMACTimer);
            this.groupBoxMACAddressTable.Controls.Add(this.labelMACAddressTableTimerValue);
            this.groupBoxMACAddressTable.Controls.Add(this.numericUpDownMACAddressTableTimerValue);
            this.groupBoxMACAddressTable.Controls.Add(this.buttonMACAddressTableClear);
            this.groupBoxMACAddressTable.Controls.Add(this.dataGridViewMACAddressTable);
            this.groupBoxMACAddressTable.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxMACAddressTable.Location = new System.Drawing.Point(1000, 325);
            this.groupBoxMACAddressTable.Name = "groupBoxMACAddressTable";
            this.groupBoxMACAddressTable.Size = new System.Drawing.Size(543, 359);
            this.groupBoxMACAddressTable.TabIndex = 6;
            this.groupBoxMACAddressTable.TabStop = false;
            this.groupBoxMACAddressTable.Text = "MAC Address Table";
            // 
            // buttonSetMACTimer
            // 
            this.buttonSetMACTimer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSetMACTimer.Location = new System.Drawing.Point(402, 318);
            this.buttonSetMACTimer.Name = "buttonSetMACTimer";
            this.buttonSetMACTimer.Size = new System.Drawing.Size(135, 29);
            this.buttonSetMACTimer.TabIndex = 16;
            this.buttonSetMACTimer.Text = "Set";
            this.buttonSetMACTimer.UseVisualStyleBackColor = true;
            this.buttonSetMACTimer.Click += new System.EventHandler(this.buttonSetMACTimer_Click);
            // 
            // labelMACAddressTableTimerValue
            // 
            this.labelMACAddressTableTimerValue.AutoSize = true;
            this.labelMACAddressTableTimerValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMACAddressTableTimerValue.Location = new System.Drawing.Point(157, 322);
            this.labelMACAddressTableTimerValue.Name = "labelMACAddressTableTimerValue";
            this.labelMACAddressTableTimerValue.Size = new System.Drawing.Size(94, 20);
            this.labelMACAddressTableTimerValue.TabIndex = 7;
            this.labelMACAddressTableTimerValue.Text = "Timer Value: ";
            // 
            // numericUpDownMACAddressTableTimerValue
            // 
            this.numericUpDownMACAddressTableTimerValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownMACAddressTableTimerValue.Location = new System.Drawing.Point(257, 320);
            this.numericUpDownMACAddressTableTimerValue.Name = "numericUpDownMACAddressTableTimerValue";
            this.numericUpDownMACAddressTableTimerValue.Size = new System.Drawing.Size(139, 27);
            this.numericUpDownMACAddressTableTimerValue.TabIndex = 7;
            this.numericUpDownMACAddressTableTimerValue.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // buttonMACAddressTableClear
            // 
            this.buttonMACAddressTableClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonMACAddressTableClear.Location = new System.Drawing.Point(16, 318);
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
            this.dataGridViewInt1Stats.Size = new System.Drawing.Size(430, 295);
            this.dataGridViewInt1Stats.TabIndex = 7;
            this.dataGridViewInt1Stats.TabStop = false;
            // 
            // dataGridViewInt2Stats
            // 
            this.dataGridViewInt2Stats.AllowUserToAddRows = false;
            this.dataGridViewInt2Stats.AllowUserToDeleteRows = false;
            this.dataGridViewInt2Stats.AllowUserToResizeRows = false;
            this.dataGridViewInt2Stats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInt2Stats.Location = new System.Drawing.Point(543, 48);
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
            this.dataGridViewInt2Stats.Size = new System.Drawing.Size(430, 295);
            this.dataGridViewInt2Stats.TabIndex = 8;
            this.dataGridViewInt2Stats.TabStop = false;
            // 
            // groupBoxStats
            // 
            this.groupBoxStats.Controls.Add(this.buttonStatResetAll);
            this.groupBoxStats.Controls.Add(this.buttonInt2StatReset);
            this.groupBoxStats.Controls.Add(this.buttonInt1StatReset);
            this.groupBoxStats.Controls.Add(this.labelInt2Stat);
            this.groupBoxStats.Controls.Add(this.labelInt1Stat);
            this.groupBoxStats.Controls.Add(this.dataGridViewInt2Stats);
            this.groupBoxStats.Controls.Add(this.dataGridViewInt1Stats);
            this.groupBoxStats.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxStats.Location = new System.Drawing.Point(12, 325);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(982, 359);
            this.groupBoxStats.TabIndex = 9;
            this.groupBoxStats.TabStop = false;
            this.groupBoxStats.Text = "Statistics";
            // 
            // buttonStatResetAll
            // 
            this.buttonStatResetAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonStatResetAll.Location = new System.Drawing.Point(448, 174);
            this.buttonStatResetAll.Name = "buttonStatResetAll";
            this.buttonStatResetAll.Size = new System.Drawing.Size(89, 53);
            this.buttonStatResetAll.TabIndex = 13;
            this.buttonStatResetAll.Text = "Reset All";
            this.buttonStatResetAll.UseVisualStyleBackColor = true;
            this.buttonStatResetAll.Click += new System.EventHandler(this.buttonStatResetAll_Click);
            // 
            // buttonInt2StatReset
            // 
            this.buttonInt2StatReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonInt2StatReset.Location = new System.Drawing.Point(448, 115);
            this.buttonInt2StatReset.Name = "buttonInt2StatReset";
            this.buttonInt2StatReset.Size = new System.Drawing.Size(89, 53);
            this.buttonInt2StatReset.TabIndex = 12;
            this.buttonInt2StatReset.Text = "Reset Stats Int. 2";
            this.buttonInt2StatReset.UseVisualStyleBackColor = true;
            this.buttonInt2StatReset.Click += new System.EventHandler(this.buttonInt2StatReset_Click);
            // 
            // buttonInt1StatReset
            // 
            this.buttonInt1StatReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonInt1StatReset.Location = new System.Drawing.Point(448, 56);
            this.buttonInt1StatReset.Name = "buttonInt1StatReset";
            this.buttonInt1StatReset.Size = new System.Drawing.Size(89, 53);
            this.buttonInt1StatReset.TabIndex = 11;
            this.buttonInt1StatReset.Text = "Reset Stats Int. 1";
            this.buttonInt1StatReset.UseVisualStyleBackColor = true;
            this.buttonInt1StatReset.Click += new System.EventHandler(this.buttonInt1StatReset_Click);
            // 
            // labelInt2Stat
            // 
            this.labelInt2Stat.AutoSize = true;
            this.labelInt2Stat.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInt2Stat.Location = new System.Drawing.Point(543, 26);
            this.labelInt2Stat.Name = "labelInt2Stat";
            this.labelInt2Stat.Size = new System.Drawing.Size(62, 15);
            this.labelInt2Stat.TabIndex = 10;
            this.labelInt2Stat.Text = "Interface 2";
            // 
            // labelInt1Stat
            // 
            this.labelInt1Stat.AutoSize = true;
            this.labelInt1Stat.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInt1Stat.Location = new System.Drawing.Point(12, 26);
            this.labelInt1Stat.Name = "labelInt1Stat";
            this.labelInt1Stat.Size = new System.Drawing.Size(62, 15);
            this.labelInt1Stat.TabIndex = 9;
            this.labelInt1Stat.Text = "Interface 1";
            // 
            // dataGridViewACL
            // 
            this.dataGridViewACL.AllowUserToAddRows = false;
            this.dataGridViewACL.AllowUserToDeleteRows = false;
            this.dataGridViewACL.AllowUserToOrderColumns = true;
            this.dataGridViewACL.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewACL.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewACL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewACL.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewACL.Location = new System.Drawing.Point(6, 26);
            this.dataGridViewACL.Name = "dataGridViewACL";
            this.dataGridViewACL.ReadOnly = true;
            this.dataGridViewACL.RowHeadersWidth = 51;
            this.dataGridViewACL.RowTemplate.Height = 29;
            this.dataGridViewACL.Size = new System.Drawing.Size(1515, 258);
            this.dataGridViewACL.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewACL);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(12, 690);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1531, 290);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ACL Rules";
            // 
            // MacTableTimer
            // 
            this.MacTableTimer.Interval = 1000;
            this.MacTableTimer.Tick += new System.EventHandler(this.MacTableTimer_Tick);
            // 
            // CableDetectionTimer
            // 
            this.CableDetectionTimer.Interval = 800;
            this.CableDetectionTimer.Tick += new System.EventHandler(this.CableDetectionTimer_Tick);
            // 
            // ACLTableUpdater
            // 
            this.ACLTableUpdater.Enabled = true;
            this.ACLTableUpdater.Interval = 1500;
            this.ACLTableUpdater.Tick += new System.EventHandler(this.ACLTableUpdater_Tick);
            // 
            // MACTableGUITimer
            // 
            this.MACTableGUITimer.Interval = 1000;
            this.MACTableGUITimer.Tick += new System.EventHandler(this.MACTableGUITimer_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 992);
            this.Controls.Add(this.groupBoxStats);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxMACAddressTable);
            this.Controls.Add(this.groupBoxSwitchSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "PSIP - Multilayer Software Switch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBoxSwitchSettings.ResumeLayout(false);
            this.groupBoxACL.ResumeLayout(false);
            this.groupBoxACL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownACLSrcPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownACLDstPort)).EndInit();
            this.groupBoxCableStatus.ResumeLayout(false);
            this.groupBoxCableStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCableDetectionSeconds)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewACL)).EndInit();
            this.groupBox2.ResumeLayout(false);
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
        private Button buttonStatResetAll;
        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private TextBox textBoxSysLogServerPort;
        private TextBox textBoxSysLogClientPort;
        private Label label2;
        private Label label1;
        private TextBox textBoxSysLogClientIP;
        private TextBox textBoxSysLogServerIP;
        private CheckBox checkBoxSysLogEnabled;
        private Button buttonSysLogConfigure;
        private Label label5;
        private RadioButton radioButtonSysLogInt2;
        private RadioButton radioButtonSysLogInt1;
        private GroupBox groupBox2;
        public DataGridView dataGridViewACL;
        public System.Windows.Forms.Timer MacTableTimer;
        private System.Windows.Forms.Timer CableDetectionTimer;
        public NumericUpDown numericUpDownMACAddressTableTimerValue;
        private Button buttonSetMACTimer;
        private NumericUpDown numericUpDownCableDetectionSeconds;
        private GroupBox groupBoxACL;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private GroupBox groupBoxCableStatus;
        private Label labelInterfaceCableConnected1;
        private Label labeld1CableConnected;
        public ComboBox comboBoxACLDirection;
        public ComboBox comboBoxACLDevice;
        public ComboBox comboBoxACLAllowance;
        public ComboBox comboBoxACLProtocol;
        public TextBox textBoxACLSrcMAC;
        public TextBox textBoxACLCLientMAC;
        public TextBox textBoxACLSrvIP;
        public TextBox textBoxACLClientIP;
        public NumericUpDown numericUpDownACLDstPort;
        public Button buttonACLAddRule;
        private Label label15;
        public ComboBox comboBoxICMPType;
        private CheckBox checkBoxACLEnabled;
        public Button buttonACLDeleteAll;
        private Label labelInterfaceCableConnected2;
        private Label labeld2CableConnected;
        private Label label16;
        private Label label17;
        public NumericUpDown numericUpDownACLSrcPort;
        public System.Windows.Forms.Timer ACLTableUpdater;
        public System.Windows.Forms.Timer MACTableGUITimer;
    }
}