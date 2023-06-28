using System.Runtime.InteropServices;

namespace FesteLOC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Theme.UseImmersiveDarkMode(Handle, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}