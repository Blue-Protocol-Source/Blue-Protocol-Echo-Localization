namespace Blue_Protocol_Echo_Localization
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            OverrideDataDirectoryLabel = new Label();
            OverrideDataDirectoryTB = new TextBox();
            OverrideDataDirectoryBrowseBtn = new Button();
            SaveDataDirectoryBrowseBtn = new Button();
            SaveDataDirectoryTB = new TextBox();
            SaveDataDirectoryLabel = new Label();
            LocalizationLabel = new Label();
            LocalizationComboBox = new ComboBox();
            DecryptSaveDataLabel = new Label();
            DecryptSaveDataCheckBox = new CheckBox();
            DebugLogGroupBox = new GroupBox();
            DebugOutput = new TextBox();
            SettingsGroupBox = new GroupBox();
            IPLbl = new Label();
            CheckIPbtn = new Button();
            ApplySettingsBtn = new Button();
            ShowAdvancedSettingsLabel = new Label();
            ShowAdvancedSettingsCheckBox = new CheckBox();
            AdvancedSettingsGroupBox = new GroupBox();
            AESKeyTB = new TextBox();
            AESKeyLabel = new Label();
            SaveDataNoteLabel = new Label();
            SaveIncomingDataCheckBox = new CheckBox();
            SaveIncomingDataLabel = new Label();
            DebugLogGroupBox.SuspendLayout();
            SettingsGroupBox.SuspendLayout();
            AdvancedSettingsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // OverrideDataDirectoryLabel
            // 
            OverrideDataDirectoryLabel.BackColor = Color.Transparent;
            OverrideDataDirectoryLabel.Location = new Point(6, 19);
            OverrideDataDirectoryLabel.Name = "OverrideDataDirectoryLabel";
            OverrideDataDirectoryLabel.Size = new Size(155, 23);
            OverrideDataDirectoryLabel.TabIndex = 0;
            OverrideDataDirectoryLabel.Text = "Override Data Directory :";
            OverrideDataDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // OverrideDataDirectoryTB
            // 
            OverrideDataDirectoryTB.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            OverrideDataDirectoryTB.BorderStyle = BorderStyle.FixedSingle;
            OverrideDataDirectoryTB.Location = new Point(167, 19);
            OverrideDataDirectoryTB.Name = "OverrideDataDirectoryTB";
            OverrideDataDirectoryTB.Size = new Size(397, 23);
            OverrideDataDirectoryTB.TabIndex = 1;
            OverrideDataDirectoryTB.TextChanged += OverrideDataDirectoryTB_TextChanged;
            // 
            // OverrideDataDirectoryBrowseBtn
            // 
            OverrideDataDirectoryBrowseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OverrideDataDirectoryBrowseBtn.Location = new Point(570, 19);
            OverrideDataDirectoryBrowseBtn.Name = "OverrideDataDirectoryBrowseBtn";
            OverrideDataDirectoryBrowseBtn.Size = new Size(50, 23);
            OverrideDataDirectoryBrowseBtn.TabIndex = 2;
            OverrideDataDirectoryBrowseBtn.Text = "...";
            OverrideDataDirectoryBrowseBtn.UseVisualStyleBackColor = true;
            OverrideDataDirectoryBrowseBtn.Click += OverrideDataDirectoryBrowseBtn_Click;
            // 
            // SaveDataDirectoryBrowseBtn
            // 
            SaveDataDirectoryBrowseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SaveDataDirectoryBrowseBtn.Location = new Point(570, 62);
            SaveDataDirectoryBrowseBtn.Name = "SaveDataDirectoryBrowseBtn";
            SaveDataDirectoryBrowseBtn.Size = new Size(50, 23);
            SaveDataDirectoryBrowseBtn.TabIndex = 5;
            SaveDataDirectoryBrowseBtn.Text = "...";
            SaveDataDirectoryBrowseBtn.UseVisualStyleBackColor = true;
            SaveDataDirectoryBrowseBtn.Click += SaveDataDirectoryBrowseBtn_Click;
            // 
            // SaveDataDirectoryTB
            // 
            SaveDataDirectoryTB.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SaveDataDirectoryTB.BorderStyle = BorderStyle.FixedSingle;
            SaveDataDirectoryTB.Location = new Point(167, 62);
            SaveDataDirectoryTB.Name = "SaveDataDirectoryTB";
            SaveDataDirectoryTB.Size = new Size(397, 23);
            SaveDataDirectoryTB.TabIndex = 4;
            SaveDataDirectoryTB.TextChanged += SaveDataDirectoryTB_TextChanged;
            // 
            // SaveDataDirectoryLabel
            // 
            SaveDataDirectoryLabel.BackColor = Color.Transparent;
            SaveDataDirectoryLabel.Location = new Point(6, 62);
            SaveDataDirectoryLabel.Name = "SaveDataDirectoryLabel";
            SaveDataDirectoryLabel.Size = new Size(155, 23);
            SaveDataDirectoryLabel.TabIndex = 3;
            SaveDataDirectoryLabel.Text = "Saved Data Directory :";
            SaveDataDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LocalizationLabel
            // 
            LocalizationLabel.Location = new Point(6, 156);
            LocalizationLabel.Name = "LocalizationLabel";
            LocalizationLabel.Size = new Size(155, 23);
            LocalizationLabel.TabIndex = 9;
            LocalizationLabel.Text = "Localization :";
            LocalizationLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LocalizationComboBox
            // 
            LocalizationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LocalizationComboBox.FormattingEnabled = true;
            LocalizationComboBox.Location = new Point(167, 157);
            LocalizationComboBox.Name = "LocalizationComboBox";
            LocalizationComboBox.Size = new Size(200, 23);
            LocalizationComboBox.TabIndex = 10;
            LocalizationComboBox.SelectedIndexChanged += LocalizationComboBox_SelectedIndexChanged;
            // 
            // DecryptSaveDataLabel
            // 
            DecryptSaveDataLabel.BackColor = Color.Transparent;
            DecryptSaveDataLabel.Location = new Point(6, 45);
            DecryptSaveDataLabel.Name = "DecryptSaveDataLabel";
            DecryptSaveDataLabel.Size = new Size(180, 23);
            DecryptSaveDataLabel.TabIndex = 2;
            DecryptSaveDataLabel.Text = "Decrypt Saved Data :";
            DecryptSaveDataLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DecryptSaveDataCheckBox
            // 
            DecryptSaveDataCheckBox.AutoSize = true;
            DecryptSaveDataCheckBox.BackColor = Color.Transparent;
            DecryptSaveDataCheckBox.Enabled = false;
            DecryptSaveDataCheckBox.Location = new Point(192, 50);
            DecryptSaveDataCheckBox.Name = "DecryptSaveDataCheckBox";
            DecryptSaveDataCheckBox.Size = new Size(15, 14);
            DecryptSaveDataCheckBox.TabIndex = 3;
            DecryptSaveDataCheckBox.UseVisualStyleBackColor = false;
            // 
            // DebugLogGroupBox
            // 
            DebugLogGroupBox.Controls.Add(DebugOutput);
            DebugLogGroupBox.Location = new Point(12, 328);
            DebugLogGroupBox.Name = "DebugLogGroupBox";
            DebugLogGroupBox.Size = new Size(626, 177);
            DebugLogGroupBox.TabIndex = 1;
            DebugLogGroupBox.TabStop = false;
            DebugLogGroupBox.Text = "Debug Log";
            // 
            // DebugOutput
            // 
            DebugOutput.BorderStyle = BorderStyle.FixedSingle;
            DebugOutput.Dock = DockStyle.Fill;
            DebugOutput.Location = new Point(3, 19);
            DebugOutput.MaxLength = int.MaxValue;
            DebugOutput.Multiline = true;
            DebugOutput.Name = "DebugOutput";
            DebugOutput.ReadOnly = true;
            DebugOutput.ScrollBars = ScrollBars.Both;
            DebugOutput.Size = new Size(620, 155);
            DebugOutput.TabIndex = 0;
            DebugOutput.WordWrap = false;
            // 
            // SettingsGroupBox
            // 
            SettingsGroupBox.Controls.Add(IPLbl);
            SettingsGroupBox.Controls.Add(CheckIPbtn);
            SettingsGroupBox.Controls.Add(ApplySettingsBtn);
            SettingsGroupBox.Controls.Add(ShowAdvancedSettingsLabel);
            SettingsGroupBox.Controls.Add(ShowAdvancedSettingsCheckBox);
            SettingsGroupBox.Controls.Add(AdvancedSettingsGroupBox);
            SettingsGroupBox.Controls.Add(SaveDataNoteLabel);
            SettingsGroupBox.Controls.Add(SaveIncomingDataCheckBox);
            SettingsGroupBox.Controls.Add(SaveIncomingDataLabel);
            SettingsGroupBox.Controls.Add(OverrideDataDirectoryTB);
            SettingsGroupBox.Controls.Add(OverrideDataDirectoryLabel);
            SettingsGroupBox.Controls.Add(OverrideDataDirectoryBrowseBtn);
            SettingsGroupBox.Controls.Add(SaveDataDirectoryLabel);
            SettingsGroupBox.Controls.Add(LocalizationComboBox);
            SettingsGroupBox.Controls.Add(SaveDataDirectoryTB);
            SettingsGroupBox.Controls.Add(LocalizationLabel);
            SettingsGroupBox.Controls.Add(SaveDataDirectoryBrowseBtn);
            SettingsGroupBox.Location = new Point(12, 12);
            SettingsGroupBox.Name = "SettingsGroupBox";
            SettingsGroupBox.Size = new Size(626, 310);
            SettingsGroupBox.TabIndex = 0;
            SettingsGroupBox.TabStop = false;
            SettingsGroupBox.Text = "Settings";
            // 
            // IPLbl
            // 
            IPLbl.AutoSize = true;
            IPLbl.Location = new Point(557, 198);
            IPLbl.Name = "IPLbl";
            IPLbl.Size = new Size(38, 15);
            IPLbl.TabIndex = 16;
            IPLbl.Text = "label1";
            // 
            // CheckIPbtn
            // 
            CheckIPbtn.Location = new Point(461, 194);
            CheckIPbtn.Name = "CheckIPbtn";
            CheckIPbtn.Size = new Size(90, 23);
            CheckIPbtn.TabIndex = 15;
            CheckIPbtn.Text = "Check Region";
            CheckIPbtn.UseVisualStyleBackColor = true;
            CheckIPbtn.Click += CheckIPbtn_Click;
            // 
            // ApplySettingsBtn
            // 
            ApplySettingsBtn.Location = new Point(461, 157);
            ApplySettingsBtn.Name = "ApplySettingsBtn";
            ApplySettingsBtn.Size = new Size(153, 23);
            ApplySettingsBtn.TabIndex = 14;
            ApplySettingsBtn.Text = "[DEBUG] Apply Settings";
            ApplySettingsBtn.UseVisualStyleBackColor = true;
            ApplySettingsBtn.Click += ApplySettingsBtn_Click;
            // 
            // ShowAdvancedSettingsLabel
            // 
            ShowAdvancedSettingsLabel.BackColor = Color.Transparent;
            ShowAdvancedSettingsLabel.Location = new Point(6, 189);
            ShowAdvancedSettingsLabel.Name = "ShowAdvancedSettingsLabel";
            ShowAdvancedSettingsLabel.Size = new Size(155, 23);
            ShowAdvancedSettingsLabel.TabIndex = 11;
            ShowAdvancedSettingsLabel.Text = "Show Advanced Settings :";
            ShowAdvancedSettingsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ShowAdvancedSettingsCheckBox
            // 
            ShowAdvancedSettingsCheckBox.AutoSize = true;
            ShowAdvancedSettingsCheckBox.BackColor = Color.Transparent;
            ShowAdvancedSettingsCheckBox.Location = new Point(167, 194);
            ShowAdvancedSettingsCheckBox.Name = "ShowAdvancedSettingsCheckBox";
            ShowAdvancedSettingsCheckBox.Size = new Size(15, 14);
            ShowAdvancedSettingsCheckBox.TabIndex = 12;
            ShowAdvancedSettingsCheckBox.UseVisualStyleBackColor = false;
            ShowAdvancedSettingsCheckBox.CheckedChanged += ShowAdvancedSettingsCheckBox_CheckedChanged;
            // 
            // AdvancedSettingsGroupBox
            // 
            AdvancedSettingsGroupBox.Controls.Add(AESKeyTB);
            AdvancedSettingsGroupBox.Controls.Add(DecryptSaveDataLabel);
            AdvancedSettingsGroupBox.Controls.Add(DecryptSaveDataCheckBox);
            AdvancedSettingsGroupBox.Controls.Add(AESKeyLabel);
            AdvancedSettingsGroupBox.Location = new Point(6, 215);
            AdvancedSettingsGroupBox.Name = "AdvancedSettingsGroupBox";
            AdvancedSettingsGroupBox.Size = new Size(614, 80);
            AdvancedSettingsGroupBox.TabIndex = 13;
            AdvancedSettingsGroupBox.TabStop = false;
            AdvancedSettingsGroupBox.Text = "Advanced Settings";
            AdvancedSettingsGroupBox.Visible = false;
            // 
            // AESKeyTB
            // 
            AESKeyTB.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AESKeyTB.BorderStyle = BorderStyle.FixedSingle;
            AESKeyTB.Location = new Point(167, 19);
            AESKeyTB.Name = "AESKeyTB";
            AESKeyTB.Size = new Size(441, 23);
            AESKeyTB.TabIndex = 1;
            // 
            // AESKeyLabel
            // 
            AESKeyLabel.BackColor = Color.Transparent;
            AESKeyLabel.Location = new Point(6, 19);
            AESKeyLabel.Name = "AESKeyLabel";
            AESKeyLabel.Size = new Size(155, 23);
            AESKeyLabel.TabIndex = 0;
            AESKeyLabel.Text = "Decryption AES Key :";
            AESKeyLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SaveDataNoteLabel
            // 
            SaveDataNoteLabel.BackColor = Color.Transparent;
            SaveDataNoteLabel.Location = new Point(6, 111);
            SaveDataNoteLabel.Name = "SaveDataNoteLabel";
            SaveDataNoteLabel.Size = new Size(614, 43);
            SaveDataNoteLabel.TabIndex = 8;
            SaveDataNoteLabel.Text = "Note: Data is stored into folders named based on when the data was last updated on the server side.\r\nNot all data is updated at the same time.";
            SaveDataNoteLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SaveIncomingDataCheckBox
            // 
            SaveIncomingDataCheckBox.AutoSize = true;
            SaveIncomingDataCheckBox.BackColor = Color.Transparent;
            SaveIncomingDataCheckBox.Enabled = false;
            SaveIncomingDataCheckBox.Location = new Point(192, 93);
            SaveIncomingDataCheckBox.Name = "SaveIncomingDataCheckBox";
            SaveIncomingDataCheckBox.Size = new Size(15, 14);
            SaveIncomingDataCheckBox.TabIndex = 7;
            SaveIncomingDataCheckBox.UseVisualStyleBackColor = false;
            SaveIncomingDataCheckBox.CheckedChanged += SaveIncomingDataCheckBox_CheckedChanged;
            // 
            // SaveIncomingDataLabel
            // 
            SaveIncomingDataLabel.BackColor = Color.Transparent;
            SaveIncomingDataLabel.Location = new Point(6, 88);
            SaveIncomingDataLabel.Name = "SaveIncomingDataLabel";
            SaveIncomingDataLabel.Size = new Size(180, 23);
            SaveIncomingDataLabel.TabIndex = 6;
            SaveIncomingDataLabel.Text = "Save Incoming Data To File :";
            SaveIncomingDataLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 517);
            Controls.Add(SettingsGroupBox);
            Controls.Add(DebugLogGroupBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Echo Localization - Blue Protocol Server Data Translator";
            Load += MainForm_Load;
            DebugLogGroupBox.ResumeLayout(false);
            DebugLogGroupBox.PerformLayout();
            SettingsGroupBox.ResumeLayout(false);
            SettingsGroupBox.PerformLayout();
            AdvancedSettingsGroupBox.ResumeLayout(false);
            AdvancedSettingsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label OverrideDataDirectoryLabel;
        private TextBox OverrideDataDirectoryTB;
        private Button OverrideDataDirectoryBrowseBtn;
        private Button SaveDataDirectoryBrowseBtn;
        private TextBox SaveDataDirectoryTB;
        private Label SaveDataDirectoryLabel;
        private Label LocalizationLabel;
        private ComboBox LocalizationComboBox;
        private Label DecryptSaveDataLabel;
        private CheckBox DecryptSaveDataCheckBox;
        private GroupBox DebugLogGroupBox;
        private TextBox DebugOutput;
        private GroupBox SettingsGroupBox;
        private Label SaveDataNoteLabel;
        private CheckBox SaveIncomingDataCheckBox;
        private Label SaveIncomingDataLabel;
        private GroupBox AdvancedSettingsGroupBox;
        private TextBox AESKeyTB;
        private Label AESKeyLabel;
        private CheckBox ShowAdvancedSettingsCheckBox;
        private Label ShowAdvancedSettingsLabel;
        private Button ApplySettingsBtn;
        private Button CheckIPbtn;
        private Label IPLbl;
    }
}