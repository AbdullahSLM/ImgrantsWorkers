using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;


namespace ImgrantsWorkers
{
    public partial class DatabaseConnection : Form
    {


        public DatabaseConnection()
        {
            InitializeComponent();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => {
                try
                {
                    var serverName = serverNameField.Text.Trim();
                    DB.InitialDatabase(serverName);

                    Console.WriteLine(serverName);
                    Settings.saveSeverName(serverName);
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something wrong happend !!!");
                }
            }).Start();
        }
    }
}
