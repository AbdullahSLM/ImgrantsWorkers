using System;
using System.Windows.Forms;

namespace ImgrantsWorkers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serverName = Settings.getServerName();
            if (serverName == null) {
                Application.Run(new DatabaseConnection());
            }
            else {
                DB.InitialDatabase(serverName);
                Application.Run(new MainForm());
            }
        }
    }
}
