using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;


namespace ImgrantsWorkers
{
    static public class Settings
    {
        static public string SettingsPath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ImgrantsWorkers\\Settings"
        );

        static public void saveSeverName(string serverName)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
            File.WriteAllText(SettingsPath, serverName);
        }

        static public string getServerName()
        {
            if (File.Exists(SettingsPath))
            {
                return File.ReadAllText(SettingsPath);
            }
            else
            {
                return null;
            }
        }


    }
}
