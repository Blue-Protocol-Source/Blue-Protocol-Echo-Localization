using System.Net;
using System.Runtime.InteropServices;

namespace FesteLOC
{
    public partial class Form1 : Form
    {
        public HttpServer HttpServ;
        public Config Cfg = new Config();

        public Form1()
        {
            InitializeComponent();
            Theme.UseImmersiveDarkMode(Handle, true);

            HttpServ = new HttpServer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpServ.Init(Cfg);
        }
    }
}