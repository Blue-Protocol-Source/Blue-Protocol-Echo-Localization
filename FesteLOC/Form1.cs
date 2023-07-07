using System.Net;
using System.Runtime.InteropServices;

namespace FesteLOC
{
    public partial class Form1 : Form
    {
        string TitleText = "";
        public HttpServer HttpServ;
        public Config Cfg = new Config();

        public Form1()
        {
            InitializeComponent();

            TitleText = this.Text;
            Console.SetOut(new TextBoxWriter(DebugOutput));

            HttpServ = new HttpServer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ForeColor = Color.DarkGray;
            this.BackColor = Color.FromArgb(255, 30, 30, 30);
            DarkTheme.ApplyTheme_Dark(this);

            //ResetLocalizationOptions();
            LoadLocalizationOptions();
            SetSelectedLocalization(Cfg.LocalizationStr ?? "en");

            Console.Out.WriteLine("Http Server initializing...");
            HttpServ.Init(Cfg);
            Console.Out.WriteLine("Http Server Initialized");
        }

        private void ResetLocalizationOptions()
        {
            LocalizationComboBox.Items.Clear();
            LocalizationComboBox.Items.Add("Default");
            LocalizationComboBox.SelectedIndex = 0;
        }

        private void SetSelectedLocalization(string name)
        {
            var idx = LocalizationComboBox.Items.IndexOf(name);
            LocalizationComboBox.SelectedIndex = idx;
        }

        private void LoadLocalizationOptions()
        {
            string LocalizationDirectory = Path.Combine(Cfg.OverrideDir, "Localisations");
            if (Directory.Exists(LocalizationDirectory))
            {
                var dirList = Directory.GetDirectories(LocalizationDirectory);
                foreach (var dir in dirList)
                {
                    var name = Path.GetFileName(dir);
                    LocalizationComboBox.Items.Add(Path.GetFileName(name));
                }
            }
        }

        private void OverrideDataDirectoryTB_TextChanged(object sender, EventArgs e)
        {
            LocalizationComboBox.Enabled = OverrideDataDirectoryTB.Text.Length > 0 && Directory.Exists(OverrideDataDirectoryTB.Text);

            LoadLocalizationOptions();
        }

        private void SaveDataDirectoryTB_TextChanged(object sender, EventArgs e)
        {
            SaveIncomingDataCheckBox.Enabled = SaveDataDirectoryTB.Text.Length > 0 && Directory.Exists(SaveDataDirectoryTB.Text);
        }

        private void OverrideDataDirectoryBrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                Description = "Please select the root directory that contains override files (ex: new localizations)...",
                UseDescriptionForTitle = true
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                OverrideDataDirectoryTB.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void SaveDataDirectoryBrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                Description = "Please select the root directory to save server downloaded data to (this should be the same folder every time)...",
                UseDescriptionForTitle = true
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SaveDataDirectoryTB.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void SaveIncomingDataCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DecryptSaveDataCheckBox.Enabled = SaveIncomingDataCheckBox.Checked;
        }

        private void ShowAdvancedSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AdvancedSettingsGroupBox.Visible = ShowAdvancedSettingsCheckBox.Checked;
        }

        private void ApplySettingsBtn_Click(object sender, EventArgs e)
        {
            Cfg.OverrideDir = OverrideDataDirectoryTB.Text;
            Cfg.SaveDataDir = SaveDataDirectoryTB.Text;
            Cfg.SaveServerData = SaveIncomingDataCheckBox.Checked;
            Cfg.LocalizationStr = LocalizationComboBox.Text;
            Cfg.AESKey = AESKeyTB.Text;
            Cfg.SaveDecryptedData = DecryptSaveDataCheckBox.Checked;
        }

        private void LocalizationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}