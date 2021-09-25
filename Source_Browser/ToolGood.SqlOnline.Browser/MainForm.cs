using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using MiniBlinkPinvoke;
using ToolGood.MyBrowser;
using ToolGood.SqlOnline.Browser.Core;

namespace ToolGood.SqlOnline.Browser
{
    public partial class MainForm : Form
    {
        private BlinkBrowser blinkBrowser1;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            blinkBrowser1 = new BlinkBrowser();
            blinkBrowser1.Dock = DockStyle.Fill;
            this.Controls.Add(this.blinkBrowser1);
            blinkBrowser1.GlobalObjectJs = this;
            this.blinkBrowser1.OnDownloadFile += BlinkBrowser1_OnDownloadFile;

            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(BlinkBrowser.CookiePath + "\\cookies.dat")) {
                File.Delete(BlinkBrowser.CookiePath + "\\cookies.dat");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            var dpixRatio = graphics.DpiX / 96;
            graphics.Dispose();
            blinkBrowser1.ZoomFactor = (float)dpixRatio;
            this.blinkBrowser1.Url = Config.GetUrl();
        }

        private void BlinkBrowser1_OnDownloadFile(string url)
        {
            var cookie = this.blinkBrowser1.GetCookiesFromFile;
            var userAgent = Config.GetUserAgent();

            var fileName = Path.GetFileName(url.Split(new char[] { '?', '#' })[0]);
            if (fileName.Contains(".") == false) { fileName = null; }

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try {
                if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)) {
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    //if (ServicePointManager.Expect100Continue) { ServicePointManager.Expect100Continue = false; }
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = HttpWebRequest.Create(url) as HttpWebRequest;
                    //request.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request.ServicePoint.Expect100Continue = false;
                } else {
                    request = HttpWebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "GET";
                request.UserAgent = userAgent;
                request.Accept = "*/*";
                request.AllowAutoRedirect = true;
                request.AllowWriteStreamBuffering = true;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.UseDefaultCredentials = true;
                request.Headers["Cookie"] = cookie;

                response = (HttpWebResponse)request.GetResponse();

                using (Stream rs = response.GetResponseStream()) {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = "保存下载文件";
                    saveFileDialog.CreatePrompt = false;
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.FileName = fileName;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                        var fs = saveFileDialog.OpenFile();

                        byte[] bytes = new byte[1024];
                        var l = rs.Read(bytes, 0, 1024);
                        while (l > 0) {
                            fs.Write(bytes, 0, l);
                            l = rs.Read(bytes, 0, 1024);
                        }
                        fs.Flush();
                        fs.Close();
                    }
                }
            } finally {
                request = null;
                response = null;
            }

        }

        /// <summary>
        /// 必须在BlinkBrowser 添加相同的方法名
        /// </summary>
        /// <returns></returns>
        [JSFunc("GetMAC")]
        public string GetMAC()
        {
            return Config.GetMAC();
        }

        /// <summary>
        /// 必须在BlinkBrowser 添加相同的方法名
        /// </summary>
        /// <returns></returns>
        [JSFunc("GetMachineCode")]
        public string GetMachineCode()
        {
            return Config.GetMachineCode();
        }


        [JSFunc("OpenUrl")]
        public void OpenUrl(object url)
        {
            var exePath = Config.GetDefaultWebBrowserFilePath();
            System.Diagnostics.Process.Start(exePath, url.ToString());
        }

    }
}
