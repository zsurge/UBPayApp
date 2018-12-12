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

namespace UBPayApp
{
    /// <summary>
    /// Set.xaml 的交互逻辑
    /// </summary>
    public partial class Set : Page
    {
        Thread procThreadProcess = null;
        IniFile ParmIni;

        bool store_flag = true;
        bool user_flag = true;
        bool device_flag = true;


        public Set()
        {
            InitializeComponent();

            //del 2018.10.6
            //tBoxCaptureScreen_Left.Text = Var.CaptureScreen_Left.ToString();
            //tBoxCaptureScreen_Top.Text = Var.CaptureScreen_Top.ToString();
            //tBoxCaptureScreen_Width.Text = Var.CaptureScreen_Width.ToString();
            //tBoxCaptureScreen_Height.Text = Var.CaptureScreen_Height.ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            procThreadProcess = new Thread(new ThreadStart(ThreadProcess1));
            procThreadProcess.Start();
        }

        private void ThreadProcess1()
        {
            string result = null;
            if (PayApi.ApiGetUserInfo(Var.ltoken, out result, out Var.g_User_Info, out Var.g_Agent_Info, out Var.g_Merchant_Info, out Var.g_Store_Info) == true)
            {
                //save token
                MessageBox.Show(result);
            }
            else
            {

            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            procThreadProcess = new Thread(new ThreadStart(ThreadProcess2));
            procThreadProcess.Start();
        }

        private void ThreadProcess2()
        {
            string result = null;
            Var.g_StoreList_Info = new Var.StoreList_Info[128];
            Var.Get_StoreListCount = 0;
            if (PayApi.ApiGetStoreList(Var.ltoken, out result, out Var.g_StoreList_Info, out Var.Get_StoreListCount) == true)
            {
                //save token
                MessageBox.Show(result);
            }
            else
            {

            }
        }

        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid2.Visibility = System.Windows.Visibility.Hidden;
            grid8.Visibility = System.Windows.Visibility.Visible;
            grid11.Visibility = System.Windows.Visibility.Hidden;
            grid3.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF333378"));
            grid4.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF41416C"));
            grid5.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF333378"));
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid2.Visibility = System.Windows.Visibility.Visible;
            grid8.Visibility = System.Windows.Visibility.Hidden;
            grid11.Visibility = System.Windows.Visibility.Hidden;
            grid3.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF41416C"));
            grid4.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF333378"));
            grid5.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF333378"));
        }

        private void label3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid2.Visibility = System.Windows.Visibility.Hidden;
            grid8.Visibility = System.Windows.Visibility.Hidden;
            grid11.Visibility = System.Windows.Visibility.Visible;
            grid3.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF333378"));
            grid4.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF333378"));
            grid5.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF41416C"));
        }

        private void btYes_Checked(object sender, RoutedEventArgs e)
        {
            ParmIni.IniWriteValue("Init", "windows", "1");
        }

        private void btNo_Checked(object sender, RoutedEventArgs e)
        {
            ParmIni.IniWriteValue("Init", "windows", "2");
        }

        private void btPosition_Click(object sender, RoutedEventArgs e)
        {
            //ParmIni.IniWriteValue("CaptureScreen", "Left", tBoxCaptureScreen_Left.Text);
            //ParmIni.IniWriteValue("CaptureScreen", "Top", tBoxCaptureScreen_Top.Text);
            //ParmIni.IniWriteValue("CaptureScreen", "Width", tBoxCaptureScreen_Width.Text);
            //ParmIni.IniWriteValue("CaptureScreen", "Height", tBoxCaptureScreen_Height.Text);

            SetAmountPosition win = new SetAmountPosition();
            
            if (!win.IsVisible)
            {
                win.ShowDialog();
            }
            else
            {
                win.Activate();
            }
        }

        private void tBoxCaptureScreen_Left_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {                
                e.Handled = false;            
            }            
            else 
                e.Handled = true;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            Var.PayChannel = 1;
            ParmIni.IniWriteValue("Pay", "PayChannel", "1");
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            Var.PayChannel = 2;
            ParmIni.IniWriteValue("Pay", "PayChannel", "2");
        }

        private void cmbPaymentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cmbStore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!store_flag)
            {
                ParmIni.IniWriteValue("Init", "store_id", Var.g_StoreList_Info[cmbStore.SelectedIndex].id);
                Var.store_id = Var.g_StoreList_Info[cmbStore.SelectedIndex].id;
                GetUserListAndDisplay();
                GetDeviceListAndDisplay();
            }
        }

        private void cmbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ParmIni.IniWriteValue("Init", "operator_id", Var.g_UserList_Info[cmbUser.SelectedIndex].id);

            if (!user_flag)
            {
                if (cmbUser.SelectedIndex == -1)
                {
                    //Var.operator_id = Var.g_UserList_Info[0].id;
                    return;
                }
                else
                {
                    Var.operator_id = Var.g_UserList_Info[cmbUser.SelectedIndex].id;
                    ParmIni.IniWriteValue("Init", "operator_id", Var.g_UserList_Info[cmbUser.SelectedIndex].id);
                }
            }            

        }

        private void cmbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ParmIni.IniWriteValue("Init", "device_id", Var.g_DeviceList_Info[cmbDevice.SelectedIndex].id);  
            //if (e.RemovedItems.Count >= 1)
            if (!device_flag)
            {
                if (cmbDevice.SelectedIndex == -1)
                {
                    //Var.device_id = Var.g_DeviceList_Info[0].id;
                    return;
                }
                else
                {
                    Var.device_id = Var.g_DeviceList_Info[cmbDevice.SelectedIndex].id;
                    ParmIni.IniWriteValue("Init", "device_id", Var.g_DeviceList_Info[cmbDevice.SelectedIndex].id);
                }
            }
        }


        private void GetUserListAndDisplay()
        {  
            string result = string.Empty;
            //modify 20181104 放在设置时查询，不同的门店有不同的用户ID和设备ID
            Var.g_UserList_Info = new Var.UserList_Info[128];
            Var.Get_UserListCount = 0;
            if (PayApi.ApiGetUserList(Var.store_id, Var.ltoken, out result, out Var.g_UserList_Info, out Var.Get_UserListCount) == false)
            {
                MessageBox.Show("获取店员列表：" + result);
                return;
            }
            else
            {
                cmbUser.Items.Clear();

                for (int i = 0; i < Var.Get_UserListCount; i++)
                {
                    cmbUser.Items.Add(Var.g_UserList_Info[i].username);
                }
                cmbUser.SelectedIndex = 0;                
            }
        }

        private void GetDeviceListAndDisplay()
        {
            string result = string.Empty;
            Var.g_DeviceList_Info = new Var.DeviceList_Info[128];
            Var.Get_DeviceListCount = 0;
            if (PayApi.ApiGetDeviceList(Var.store_id, Var.ltoken, out result, out Var.g_DeviceList_Info, out Var.Get_DeviceListCount) == false)
            {   
                MessageBox.Show("获取设备列表：" + result);
                return;
            }
            else
            {
                cmbDevice.Items.Clear();

                for (int i = 0; i < Var.Get_DeviceListCount; i++)
                {
                    cmbDevice.Items.Add(Var.g_DeviceList_Info[i].name);
                }
                cmbDevice.SelectedIndex = 0;
            }
        }

        private void btCheckPW_Click(object sender, RoutedEventArgs e)
        {
            string result = "";

            if (tKeyBoxInputID.Text.Length != 32)
            {
                MessageBox.Show("请输入正确的KEY");
                return;
            }

            bool ret = PayApi.ApiVerifyKey(tKeyBoxInputID.Text, Var.ltoken, out result);

            if(ret)
            {
                MessageBox.Show(result);
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ParmIni = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"/Parm.ini");
            string stemp = ParmIni.IniReadValue("Init", "store_id");
            int iPos = 0;

            // 门店
            for (int i = 0; i < Var.Get_StoreListCount; i++)
            {
                cmbStore.Items.Add(Var.g_StoreList_Info[i].name);
                if (stemp.Trim() == Var.g_StoreList_Info[i].id)
                {
                    iPos = i;
                }
            }  
            cmbStore.SelectedIndex = iPos;

            store_flag = false;


            ////modify 20181104 在选择相应门店的时候，才列出相应店员
            //// 店员
            cmbUser.Items.Clear();
            stemp = ParmIni.IniReadValue("Init", "operator_id");
            for (int i = 0; i < Var.Get_UserListCount; i++)
            {
                cmbUser.Items.Add(Var.g_UserList_Info[i].username);
                if (stemp.Trim() == Var.g_UserList_Info[i].id)
                {
                    iPos = i;
                }
            }
            cmbUser.SelectedIndex = iPos;
            user_flag = false;



            //modify 20181104 在选择相应门店时，列出所有设备ID
            //// 设备
            cmbDevice.Items.Clear();
            stemp = ParmIni.IniReadValue("Init", "device_id");
            for (int i = 0; i < Var.Get_DeviceListCount; i++)
            {
                cmbDevice.Items.Add(Var.g_DeviceList_Info[i].name);
                if (stemp.Trim() == Var.g_DeviceList_Info[i].id)
                {
                    iPos = i;
                }
            }
            cmbDevice.SelectedIndex = iPos;
            device_flag = false;


            grid2.Visibility = System.Windows.Visibility.Visible;
            grid8.Visibility = System.Windows.Visibility.Hidden;
            grid11.Visibility = System.Windows.Visibility.Hidden;

            if (Var.windowsType == 1)
            {
                btYes.IsChecked = true;
                btNo.IsChecked = false;
            }
            else
            {
                btYes.IsChecked = false;
                btNo.IsChecked = true;
            }

            if (Var.sound == 1)
            {
                rBSoundStart.IsChecked = true;
                rBSoundClose.IsChecked = false;
            }
            else
            {
                rBSoundStart.IsChecked = false;
                rBSoundClose.IsChecked = true;
            }

            if (Var.dispnotes == 1)
            {
                rBDispNotesStart.IsChecked = true;
                rBDispNotesClose.IsChecked = false;
            }
            else
            {
                rBDispNotesStart.IsChecked = false;
                rBDispNotesClose.IsChecked = true;
            }

            if (Var.PayChannel == 1)
            {
                radioButton1.IsChecked = true;
                radioButton2.IsChecked = false;
            }
            else
            {
                radioButton1.IsChecked = false;
                radioButton2.IsChecked = true;
            }

            if (Var.PayChannel == 1)
            {
                radioButton3.IsChecked = true;
                radioButton4.IsChecked = false;
                radioButton5.IsChecked = false;
            }
            else if (Var.PayChannel == 2)
            {
                radioButton3.IsChecked = false;
                radioButton4.IsChecked = true;
                radioButton5.IsChecked = false;
            }
            else
            {
                radioButton3.IsChecked = false;
                radioButton4.IsChecked = false;
                radioButton5.IsChecked = true;
            }


        }
    }
}
