using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace UBPayApp
{
    /// <summary>
    /// MainPay.xaml 的交互逻辑
    /// </summary>
    public partial class MainPay : Page
    {
        string dir = "";
        IniFile ParmIni;

        public MainPay()
        {
            InitializeComponent();

            dir = System.AppDomain.CurrentDomain.BaseDirectory;
            ParmIni = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"/Parm.ini");

            //lb__LoginUserName.Content = Var.g_User_Info.username;
            //lb__LoginUserName1.Content = Var.g_User_Info.username;

            menu_user.Header = Var.g_User_Info.username; ;

            img__OrderQuery.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdan_s.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__OrderSum.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdanchaxun.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Charge.Source = BitmapFrame.Create(new Uri(dir + @"pic/jiaojieban.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Setting.Source = BitmapFrame.Create(new Uri(dir + @"pic/shezhi.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Refund.Source = BitmapFrame.Create(new Uri(dir + @"pic/refund.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);

            lb__OrderQuery.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            g_line1.Visibility = System.Windows.Visibility.Visible;
            lb__OrderSum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line2.Visibility = System.Windows.Visibility.Hidden;
            lb__Change.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line3.Visibility = System.Windows.Visibility.Hidden;
            lb__Set.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line4.Visibility = System.Windows.Visibility.Hidden;
            lb__Refund.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line5.Visibility = System.Windows.Visibility.Hidden;

            Var.bLogin = true;
            this.frame1.Source = new Uri("OrderQuery.xaml", UriKind.Relative);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //this.frame1.Source = new Uri("MainPay.xaml", UriKind.Relative);
            goPage("MainPay.xaml");
        }

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

        private void img__Quit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            goPage("Login.xaml");
        }

        private void lb__OrderQuery_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.frame1.Source = new Uri("OrderQuery.xaml", UriKind.Relative);

            img__OrderQuery.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdan_s.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__OrderSum.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdanchaxun.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Charge.Source = BitmapFrame.Create(new Uri(dir + @"pic/jiaojieban.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Setting.Source = BitmapFrame.Create(new Uri(dir + @"pic/shezhi.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Refund.Source = BitmapFrame.Create(new Uri(dir + @"pic/refund.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);

            lb__OrderQuery.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            g_line1.Visibility = System.Windows.Visibility.Visible;
            lb__OrderSum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line2.Visibility = System.Windows.Visibility.Hidden;
            lb__Change.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line3.Visibility = System.Windows.Visibility.Hidden;
            lb__Set.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line4.Visibility = System.Windows.Visibility.Hidden;
            lb__Refund.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line5.Visibility = System.Windows.Visibility.Hidden;
        }

        private void lb__OrderSum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.frame1.Source = new Uri("OrderSum.xaml", UriKind.Relative);

            img__OrderQuery.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdan.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__OrderSum.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdanchaxun_s.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Charge.Source = BitmapFrame.Create(new Uri(dir + @"pic/jiaojieban.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Setting.Source = BitmapFrame.Create(new Uri(dir + @"pic/shezhi.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Refund.Source = BitmapFrame.Create(new Uri(dir + @"pic/refund.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);

            lb__OrderQuery.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line1.Visibility = System.Windows.Visibility.Hidden;

            lb__OrderSum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            g_line2.Visibility = System.Windows.Visibility.Visible;

            lb__Change.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line3.Visibility = System.Windows.Visibility.Hidden;

            lb__Set.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line4.Visibility = System.Windows.Visibility.Hidden;

            lb__Refund.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line5.Visibility = System.Windows.Visibility.Hidden;
        }

        private void lb__Change_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //goPage("Login.xaml");
            this.frame1.Source = new Uri("OrderCount.xaml", UriKind.Relative);

            img__OrderQuery.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdan.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__OrderSum.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdanchaxun.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Charge.Source = BitmapFrame.Create(new Uri(dir + @"pic/jiaojieban_s.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Setting.Source = BitmapFrame.Create(new Uri(dir + @"pic/shezhi.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Refund.Source = BitmapFrame.Create(new Uri(dir + @"pic/refund.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);



            lb__OrderQuery.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line1.Visibility = System.Windows.Visibility.Hidden;
            lb__OrderSum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line2.Visibility = System.Windows.Visibility.Hidden;
            lb__Refund.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line3.Visibility = System.Windows.Visibility.Hidden;
            lb__Change.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            g_line4.Visibility = System.Windows.Visibility.Visible;
            lb__Set.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line5.Visibility = System.Windows.Visibility.Hidden;
        }

        private void lb__Set_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.frame1.Source = new Uri("Set.xaml", UriKind.Relative);

            img__OrderQuery.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdan.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__OrderSum.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdanchaxun.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Charge.Source = BitmapFrame.Create(new Uri(dir + @"pic/jiaojieban.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Setting.Source = BitmapFrame.Create(new Uri(dir + @"pic/shezhi_s.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Refund.Source = BitmapFrame.Create(new Uri(dir + @"pic/refund.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);

            lb__OrderQuery.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line1.Visibility = System.Windows.Visibility.Hidden;

            lb__OrderSum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line2.Visibility = System.Windows.Visibility.Hidden;

            lb__Change.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line3.Visibility = System.Windows.Visibility.Hidden;

            lb__Set.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            g_line4.Visibility = System.Windows.Visibility.Hidden;

            lb__Refund.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line5.Visibility = System.Windows.Visibility.Visible;
        }

        #region 输入编辑访客姓名 控件滑屏操作
        double mPointY;//触摸点的Y坐标
        double mOffsetY;//滚动条当前位置
        bool mIsTouch = false;//是否触摸

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void scrollViewer1_TouchDown(object sender, TouchEventArgs e)
        {
            mIsTouch = true;                                        //正在触摸
            mOffsetY = this.scrollViewer1.VerticalOffset;           //获取ScrollViewer滚动条当前位置
            TouchPoint point = e.GetTouchPoint(scrollViewer1);      //获取相对于ScrollViewer的触摸点位置
            mPointY = point.Position.Y;                             //触摸点的Y坐标
            //tb_Select.Focus();
            //for (int i = 0; i < HaveVisitorNum; i++)
            //{
            //    tbInput[i].Focusable = false;
            //    bMouseClick = false;
            //}
        }

        bool bMouseClick = false;
        private void scrollViewer1_TouchMove(object sender, TouchEventArgs e)
        {
            if (mIsTouch == true)                                   //如果正在触摸
            {
                TouchPoint point = e.GetTouchPoint(scrollViewer1);  //获取相对于ScrollViewer的触摸点位置
                double DiffOffset = point.Position.Y - mPointY;     //计算相对位置

                //tb_Select.Focus();

                this.scrollViewer1.ScrollToVerticalOffset(mOffsetY - DiffOffset);//ScrollViewer滚动到指定位
            }
        }

        private void scrollViewer1_TouchUp(object sender, TouchEventArgs e)
        {
            mIsTouch = false;                                       //触摸结束
        }
        #endregion

        //bool dispuser_iclick = false;
        //private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (dispuser_iclick == true)
        //    {
        //        dispuser_iclick = false;
        //        scrollViewer1.Visibility = Visibility.Hidden;
        //    }
        //    else
        //    {
        //        LoadDispVisitor();

        //        dispuser_iclick = true;
        //        scrollViewer1.Visibility = Visibility.Visible;
        //    }
        //}

        //private void LoadDispVisitor()
        //{
        //    for (int i = 0; i < Var.Get_UserListCount; i++)
        //    {
        //        Border img_Bg = new Border();
        //        img_Bg.BorderThickness = new Thickness(1,1,1,1);
        //        img_Bg.BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF737070"));
        //        img_Bg.Width = 100;
        //        img_Bg.Height = 30;

        //        Label Lb_User = new Label();
        //        Lb_User.Width = 100;
        //        Lb_User.Height = 30;

        //        Lb_User.FontSize = 15;
        //        Lb_User.Content = Var.g_UserList_Info[i].username;
        //        img_Bg.Margin = new Thickness(10, 5 + i * 30, 0, 0);
        //        Lb_User.Margin = new Thickness(12, 7 + i * 30, 0, 0);
        //        inputCanvas.Children.Add(img_Bg);
        //        inputCanvas.Children.Add(Lb_User);
        //    }
        //    inputCanvas.Height = Var.Get_UserListCount * 30+ 10;
        //}

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void frame1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //dispuser_iclick = false;
            scrollViewer1.Visibility = Visibility.Hidden;
        }

        private void lb__Refund_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.frame1.Source = new Uri("RefundQuery.xaml", UriKind.Relative);

            img__OrderQuery.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdan.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__OrderSum.Source = BitmapFrame.Create(new Uri(dir + @"pic/dingdanchaxun.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Charge.Source = BitmapFrame.Create(new Uri(dir + @"pic/jiaojieban.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Setting.Source = BitmapFrame.Create(new Uri(dir + @"pic/shezhi.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);
            img__Refund.Source = BitmapFrame.Create(new Uri(dir + @"pic/refund_s.png"), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None);



            lb__OrderQuery.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line1.Visibility = System.Windows.Visibility.Hidden;
            lb__OrderSum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line2.Visibility = System.Windows.Visibility.Hidden;
            lb__Refund.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            g_line3.Visibility = System.Windows.Visibility.Visible;
            lb__Change.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line4.Visibility = System.Windows.Visibility.Hidden;
            lb__Set.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB1ACAC"));
            g_line5.Visibility = System.Windows.Visibility.Hidden;

        }

        private void CheckUpgrade_Click(object sender, RoutedEventArgs e)
        {
            //检查更新
            string result = string.Empty;
            string version = string.Empty;
            string url = string.Empty;
            
            if (PayApi.ApiGetNewVersion("3", out result, out version, out url) == false)
            {
                return;
            }

            //定义消息框             
            string messageBoxText = "当前版本为：" + Var.AppVer + "\r\n" + "最新版本为:" + version + "\r\n" + "请选择是否升级?";
            string caption = "update";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            //显示消息框              
            MessageBoxResult msg = MessageBox.Show(messageBoxText, caption, button, icon);


            //下载V1.0.0
            int currentVer = Convert.ToInt16(Var.AppVer.Substring(1, 1)) * 100 + Convert.ToInt16(Var.AppVer.Substring(3, 1)) * 10 + Convert.ToInt16(Var.AppVer.Substring(5, 1));
            int newVer = Convert.ToInt16(version.Substring(0, 1)) * 100 + Convert.ToInt16(version.Substring(2, 1)) * 10 + Convert.ToInt16(version.Substring(4, 1));



            //处理消息框信息              
            switch (msg)
            {
                case MessageBoxResult.Yes:
                    if (newVer > currentVer)
                    {
                        string path = "UBAutoUpdate.exe";
                        if (File.Exists(path))
                        {
                            Process.Start(path, url);
                        }

                        Application.Current.Shutdown();
                    }
                    break;
                case MessageBoxResult.No:
                    // ...                      
                    break;
                case MessageBoxResult.Cancel:
                    // ...                     
                    break;
            }

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginOut_Click(object sender, RoutedEventArgs e)
        {
            System.Reflection.Assembly.GetEntryAssembly();
            string startpath = System.IO.Directory.GetCurrentDirectory();
            System.Diagnostics.Process.Start(startpath + "\\UBPayApp.exe");
            Application.Current.Shutdown();
        }

        private void ReSetting_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("暂无该功能", "系统提示", MessageBoxButton.OK, MessageBoxImage.Exclamation); 
        }

        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("暂无该功能", "系统提示", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
