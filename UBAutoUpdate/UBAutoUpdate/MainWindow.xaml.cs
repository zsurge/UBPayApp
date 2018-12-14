using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.ComponentModel;

namespace UBAutoUpdate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] recArgs;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
            {
                recArgs = args;
            }
        }


        //定义全局变量
        WebClient webClient; //下载文件使用
        Stopwatch sw = new Stopwatch(); //用于计算下载速度
        readonly public string tmpPath = Directory.GetCurrentDirectory() + "\\Temp";

        static public bool isRun = false;
        static public string mainAppExe = Directory.GetCurrentDirectory() + "\\UBPayApp.exe";
        static public string gFileName = string.Empty;

        static public string url = string.Empty;


        //创建目录
        static public void CreateDirtory(string path)
        {
            if (!File.Exists(path))
            {
                string[] dirArray = path.Split('\\');
                string temp = string.Empty;
                for (int i = 0; i < dirArray.Length; i++)
                {
                    temp += dirArray[i].Trim() + "\\";
                    if (!Directory.Exists(temp))
                        Directory.CreateDirectory(temp);
                }
            }
        }

        //复制文件;
        public void CopyFile(string sourcePath, string objPath)
        {
            //			char[] split = @"\".ToCharArray();
            if (!Directory.Exists(objPath))
            {
                Directory.CreateDirectory(objPath);
            }
            string[] files = Directory.GetFiles(sourcePath);
            for (int i = 0; i < files.Length; i++)
            {
                string[] childfile = files[i].Split('\\');
                File.Copy(files[i], objPath + @"\" + childfile[childfile.Length - 1], true);
            }
            string[] dirs = Directory.GetDirectories(sourcePath);
            for (int i = 0; i < dirs.Length; i++)
            {
                string[] childdir = dirs[i].Split('\\');
                CopyFile(dirs[i], objPath + @"\" + childdir[childdir.Length - 1]);
            }
        }




        public string GetHttpResp(string url)
        {

            string strResp = string.Empty;

            // Creates an HttpWebRequest with the specified URL. 
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);

            // Sends the HttpWebRequest and waits for the response.	
            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();

            // Gets the stream associated with the response.
            Stream receiveStream = HttpWResp.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, encode);
            Char[] read = new Char[256];

            // Reads 256 characters at a time.    
            int count = readStream.Read(read, 0, 256);

            while (count > 0)
            {
                // Dumps the 256 characters on a string and displays the string to the console.
                String str = new String(read, 0, count);
                strResp += str;
                count = readStream.Read(read, 0, 256);
            }

            // Insert code that uses the response object.
            HttpWResp.Close();

            return strResp;
        }//end GetHttpResp


        //获取文件名
        public string GetFileName(string path)
        {
            try
            {
                string[] dirArray = path.Split('/');

                return dirArray[dirArray.Length - 1];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return "v1.0.1.zip";
        }

        public void DownLoadFile()
        {   
            CreateDirtory(tmpPath);

            gFileName = GetFileName(url);

            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                try
                {
                    Uri URL;
                    // 先判断是否包括http://  
                    if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        URL = new Uri("http://" + url);
                    else
                        URL = new Uri(url);
                    sw.Start();
                    // 开始异步下载  
                    webClient.DownloadFileAsync(URL, gFileName);
                    //webClient.DownloadFile(URL, "1.0.2.zip");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        // The event that will trigger when the WebClient is completed  
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if (e.Cancelled == true)
            {
                //下载未完成  
                MessageBox.Show("下载未完成");
            }
            else
            {
                //解压文件
                string zipFilePath = Directory.GetCurrentDirectory()+ "\\"+ gFileName;
                string unZipDir = tmpPath;
                string err = string.Empty;
                Zip.UnZipFile(zipFilePath, unZipDir, out err);


                //复制文件
                try
                {
                    CopyFile(unZipDir, Directory.GetCurrentDirectory());

                    System.IO.Directory.Delete(tmpPath, true);
                    File.Delete(zipFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                //运行程序
                System.Reflection.Assembly.GetEntryAssembly();               

                isRun = IsMainAppRun();

                if (false == isRun)
                {
                    Process.Start(mainAppExe);
                }
                
                this.Close();
            }
        }

        //判断主应用程序是否正在运行
        private bool IsMainAppRun()
        {            
            bool isRun = false;
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower())
                {
                    isRun = true;
                    //break;
                }
            }
            return isRun;
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                //显示下载速度
                if (textBlockFileInfo.Text != (Convert.ToDouble(e.BytesReceived) / 1024 / sw.Elapsed.TotalSeconds).ToString("0"))
                    textBlockFileInfo.Text = (Convert.ToDouble(e.BytesReceived) / 1024 / sw.Elapsed.TotalSeconds).ToString("0.00") + " kb/s";

                // 进度条  
                if (probar.Value != e.ProgressPercentage)
                    probar.Value = e.ProgressPercentage;

                // 当前比例  
                if (textBlockFileInfo.Text != e.ProgressPercentage.ToString() + "%")
                    textBlockFileInfo.Text = e.ProgressPercentage.ToString() + "%";

                // 下载了多少 还剩余多少  
                textBlockSizeInfo.Text = (Convert.ToDouble(e.BytesReceived) / 1024 / 1024).ToString("0.00") + " Mb" + "  /  " + (Convert.ToDouble(e.TotalBytesToReceive) / 1024 / 1024).ToString("0.00") + " Mb";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            DownLoadFile();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {        
            if (null != recArgs && recArgs.Length > 0)
            {                
                foreach (string str in recArgs)
                    url += str;              
            }

            DownLoadFile();
        }
    }
}
