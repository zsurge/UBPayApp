using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using System.Threading;

using ThoughtWorks;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

using System.Drawing;

namespace UBPayApp
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Page
    {
        string dir = "";
        Thread procThreadProcess = null;
        Thread procCheckLoginThreadProcess = null;
        Thread procChengeBarCodeThreadProcess = null;
        IniFile ParmIni;

        public Login()
        {
            InitializeComponent();

            lb_Ver.Content = Var.AppVer;
            dir = System.AppDomain.CurrentDomain.BaseDirectory;
            ParmIni = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"/Parm.ini");

            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                    (Action)(() => { Keyboard.Focus(tBoxInputID); }));
            lb__BarCodeLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            lb__IDLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

            gridIDLogin.Visibility = System.Windows.Visibility.Visible;
            gridCoeLogin.Visibility = System.Windows.Visibility.Hidden;
            gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
            Var.bLogin = false;
        }

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    //this.frame1.Source = new Uri("MainPay.xaml", UriKind.Relative);

        //    //gridlogin.Visibility = System.Windows.Visibility.Visible;
        //    //procThreadProcess = new Thread(new ThreadStart(ThreadProcess));
        //    //procThreadProcess.Start();
        //    goPage("MainPay.xaml");
        //}

        public void goPage(string page)
        {
            Frame pageFrame1 = null;
            DependencyObject currParent1 = System.Windows.Media.VisualTreeHelper.GetParent(this);
            while (currParent1 != null && pageFrame1 == null)
            {
                pageFrame1 = currParent1 as Frame;
                currParent1 = VisualTreeHelper.GetParent(currParent1);
            }
            // Change the page of the frame.
            if (pageFrame1 != null)
            {
                pageFrame1.Source = new Uri(page, UriKind.Relative);
            }
        }

        private void tBoxInputID_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            img_Login__Inputframe1.Source = Function.GetBitmapImage(dir + @"pic\inputframe_hot.png");
            img_Login__Inputframe2.Source = Function.GetBitmapImage(dir + @"pic\inputframe.png");
        }

        private void tBoxInputID_GotFocus(object sender, RoutedEventArgs e)
        {
            img_Login__Inputframe1.Source = Function.GetBitmapImage(dir + @"pic\inputframe_hot.png");
            img_Login__Inputframe2.Source = Function.GetBitmapImage(dir + @"pic\inputframe.png");
        }

        private void tBoxInputPW_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            img_Login__Inputframe2.Source = Function.GetBitmapImage(dir + @"pic\inputframe_hot.png");
            img_Login__Inputframe1.Source = Function.GetBitmapImage(dir + @"pic\inputframe.png");
        }

        private void tBoxInputPW_GotFocus(object sender, RoutedEventArgs e)
        {
            img_Login__Inputframe2.Source = Function.GetBitmapImage(dir + @"pic\inputframe_hot.png");
            img_Login__Inputframe1.Source = Function.GetBitmapImage(dir + @"pic\inputframe.png");
        }

        private void lb__IDLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            iCurrentPosi = 1;
            Var.mLoginCheckEvent.Set();
            Var.mChengeBarCodeEvent.Set();
            lb__BarCodeLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            lb__IDLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            gridIDLogin.Visibility = System.Windows.Visibility.Visible;
            gridCoeLogin.Visibility = System.Windows.Visibility.Hidden;
            tBoxInputID.Text = "";
            tBoxInputPW.Password = "";
            lbInputID.Content = "请输入账号";
            lbInputPW.Content = "请输入密码";
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                    (Action)(() => { Keyboard.Focus(tBoxInputID); }));
        }

        int iCurrentPosi = 0;
        private void lb__BarCodeLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (bLogining)
                return;
            if (iCurrentPosi == 2)
                return;

            iCurrentPosi = 2;
            lb__IDLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            lb__BarCodeLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            gridIDLogin.Visibility = System.Windows.Visibility.Hidden;
            gridCoeLogin.Visibility = System.Windows.Visibility.Visible;
            img__Code.Visibility = System.Windows.Visibility.Hidden;
            lb__Info.Visibility = System.Windows.Visibility.Visible;
            lb__Info.Margin = new Thickness(0, 26, 0, 0);
            lb__Info.Content = "正在获取登录二维码...";

            procThreadProcess = new Thread(new ThreadStart(ThreadProcess1));
            procThreadProcess.Start();
        }

        string url = "";
        string guid = "";
        private void ThreadProcess1()
        {
            string result = "";
            
            if (PayApi.ApiGetQrCode("user", out result, out guid, out url) == true)
            {
                output(2, "");
            }
            else
            {
                output(3, result);
            }
        }

        bool bLogining = false;
        private void bt__login_Click(object sender, RoutedEventArgs e)
        {
            if (bLogining)
                return;

            bLogining = true;
            gridLoginInfo.Visibility = System.Windows.Visibility.Visible;
            procThreadProcess = new Thread(new ThreadStart(ThreadProcess2));
            procThreadProcess.Start();
        }

        private void ThreadProcess2()
        {
            output(0, "");
        }

        BitmapImage image;
        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                //保存为PNG到内存流  
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }

        
        private delegate void DispFunction();
        public void output(int iState, string sMsg)
        {
            try
            {
                this.Dispatcher.Invoke(new DispFunction(delegate()
                {
                    switch (iState)
                    {
                        case 0:
                            string result = null;
                            if (PayApi.ApiLogin(tBoxInputID.Text, tBoxInputPW.Password, out result) == false)
                            {
                                gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
                                bLogining = false;
                               
                                MessageBox.Show(result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                   (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }
                            output(1, "");
                            break;

                        case 1:
                            gridLoginInfo.Visibility = System.Windows.Visibility.Visible;
                            if (PayApi.ApiGetUserInfo(Var.ltoken, out result, out Var.g_User_Info, out Var.g_Agent_Info, out Var.g_Merchant_Info, out Var.g_Store_Info) == false)
                            {
                                gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
                                bLogining = false;

                                MessageBox.Show("获取用户信息：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                   (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }

                            Var.merchant_id = Var.g_User_Info.merchant_id;
                            Var.store_id = Var.g_User_Info.store_id;
                            Var.operator_id = Var.g_User_Info.id;
                            Var.device_id = ParmIni.IniReadValue("Init", "device_id");

                            ParmIni.IniWriteValue("Init", "merchant_id", Var.merchant_id);
                            ParmIni.IniWriteValue("Init", "store_id", Var.store_id);
                            ParmIni.IniWriteValue("Init", "operator_id", Var.operator_id);

                            Var.g_StoreList_Info = new Var.StoreList_Info[128];
                            Var.Get_StoreListCount = 0;
                            if (PayApi.ApiGetStoreList(Var.ltoken, out result, out Var.g_StoreList_Info, out Var.Get_StoreListCount) == false)
                            {
                                gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
                                bLogining = false;

                                MessageBox.Show("获取门店列表：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                   (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }

                            //modify 20181104 放在设置时查询，不同的门店有不同的用户ID和设备ID
                            Var.g_UserList_Info = new Var.UserList_Info[128];
                            Var.Get_UserListCount = 0;
                            if (PayApi.ApiGetUserList(Var.g_Store_Info.id, Var.ltoken, out result, out Var.g_UserList_Info, out Var.Get_UserListCount) == false)
                            {
                                gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
                                bLogining = false;

                                MessageBox.Show("获取店员列表：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                   (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }

                            //Var.g_DeviceList_Info = new Var.DeviceList_Info[128];
                            Var.Get_DeviceListCount = 0;
                            if (PayApi.ApiGetDeviceList(Var.g_Store_Info.id, Var.ltoken, out result, out Var.g_DeviceList_Info, out Var.Get_DeviceListCount) == false)
                            {
                                gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
                                bLogining = false;

                                MessageBox.Show("获取设备列表：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                   (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }

                            //if (Var.Get_DeviceListCount >= 1)
                            //{
                            //    Var.device_id = Var.g_DeviceList_Info[0].id;
                            //    ParmIni.IniWriteValue("Init", "device_id", Var.g_DeviceList_Info[0].id);
                            //}

                            //获取交易类型列表
                            Var.g_all_tran_list = new Var.all_tran_list[128];
                            Var.all_tran_list_count = 0;

                            if (PayApi.ApiGet_all_paymentList(Var.ltoken, out result, out Var.g_all_payment_list) == false)
                            {
                                gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
                                bLogining = false;

                                MessageBox.Show("获取交易类型列表：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                   (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }

                            //获取交易方式列表 add 1130   
                            if (PayApi.ApiGet_all_paymentChannel(Var.ltoken, out result, out Var.g_all_payment_channel) == false)
                            {
                                gridLoginInfo.Visibility = System.Windows.Visibility.Hidden;
                                bLogining = false;

                                MessageBox.Show("获取交易方式表表：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                   (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }

                            //获取订单状态 add 1130                            
                            if (PayApi.ApiGet_all_OrderStatus(Var.ltoken, out result, out Var.g_all_payment_Order_Status) == false)                                
                            {
                                MessageBox.Show("获取订单状态：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                  (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }

                            if (PayApi.ApiGet_all_RefundStatus(Var.ltoken, out result, out Var.g_all_payment_refund_status) == false)                           
                            {
                                MessageBox.Show("获取退款订单状态：" + result);
                                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                  (Action)(() => { Keyboard.Focus(tBoxInputID); }));
                                return;
                            }


                            //记录下登录的时间 2018.11.18
                            Var.LoginTime = DateTime.Now.ToString();

                            Var.mChengeBarCodeEvent.Set();
                            goPage("MainPay.xaml");

                            break;

                        case 2:
                            img__Code.Visibility = System.Windows.Visibility.Visible;
                            lb__Info.Visibility = System.Windows.Visibility.Visible;

                            //初始化二维码生成工具
                            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                            qrCodeEncoder.QRCodeVersion = 0;
                            qrCodeEncoder.QRCodeScale = 4;

                            //将字符串生成二维码图片
                            Bitmap img = qrCodeEncoder.Encode(url, Encoding.Default);

                            image = BitmapToBitmapImage(img);

                            img__Code.Source = image;
                            lb__Info.Content = "";

                            Var.mLoginCheckEvent.Set();
                            procCheckLoginThreadProcess = new Thread(new ThreadStart(this.ThreadUserCheckLogin));
                            procCheckLoginThreadProcess.Start();      // 重新启动设备初始化线程

                            Var.mChengeBarCodeEvent.Set();
                            Var.iCountTime = 0;
                            procChengeBarCodeThreadProcess = new Thread(new ThreadStart(ThreadChengeBarCode));
                            procChengeBarCodeThreadProcess.Start();
                            break;

                        case 3:
                            lb__Info.Content = sMsg;
                            lb__Info.Margin = new Thickness(0, 26, 0, 0);
                            break;

                        case 4:
                            lb__Info.Content = sMsg;
                            lb__Info.Margin = new Thickness(0, 110, 0, 0);
                            break;

                        case 5:
                            img__Code.Visibility = System.Windows.Visibility.Hidden;
                            lb__Info.Visibility = System.Windows.Visibility.Visible;
                            lb__Info.Margin = new Thickness(0, 26, 0, 0);
                            lb__Info.Content = "正在获取登录二维码...";
                            break;

                        case 6:
                            //初始化二维码生成工具
                            img__Code.Visibility = System.Windows.Visibility.Visible;
                            QRCodeEncoder qrCodeEncoder1 = new QRCodeEncoder();
                            qrCodeEncoder1.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                            qrCodeEncoder1.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                            qrCodeEncoder1.QRCodeVersion = 0;
                            qrCodeEncoder1.QRCodeScale = 4;

                            //将字符串生成二维码图片
                            Bitmap img1 = qrCodeEncoder1.Encode(url, Encoding.Default);

                            image = BitmapToBitmapImage(img1);

                            img__Code.Source = image;
                            lb__Info.Content = "";
                            break;
                    }

                }));
            }
            catch (Exception)
            {
            }
        }

        private void ThreadChengeBarCode()
        {
            string result = null;
            Var.mChengeBarCodeEvent.Reset();
            LogManager.WriteLogTran(LogType.Message, "启动刷新二维码线程...", "");
            while (true)
            {
                if (Var.mChengeBarCodeEvent.WaitOne(50) == true)
                {
                    LogManager.WriteLogTran(LogType.Message, "关闭刷新二维码线程", "");
                    break;
                }
                Var.iCountTime++;
                if (Var.iCountTime == Var.RefreshTime)
                {
                    output(5, "");
                    if (PayApi.ApiGetQrCode("user", out result, out guid, out url) == true)
                    {
                        output(6, "");
                    }
                    else
                    {
                        output(3, result);
                    }
                    Var.iCountTime = 0;
                }
                Thread.Sleep(1000);
            }
        }

        private void ThreadUserCheckLogin()
        {
            string result = null;
            Var.mLoginCheckEvent.Reset();
            int iret = 0;
            LogManager.WriteLogTran(LogType.Message, "启动检查用户二维码扫描登录线程...", "");
            while (true)
            {
                if (Var.mLoginCheckEvent.WaitOne(50) == true)
                {
                    LogManager.WriteLogTran(LogType.Message, "关闭检查用户二维码扫描登录线程", "");
                    break;
                }
                iret= PayApi.ApiQrCodeLogin(guid, out result);
                if (iret == 1)
                {
                    output(1, "");
                }
                else
                {
                    output(4, result);
                }
                Thread.Sleep(2000);
            }
        }

        private void tBoxInputID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tBoxInputID.Text.Length > 0)
                lbInputID.Content = "";
            else
                lbInputID.Content = "请输入账号";
        }

        private void tBoxInputPW_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tBoxInputPW.Password.Length > 0)
                lbInputPW.Content = "";
            else
                lbInputPW.Content = "请输入密码";
        }

        private void tBoxInputID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                                    (Action)(() => { Keyboard.Focus(tBoxInputPW); }));
        }

        private void tBoxInputPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                procThreadProcess = new Thread(new ThreadStart(ThreadProcess2));
                procThreadProcess.Start();
            }
        }

        private void lb_Ver_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown(-1);
        }
    }
}
