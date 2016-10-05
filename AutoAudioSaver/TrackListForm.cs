using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using AutoAudioSaver.Properties;
using System.Xml.Serialization;
using System.Xml;
using System.Threading;


namespace AutoAudioSaver
{
    public partial class TrackListForm : Form
    {
        private List<Audio> trackList;
        private WebClient downloader;
        private WebClient multipleDownloader;
        private EventWaitHandle handle;

        public TrackListForm()
        {
            InitializeComponent();
            downloader = new WebClient();
            downloader.DownloadProgressChanged += (_, b) => progressBar.Value = b.ProgressPercentage; 
            downloader.DownloadFileCompleted += (_,__) => progressBar.Value = 0;
            SaveButton.Enabled = Settings.Default.trackListWasSaved;
            folderBrowserDialog.SelectedPath = Settings.Default.downloadingPath;
            DownloadingPath.Text = folderBrowserDialog.SelectedPath;
        }
        private void TrackListLoad(object sender, EventArgs e)
        {
            if (!Settings.Default.auth)
                new AuthenticationForm().Show();
            Task.Factory.StartNew(() =>           
            {
                while (!Settings.Default.auth)
                    Task.Delay(500);
                try
                {
                    trackList = GetTrackList();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ошибка: " + exception.Message);
                    this.Close();
                    Environment.Exit(0);
                }
            }).Wait();
            trackListBox.Items.AddRange(trackList.Select(audio => audio.artist + " - " + audio.title).ToArray());
        }
        private List<Audio> GetTrackList()
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/audio.get?owner_id=" +
                                                    Settings.Default.id +
                                                    "need_user=0&count=200&access_token=" +
                                                    Settings.Default.token);
            string result;
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = HttpUtility.HtmlDecode(reader.ReadToEnd());
            }
            return JsonToList(result);
        }
        private void trackListBox_DoubleClick(object sender, EventArgs e)
        {
            SaveTrack(trackList[trackListBox.SelectedIndex]);
        }
        private List<Audio> JsonToList(String json)
        {
            JToken token = JToken.Parse(json);
            return token["response"].Children().Skip(1).Select(c => c.ToObject<Audio>()).ToList();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            var savedTrackList = DeserializeTrackList();
            var trackListToSave = trackList.Take(trackList.FindIndex(item => item.aid == savedTrackList[0].aid)).ToArray();
            if (trackListToSave.Length == 0) MessageBox.Show("Нет новых");
            else
            {
                SaveButton.Enabled = false;
                Task.Factory.StartNew(() => SaveTracks(trackListToSave));
            }
            SerializeTrackList();
        }
        private void SaveTrack(Audio track)
        {
            var fileName = Settings.Default.downloadingPath + DeleteInvalidChars(track.artist + " - " + track.title + ".mp3");            
            try
            {
                downloader.DownloadFileAsync(new Uri(track.url), fileName);
            }
            catch (NotSupportedException) { }
            catch (Exception e) { MessageBox.Show("Ошибка: " + e.Message); }
        }
        private string DeleteInvalidChars(string path)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            foreach (var item in invalidChars)
                path = path.Replace(item.ToString(), String.Empty);
            return path;
        }
        private XmlWriterSettings GetWriteSettings()
        {
            return new XmlWriterSettings
            {
                Encoding = Encoding.Default,
                NewLineOnAttributes = true,
                Indent = true
            };
        }
        private void SerializeTrackList()
        {
            using (var writer = XmlWriter.Create(Resources.XmlTrackListName, GetWriteSettings()))
            {
                new XmlSerializer(typeof(List<Audio>)).Serialize(writer, trackList);
            }
            Settings.Default.trackListWasSaved = true;
            Settings.Default.Save();
        }
        private List<Audio> DeserializeTrackList()
        {
            using (var stream = new FileStream(Resources.XmlTrackListName, FileMode.Open))
            {
                return (List<Audio>)(new XmlSerializer(typeof(List<Audio>)).Deserialize(stream));
            }
        }
        private void SaveTracks(Audio[] tracks)
        {
            multipleDownloader = new WebClient();
            handle = new AutoResetEvent(false);
            multipleDownloader.DownloadFileCompleted += (_, __) => handle.Set();
            int tracksLeft = tracks.Length;
            foreach (var track in tracks)
            {
                SaveButton.Invoke(new Action(() => SaveButton.Text = "Осталось: " + tracksLeft--));
                var fileName = Settings.Default.downloadingPath + DeleteInvalidChars(track.artist + " - " + track.title + ".mp3");
                multipleDownloader.DownloadFileAsync(new Uri(track.url), fileName);
                handle.WaitOne();
            }
            SaveButton.Invoke(new Action(() => 
                {
                    SaveButton.Text = "Скачать новые";
                    SaveButton.Enabled = true;
                }));
        }

        private void ChangePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            DownloadingPath.Text = folderBrowserDialog.SelectedPath;
            Settings.Default.downloadingPath = folderBrowserDialog.SelectedPath;
        }
    }
}
