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
using System.ComponentModel;
using System.Data;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using DataTable = System.Data.DataTable;

namespace UBPayApp
{
    /// <summary>
    /// OrderQuery.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class OrderCount : Page
    {
        public OrderCount()
        {
            InitializeComponent();    
        }

        private void SetDefaultTime()
        {
            string start = " 00:00:00";
            string end = " 23:59:59";

            string DateStr = DateTime.Now.ToString("yyyy-MM-dd");

            datePicker2.Value = DateTime.Parse(DateStr + start);
            datePicker1.Value = DateTime.Parse(DateStr + end);      
        }

        private void dutyTimeSet()
        {
            datePicker2.Value = DateTime.Parse(Var.LoginTime);
            datePicker1.Value = DateTime.Now;
        }


        private void bt__OrderCount_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            
            Var.g_all_Order_Count = new Var.all_Order_Count();

            //if (cmb_summary.SelectedIndex == -1)
            //{
            //    SetDefaultTime();
            //}

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetOrderCount(Var.ltoken,Var.operator_id, Var.device_id,time_start, time_end, out result,out Var.g_all_Order_Count) == false)
            {
                return;
            }

            displayCountInfo();
        }

        public void displayCountInfo()
        {
            lb_weixin_TranAmount.Content = Var.g_all_Order_Count.weixin.sum_amount;
            lb_weixin_RefundAmount.Content = Var.g_all_Order_Count.weixin.sum_refund_amount;
            lb_weixin_RealAmount.Content = Var.g_all_Order_Count.weixin.sum_actual_amount;
            lb_weixin_DiscountAmount.Content = Var.g_all_Order_Count.weixin.sum_amount;
            lb_weixin_SettlementAmount.Content = Var.g_all_Order_Count.weixin.sum_amount;
            lb_weixin_refundCount.Content = Var.g_all_Order_Count.weixin.count_refund;
            lb_weixin_TranCount.Content = Var.g_all_Order_Count.weixin.count;

            lb_alipay_TranAmount.Content = Var.g_all_Order_Count.alipay.sum_amount;
            lb_alipay_RefundAmount.Content = Var.g_all_Order_Count.alipay.sum_refund_amount;
            lb_alipay_RealAmount.Content = Var.g_all_Order_Count.alipay.sum_actual_amount;
            lb_alipay_DiscountAmount.Content = Var.g_all_Order_Count.alipay.sum_amount;
            lb_alipay_SettlementAmount.Content = Var.g_all_Order_Count.alipay.sum_amount;
            lb_alipay_refundCount.Content = Var.g_all_Order_Count.alipay.count_refund;
            lb_alipay_TranCount.Content = Var.g_all_Order_Count.alipay.count;

            lb_xt_TranAmount.Content = Var.g_all_Order_Count.xt.sum_amount;
            lb_xt_RefundAmount.Content = Var.g_all_Order_Count.xt.sum_refund_amount;
            lb_xt_RealAmount.Content = Var.g_all_Order_Count.xt.sum_actual_amount;
            lb_xt_DiscountAmount.Content = Var.g_all_Order_Count.xt.sum_amount;
            lb_xt_SettlementAmount.Content = Var.g_all_Order_Count.xt.sum_amount;
            lb_xt_refundCount.Content = Var.g_all_Order_Count.xt.count_refund;
            lb_xt_TranCount.Content = Var.g_all_Order_Count.xt.count;

            lb_unionpay_TranAmount.Content = Var.g_all_Order_Count.unionpay.sum_amount;
            lb_unionpay_RefundAmount.Content = Var.g_all_Order_Count.unionpay.sum_refund_amount;
            lb_unionpay_RealAmount.Content = Var.g_all_Order_Count.unionpay.sum_actual_amount;
            lb_unionpay_DiscountAmount.Content = Var.g_all_Order_Count.unionpay.sum_amount;
            lb_unionpay_SettlementAmount.Content = Var.g_all_Order_Count.unionpay.sum_amount;
            lb_unionpay_refundCount.Content = Var.g_all_Order_Count.unionpay.count_refund;
            lb_unionpay_TranCount.Content = Var.g_all_Order_Count.unionpay.count;

            lb_xz_weixin_TranAmount.Content = Var.g_all_Order_Count.xz_weixin.sum_amount;
            lb_xz_weixin_RefundAmount.Content = Var.g_all_Order_Count.xz_weixin.sum_refund_amount;
            lb_xz_weixin_RealAmount.Content = Var.g_all_Order_Count.xz_weixin.sum_actual_amount;
            lb_xz_weixin_DiscountAmount.Content = Var.g_all_Order_Count.xz_weixin.sum_amount;
            lb_xz_weixin_SettlementAmount.Content = Var.g_all_Order_Count.xz_weixin.sum_amount;
            lb_xz_weixin_refundCount.Content = Var.g_all_Order_Count.xz_weixin.count_refund;
            lb_xz_weixin_TranCount.Content = Var.g_all_Order_Count.xz_weixin.count;

            lb_xz_alipay_TranAmount.Content = Var.g_all_Order_Count.xz_alipay.sum_amount;
            lb_xz_alipay_RefundAmount.Content = Var.g_all_Order_Count.xz_alipay.sum_refund_amount;
            lb_xz_alipay_RealAmount.Content = Var.g_all_Order_Count.xz_alipay.sum_actual_amount;
            lb_xz_alipay_DiscountAmount.Content = Var.g_all_Order_Count.xz_alipay.sum_amount;
            lb_xz_alipay_SettlementAmount.Content = Var.g_all_Order_Count.xz_alipay.sum_amount;
            lb_xz_alipay_refundCount.Content = Var.g_all_Order_Count.xz_alipay.count_refund;
            lb_xz_alipay_TranCount.Content = Var.g_all_Order_Count.xz_alipay.count;

            lb_sf_weixin_TranAmount.Content = Var.g_all_Order_Count.sf_weixin.sum_amount;
            lb_sf_weixin_RefundAmount.Content = Var.g_all_Order_Count.sf_weixin.sum_refund_amount;
            lb_sf_weixin_RealAmount.Content = Var.g_all_Order_Count.sf_weixin.sum_actual_amount;
            lb_sf_weixin_DiscountAmount.Content = Var.g_all_Order_Count.sf_weixin.sum_amount;
            lb_sf_weixin_SettlementAmount.Content = Var.g_all_Order_Count.sf_weixin.sum_amount;
            lb_sf_weixin_refundCount.Content = Var.g_all_Order_Count.sf_weixin.count_refund;
            lb_sf_weixin_TranCount.Content = Var.g_all_Order_Count.sf_weixin.count;

            
            lb_sf_alipay_TranAmount.Content = Var.g_all_Order_Count.sf_alipay.sum_amount;
            lb_sf_alipay_RefundAmount.Content = Var.g_all_Order_Count.sf_alipay.sum_refund_amount;
            lb_sf_alipay_RealAmount.Content = Var.g_all_Order_Count.sf_alipay.sum_actual_amount;
            lb_sf_alipay_DiscountAmount.Content = Var.g_all_Order_Count.sf_alipay.sum_amount;
            lb_sf_alipay_SettlementAmount.Content = Var.g_all_Order_Count.sf_alipay.sum_amount;
            lb_sf_alipay_refundCount.Content = Var.g_all_Order_Count.sf_alipay.count_refund;
            lb_sf_alipay_TranCount.Content = Var.g_all_Order_Count.sf_alipay.count;


        }

        //private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    datePicker1.Text = (DateTime.Parse(datePicker1.Text)).ToString("yyyy-MM-dd");
        //}

        //private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    datePicker2.Text = (DateTime.Parse(datePicker2.Text)).ToString("yyyy-MM-dd");
        //}

        //public void goPage(string page)
        //{
        //    Frame pageFrame1 = null;
        //    DependencyObject currParent1 = System.Windows.Media.VisualTreeHelper.GetParent(this);
        //    while (currParent1 != null && pageFrame1 == null)
        //    {
        //        pageFrame1 = currParent1 as Frame;
        //        currParent1 = VisualTreeHelper.GetParent(currParent1);
        //    }
        //    // Change the page of the frame.
        //    if (pageFrame1 != null)
        //    {
        //        pageFrame1.Source = new Uri(page, UriKind.Relative);
        //    }
        //}



        private void bt__change_Click(object sender, RoutedEventArgs e)
        {
            string result = null;

            string end_time = DateTime.Now.ToString();

            if (PayApi.ApiHandover(Var.ltoken, Var.operator_id,  Var.LoginTime, end_time, out result) == false)
            {
                return;
            }


            //goPage("Login.xaml");   

            //NavigationWindow wds = new NavigationWindow();
            //wds.Source = new Uri("Login.xaml", UriKind.Relative);
            //wds.Show();

            //关闭当前页
            //Window win = (Window)this.Parent;
            //win.Close();




            // Restart current process Method 2
            Var.CloseMsgFlag = false;
            System.Reflection.Assembly.GetEntryAssembly();
            string startpath = System.IO.Directory.GetCurrentDirectory();
            System.Diagnostics.Process.Start(startpath + "\\UBPayApp.exe");
            Application.Current.Shutdown();



            //goPage("Login.xaml");
            //NavigationService.GetNavigationService(this).Navigate(new Uri("Login.xaml", UriKind.Relative));
            //NavigationService.GetNavigationService(this).GoForward(); 向后转
            //NavigationService.GetNavigationService(this).GoBack(); 向前转
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultTime();

            cmb_summary.Items.Add("当班汇总");
            cmb_summary.Items.Add("当日汇总");            
        }

        private void cmb_summary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {                
                if (cmb_summary.SelectedIndex == 0)
                {
                    //当班
                    dutyTimeSet();
                }
                else
                {
                    //当日
                    SetDefaultTime();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
