using System;
using System.Net;
using System.Windows.Forms;

namespace Lab11
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // На некоторых машинах сервер требует TLS 1.2
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
