using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoAudioSaver.Properties;

namespace AutoAudioSaver
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
            if(Settings.Default.Auth) Application.Run(new TrackListForm());
            else Application.Run(new AuthenticationForm());
        }
    }
}
