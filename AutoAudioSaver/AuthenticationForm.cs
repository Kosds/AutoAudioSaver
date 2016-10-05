using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoAudioSaver.Properties;

namespace AutoAudioSaver
{
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();
        }

        private void AuthenticationFormLoad(object sender, EventArgs e)
        {
            AuthenticationBrowser.Navigate(Resources.url);
        }
        private void LoadingCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                SetAuthenticationData();
                this.Close();
            }
            catch (FormatException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void SetAuthenticationData()
        {
            string url = AuthenticationBrowser.Url.ToString();
            if (url.Contains("#"))
            {
                Settings.Default.token = url.Split('#')[1].Split('&')[0].Split('=')[1];
                Settings.Default.id = url.Split('#')[1].Split('&')[2].Split('=')[1];
                Settings.Default.auth = true;
                Settings.Default.Save();
            }
            else throw new FormatException("Ошибка при загрузке");
        }
    }
}
