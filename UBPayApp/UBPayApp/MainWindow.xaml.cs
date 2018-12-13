using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Forms;

namespace UBPayApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IntPtr Handle;
        IniFile ParmIni;

        //托盘显示图标
        private NotifyIcon notifyIcon = null;
        bool shouldClose;
        WindowState ws;


        public MainWindow()
        {
            InitializeComponent();

            InitialTray();
            //保证窗体显示在上方。
            ws = WindowState;

            Var.bAlreadyOpen = false;
            this.frame1.Source = new Uri("Login.xaml", UriKind.Relative);

            ParmIni = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"/Parm.ini");
            Var.windowsType = Convert.ToInt16(ParmIni.IniReadValue("Init", "windows"));
            Var.sound = Convert.ToInt16(ParmIni.IniReadValue("Init", "sound"));
            Var.dispnotes = Convert.ToInt16(ParmIni.IniReadValue("Init", "dispnotes"));

            Var.CaptureScreen_Left = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Left"));
            Var.CaptureScreen_Top = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Top"));
            Var.CaptureScreen_Width = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Width"));
            Var.CaptureScreen_Height = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Height"));
            Var.PayChannel = Convert.ToInt16(ParmIni.IniReadValue("Pay", "PayChannel"));
            Var.gkey = ParmIni.IniReadValue("UserKey", "key");

            if (Var.windowsType == 1)
            {
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                this.WindowState = System.Windows.WindowState.Minimized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!shouldClose)
            {
                e.Cancel = true;
                Hide();
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Var.mLoginCheckEvent.Set();
            Var.mChengeBarCodeEvent.Set();
        }

        /// <summary>
        /// 添加快捷键监听
        /// </summary>
        private void RunHotKey()
        {
            Handle = new WindowInteropHelper(this).Handle;  //获取窗口句柄
            RegisterHotKey();  //注册截图快捷键
            HwndSource source = HwndSource.FromHwnd(Handle);
            if (source != null)
                source.AddHook(WndProc);  //添加Hook，监听窗口事件
        }

        //add p u d f3 f4  for 2018.10.6
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8,
            D = 68,         
            P = 80,
            U = 85,
            F3 = 114,
            F4 = 115
        }

        /// <summary>
        /// 注册快捷键
        /// </summary>
        private void RegisterHotKey()
        {
            //101为快捷键自定义ID，0x0002为Ctrl键, 0x0001为Alt键，或运算符|表同时按住两个键有效，0x41为A键。
            //bool isRegistered = HotKey.RegisterHotKey(Handle, 101, (0x0002 | 0x0001), 0x41);
            bool F3 = HotKey.RegisterHotKey(Handle, 101, 0, 0x72);        // F3
            if (F3 == false)
            {
                System.Windows.MessageBox.Show("截图快捷键F3被占用", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            bool F4 = HotKey.RegisterHotKey(Handle, 102, 0, 0x73);       // F4
            if (F4 == false)
            {
                System.Windows.MessageBox.Show("截图快捷键F4被占用", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            bool F6 = HotKey.RegisterHotKey(Handle, 104, 0, 0x75);       // F4
            if (F6 == false)
            {
                System.Windows.MessageBox.Show("截图快捷键F6被占用", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            bool Ctrl_Alt_P = HotKey.RegisterHotKey(Handle, 105, (0x0002 | 0x0001), 0x50);        // FCtrl_Alt_P
            if (Ctrl_Alt_P == false)
            {
                System.Windows.MessageBox.Show("截图快捷键Ctrl&Alt&P被占用", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// 重写WndProc函数，类型为虚保护，响应窗体消息事件
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg">消息内容</param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled">是否相应完成</param>
        /// <returns></returns>
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;//如果m.Msg的值为0x0312那么表示用户按下了热键
            if (msg == WM_HOTKEY)
            {
                if (Var.bLogin)
                {
                    switch (wParam.ToInt32())
                    {
                        case 104:    //按下的是F6
                            this.WindowState = System.Windows.WindowState.Minimized;
                            break;
                        case 102:    //按下的是F4  
                            Var.PayChannel = 2;
                            if (Var.bAlreadyOpen == false)
                            {
                                Var.bAlreadyOpen = true;
                                PayWindow pay = new PayWindow();
                                pay.ShowDialog();
                                Var.bAlreadyOpen = false;
                            }
                            break;
                        case 101:    //按下的是F3
                            Var.PayChannel = 1;
                            if (Var.bAlreadyOpen == false)
                            {
                                Var.bAlreadyOpen = true;
                                PayWindow pay = new PayWindow();
                                pay.ShowDialog();
                                Var.bAlreadyOpen = false;
                            }
                            break;
                        case 105:
                            if (Var.bAlreadyOpen == false)
                            {
                                Var.bAlreadyOpen = true;

                                SetAmountPosition win = new SetAmountPosition();
                                if (!win.IsVisible)
                                {
                                    win.ShowDialog();
                                }
                                else
                                {
                                    win.Activate();
                                }

                                Var.bAlreadyOpen = false;
                            }
                            break;
                    }
                }
            }

            return IntPtr.Zero;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RunHotKey();  //注册并监听HotKey
        }

        private void InitialTray()
        {
            //设置托盘的各个属性
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "UB云支付平台运行中...";
            notifyIcon.Text = "UB云支付平台";
            //notifyIcon.Icon = new System.Drawing.Icon("../../Resources/ub.ico");
            notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000);
            //notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);
            notifyIcon.MouseDoubleClick += OnNotifyIconDoubleClick;


            //打开菜单项
            MenuItem open = new MenuItem("Open");
            open.Click += new EventHandler(Show);
            //退出菜单项
            MenuItem exit = new MenuItem("Exit");
            exit.Click += new EventHandler(close);

            //关联托盘控件
            MenuItem[] childen = new MenuItem[] { open, exit };
            notifyIcon.ContextMenu = new ContextMenu(childen);
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        //{
        //    //如果鼠标左键单击
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        if (this.Visibility == Visibility.Visible)
        //        {
        //            this.Visibility = Visibility.Hidden;
        //        }
        //        else
        //        {
        //            this.Visibility = Visibility.Visible;
        //            this.Activate();
        //        }
        //    }
        //}

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            this.Show();
            WindowState = ws;
        }


        private void Show(object sender, EventArgs e)
        {
            this.Show();
            WindowState = ws;
        }


        /// <summary>
        /// 退出选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close(object sender, EventArgs e)
        {

            if (System.Windows.MessageBox.Show("请确认是否退出？",
                                               "UB云支付平台",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                shouldClose = true;
                System.Windows.Application.Current.Shutdown();
            }
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }

    }
}
