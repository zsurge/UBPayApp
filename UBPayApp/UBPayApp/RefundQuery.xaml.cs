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
    /// OrderSum.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class RefundQuery : Page
    {
        public RefundQuery()
        {
            InitializeComponent();

            today.IsChecked = true;

            //交易类型        

            cmbPaymentType.Items.Add("所有交易类型");
            foreach (KeyValuePair<string, string> pair in Var.g_all_payment_list)
            {
                cmbPaymentType.Items.Add(pair.Value);
            }
            //cmbPaymentType.SelectedIndex = 0;

            //支付方式列表
            cmb_Payment_Mode.Items.Add("所有支付方式");
            foreach (KeyValuePair<string, string> pair in Var.g_all_payment_channel)
            {
                cmb_Payment_Mode.Items.Add(pair.Value);
            }
            //cmb_Payment_Mode.SelectedIndex = 0;

            //订单状态列表
            cmb_Order_Status.Items.Add("所有订单状态");
            foreach (KeyValuePair<string, string> pair in Var.g_all_payment_refund_status)
            {
                cmb_Order_Status.Items.Add(pair.Value);
            }

            cmbPaymentMode_Key = "";
            cmb_Payment_Type_Key = "";
            status = "";

        _excelHelper = new ExportToExcel();
        }
        private ExportToExcel _excelHelper;

        int gRefundQueryList_total = 0;
        PageInfo pageInfo = new PageInfo();

        private static string cmbPaymentMode_Key = string.Empty;
        private static string cmb_Payment_Type_Key = string.Empty;
        private static string status = string.Empty;

        //private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    datePicker1.Text = (DateTime.Parse(datePicker1.Text)).ToString("yyyy-MM-dd HH:mm:ss");
        //}

        private void set_label_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }

        private void tBoxInputID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tBoxInputID.Text.Length > 0)
                lbInputID.Content = "";
            else
                lbInputID.Content = "搜索关键字";
        }

        
        private DataTable LoadDispVisitor()
        {
      
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("channel_name", typeof(string)));
            dt.Columns.Add(new DataColumn("payment_name", typeof(string)));
            dt.Columns.Add(new DataColumn("device_name", typeof(string)));
            dt.Columns.Add(new DataColumn("operator_name", typeof(string)));
            dt.Columns.Add(new DataColumn("operator_id", typeof(string)));
            dt.Columns.Add(new DataColumn("order_id", typeof(string)));
            dt.Columns.Add(new DataColumn("refund_order_id", typeof(string)));
            dt.Columns.Add(new DataColumn("amount", typeof(string)));
            dt.Columns.Add(new DataColumn("refund_amount", typeof(string)));
            dt.Columns.Add(new DataColumn("time_create", typeof(string)));
            dt.Columns.Add(new DataColumn("time_update", typeof(string)));
            dt.Columns.Add(new DataColumn("_status", typeof(string)));
            dt.Columns.Add(new DataColumn("reason", typeof(string)));

            //不显示
            dt.Columns.Add(new DataColumn("merchant_id", typeof(string)));

            


            for (int i = 0; i < Var.all_tran_refund_count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["channel_name"] = Var.g_all_tran_refund[i].channel_name;
                dr["payment_name"] = Var.g_all_tran_refund[i].payment_name;
                dr["device_name"] = Var.g_all_tran_refund[i].device_name;
                dr["operator_name"] = Var.g_all_tran_refund[i].operator_name;
                dr["operator_id"] = Var.g_all_tran_refund[i].operator_id;
                dr["order_id"] = Var.g_all_tran_refund[i].order_id;
                dr["refund_order_id"] = Var.g_all_tran_refund[i].refund_order_id;
                dr["amount"] = Var.g_all_tran_refund[i].amount;
                dr["refund_amount"] = Var.g_all_tran_refund[i].refund_amount;
                dr["time_create"] = Var.g_all_tran_refund[i].time_create;
                dr["time_update"] = Var.g_all_tran_refund[i].time_update;
                dr["_status"] = Var.g_all_tran_refund[i]._status;
                dr["reason"] = Var.g_all_tran_refund[i].reason;

                //不显示
                dr["merchant_id"] = Var.g_all_tran_refund[i].merchant_id;

                dt.Rows.Add(dr);
            }

            return dt;
            //OrderRefundQueryPage.ItemsSource = dt.DefaultView;
        }


        private DataTable ExcelHeader()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("支付方式", typeof(string)));
            dt.Columns.Add(new DataColumn("交易类型", typeof(string)));
            dt.Columns.Add(new DataColumn("设备名称", typeof(string)));
            dt.Columns.Add(new DataColumn("店员名称", typeof(string)));
            dt.Columns.Add(new DataColumn("店员ID", typeof(string)));
            dt.Columns.Add(new DataColumn("订单号", typeof(string)));
            dt.Columns.Add(new DataColumn("退款订单号", typeof(string)));
            dt.Columns.Add(new DataColumn("交易金额", typeof(string)));
            dt.Columns.Add(new DataColumn("退款金额", typeof(string)));
            dt.Columns.Add(new DataColumn("订单创建时间", typeof(string)));
            dt.Columns.Add(new DataColumn("订单结束时间", typeof(string)));
            dt.Columns.Add(new DataColumn("状态", typeof(string)));
            dt.Columns.Add(new DataColumn("原因", typeof(string)));


            for (int i = 0; i < Var.all_tran_refund_count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["支付方式"] = Var.g_all_tran_refund[i].channel_name;
                dr["交易类型"] = Var.g_all_tran_refund[i].payment_name;
                dr["设备名称"] = Var.g_all_tran_refund[i].device_name;
                dr["店员名称"] = Var.g_all_tran_refund[i].operator_name;
                dr["店员ID"] = Var.g_all_tran_refund[i].operator_id;
                dr["订单号"] = "'" + Var.g_all_tran_refund[i].order_id;
                dr["退款订单号"] = "'" + Var.g_all_tran_refund[i].refund_order_id;
                dr["交易金额"] = Var.g_all_tran_refund[i].amount;
                dr["退款金额"] = Var.g_all_tran_refund[i].refund_amount;
                dr["订单创建时间"] = Var.g_all_tran_refund[i].time_create;
                dr["订单结束时间"] = Var.g_all_tran_refund[i].time_update;
                dr["状态"] = Var.g_all_tran_refund[i]._status;
                dr["原因"] = Var.g_all_tran_refund[i].reason;

                dt.Rows.Add(dr);
            }

            return dt;
            //OrderRefundQueryPage.ItemsSource = dt.DefaultView;
        }

        /// <param name="inToken">key</param>
        /// <param name="iPage">每页多少条</param>
        /// <param name="Current">当前第几页</param>
        /// <param name="time_type">时间类型1当天 2昨天 3本周 4本月 为空则按照时间段查询</param>
        /// time_start:2018-06-12 00:00:00 开始时间
        /// time_end:2018-07-23 00:00:00 结束时间
        /// <param name="channel">支付方式 使用支付方式接口获取</param>
        /// <param name="payment">交易类型 使用交易类型接口获取</param>
        /// <param name="order_id">订单号</param>
        /// <param name="refund_order_id">退款单号</param>
        /// <param name="store_id">门店id 使用门店列表接口获取</param>
        /// <param name="operator_id">店员id 使用操作员列表接口获取</param>
        /// <param name="device_id">设备id 使用设备列表接口获取</param>
        /// <param name="merchant_id">商户id</param>
        /// <param name="status">通过订单状态接口获取</param>
        /// <param name="inResult"></param>
        /// <param name="paymentList"></param>
        /// <param name="Count"></param>
        /// <param name="Refund_QueryList_total">总的数据条目</param>   //add for 2018.10.5 
        /// 

        int page = 5;
        int current = 1;
        int time_type = 1;
        int gTotalPage = 0;
        int export = 0; //为1则返回所有记录



        private void bt__RefundQuery_Click(object sender, RoutedEventArgs e)
        {
            OrderRefundQueryPage.ItemsSource = null;
            OrderRefundQueryPage.Items.Clear();

            string result = null;
            Var.order_id = tBoxInputID.Text.Trim();
            Var.g_all_tran_refund = new Var.all_tran_refund[128];
            Var.all_tran_refund_count = 0;
            int tmpTotal = 0;
            export = 0;

            ////默认是按当天来查询的            
            //SetDefaultTime();

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');


            if (PayApi.ApiGetRefund_QueryList(Var.ltoken, page, current, time_type, time_start, time_end,  cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id, Var.refund_order_id, Var.store_id, Var.operator_id, Var.device_id, Var.merchant_id, status, export,out result, out Var.g_all_tran_refund, out Var.all_tran_refund_count, out tmpTotal) == false)
            {
                return;
            }

            //ApiGetOrderCount

            gRefundQueryList_total = tmpTotal;

            DisplayRefundPage.DataContext = pageInfo;

            if (gRefundQueryList_total == 0)
            {
                pageInfo.CurrentPage = 0;
            }
            else
            {
                pageInfo.CurrentPage = current;
            }

            pageInfo.TargetPage = 0;
            pageInfo.TotalPage = gTotalPage = tmpTotal % page == 0 ? tmpTotal / page : tmpTotal / page + 1;

            //LoadDispVisitor();
            OrderRefundQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }


        private void SetDefaultTime()
        {
            if (time_type == 1)
            {
                string start = " 00:00:00";
                string end = " 23:59:59";

                string DateStr = DateTime.Now.ToString("yyyy-MM-dd");

                datePicker2.Value = DateTime.Parse(DateStr + start);
                datePicker1.Value = DateTime.Parse(DateStr + end);
            }
        }


        private void today_Click(object sender, RoutedEventArgs e)
        {
            time_type = 1;

            string start = " 00:00:00";
            string end = " 23:59:59";

            string DateStr = DateTime.Now.ToString("yyyy-MM-dd");

            datePicker2.Value = DateTime.Parse(DateStr + start);
            datePicker1.Value = DateTime.Parse(DateStr + end);
        }

        private void yesterday_Click(object sender, RoutedEventArgs e)
        {
            time_type = 2;
            string start = " 00:00:00";
            string DateStr = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

            datePicker2.Value = DateTime.Parse(DateStr + start);
            datePicker1.Value = DateTime.Today.AddSeconds(-1);
        }

        private void Nearly7days_Click(object sender, RoutedEventArgs e)
        {
            time_type = 3;
            string start = " 00:00:00";
            string DateStr = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

            datePicker2.Value = DateTime.Parse(DateStr + start);
            datePicker1.Value = DateTime.Today.AddSeconds(-1);
        }

        private void Nearly30days_Click(object sender, RoutedEventArgs e)
        {
            time_type = 4;
            string start = " 00:00:00";
            string DateStr = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

            datePicker2.Value = DateTime.Parse(DateStr + start);
            datePicker1.Value = DateTime.Today.AddSeconds(-1);
        }

        #region 输入编辑访客姓名 控件滑屏操作
        private void scrollViewer1_TouchDown(object sender, TouchEventArgs e)
        {

        }

        private void scrollViewer1_TouchMove(object sender, TouchEventArgs e)
        {

        }

        private void scrollViewer1_TouchUp(object sender, TouchEventArgs e)
        {

        }
        #endregion

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {

        }

        private void btPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            export = 0;
            if (current > 1)
            {
                current--;
            }
            else
            {
                //btPreviousPage.IsEnabled = false;
                return;
            }
            pageInfo.CurrentPage = current;
            int tmpTotal = 0;
            string result = null;
            Var.order_id = tBoxInputID.Text.Trim();
            Var.g_all_tran_refund = new Var.all_tran_refund[128];
            Var.all_tran_refund_count = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetRefund_QueryList(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id, Var.refund_order_id, Var.store_id, Var.operator_id, Var.device_id, Var.merchant_id, status, export, out result, out Var.g_all_tran_refund, out Var.all_tran_refund_count, out tmpTotal) == false)
            {
                return;
            }

            OrderRefundQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }

        private void btNextPage_Click(object sender, RoutedEventArgs e)
        {
            export = 0;
            if(current* page >= gRefundQueryList_total)
            {

                return; 
            }
            else
            {
                current++;
            }
            pageInfo.CurrentPage = current;

            string result = null;
            Var.order_id = tBoxInputID.Text.Trim();
            Var.g_all_tran_list = new Var.all_tran_list[128];
            Var.all_tran_list_count = 0;
            int tmpTotal = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetRefund_QueryList(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id, Var.refund_order_id, Var.store_id, Var.operator_id, Var.device_id, Var.merchant_id, status, export, out result, out Var.g_all_tran_refund, out Var.all_tran_refund_count, out tmpTotal) == false)
            {
                return;
            }

            OrderRefundQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }

        private void btTargetPage_Click(object sender, RoutedEventArgs e)
        {
            int iTarget = int.Parse(this.txTargetPage.Text);
            export = 0;

            if (iTarget * page > gRefundQueryList_total)
            {
                if (iTarget <= gTotalPage)
                {
                    current = iTarget;
                }
                else
                {
                    return;
                }
            }
            else
            {
                current = iTarget;
            }
            pageInfo.CurrentPage = current;

            string result = null;
            Var.order_id = tBoxInputID.Text.Trim();
            Var.g_all_tran_refund = new Var.all_tran_refund[128];
            Var.all_tran_refund_count = 0;
            int tmpTotal = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetRefund_QueryList(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id, Var.refund_order_id, Var.store_id, Var.operator_id, Var.device_id, Var.merchant_id, status, export, out result, out Var.g_all_tran_refund, out Var.all_tran_refund_count, out tmpTotal) == false)
            {
                return;
            }

            OrderRefundQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }
        public void exPortExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.XLS)|*.xls"; ;
            if ((bool)(saveFileDialog.ShowDialog()))
            {
                try
                {
                    _excelHelper.SaveToExcel(saveFileDialog.FileName, ExcelHeader());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败：" + ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("导出成功", "系统提示", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }
        private void bt__exPortExcel_Click(object sender, RoutedEventArgs e)
        {
            //export = 0;
            string result = null;
            Var.g_all_tran_refund = new Var.all_tran_refund[128];
            Var.all_tran_refund_count = 0;
            int tmpTotal = 0;

            //获取总条数
            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');
 

            //if (PayApi.ApiGetRefund_QueryList(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id, Var.refund_order_id, Var.store_id, Var.operator_id, Var.device_id, Var.merchant_id, status, export, out result, out Var.g_all_tran_refund, out Var.all_tran_refund_count, out tmpTotal) == false)
            //{
            //    return;
            //}

            //if (tmpTotal > 0)
            //{
                Var.g_all_tran_refund = new Var.all_tran_refund[1024*1024];

                export = 1;
                if (PayApi.ApiGetRefund_QueryList(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id, Var.refund_order_id, Var.store_id, Var.operator_id, Var.device_id, Var.merchant_id, status, export, out result, out Var.g_all_tran_refund, out Var.all_tran_refund_count, out tmpTotal) == false)
                {
                    return;
                }

                exPortExcel();
            //}

            
        }

        private void tBoxRefundOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tBoxRefundOrder.Text.Length > 0)
                lbInputRefundID.Content = "";
            else
                lbInputRefundID.Content = "搜索关键字";
        }

        private void bt_refund_Click(object sender, RoutedEventArgs e)
        {            
            if (tBoxRefundOrder.Text.Length == 0)
            {
                return;
            }

            Refund refund = new Refund();
            refund.getOrderId = tBoxRefundOrder.Text;
            refund.ShowDialog();
        }




        //private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    datePicker2.Text = (DateTime.Parse(datePicker2.Text)).ToString("yyyy-MM-dd");            
        //}


        //private void tBoxRefundAmount_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (tBoxRefundAmount.Text.Length > 0)
        //        lbInputRefundAmount.Content = "";
        //    else
        //        lbInputRefundAmount.Content = "搜索关键字";
        //}

        private void updateRefundStatus_Click(object sender, RoutedEventArgs e)
        {
            string refund_order_id = string.Empty;
            string status = string.Empty;
            string remark = string.Empty;
            string merchant_id = string.Empty;
            string tmp = string.Empty;
            string msg = string.Empty;
            PayInterface payInterface = new PayInterface();
            DataRowView dr = OrderRefundQueryPage.SelectedItem as DataRowView;

            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, string> describe = new Dictionary<string, string>();

            bool bret = PayApi.ApiGet_all_RefundStatus(Var.ltoken, out msg, out describe);
            if (!bret)
            {
                MessageBox.Show("获取状态失败！", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (dr != null)
            {
                refund_order_id = (dr["refund_order_id"].ToString());
                merchant_id = (dr["merchant_id"].ToString());

                if (payInterface.ApiRefundStatusQuery(refund_order_id, merchant_id, out result) == false)
                {
                    return;
                }

                result.TryGetValue("status", out tmp);
                result.TryGetValue("msg", out remark);
                describe.TryGetValue(tmp, out status);
                dr["_status"] = status;
                //dr["remark"] = remark;
                MessageBox.Show(remark, "系统提示", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("操作失败", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultTime();
        }

        private void cmb_Order_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object obj = (object)e.AddedItems;
                string str = ((object[])(obj))[0].ToString();

                //获取status
                if (cmb_Order_Status.SelectedIndex == -1)
                {
                    status = "3";
                }
                else
                {
                    if (cmb_Order_Status.SelectedIndex == 0)
                    {
                        status = "";
                    }
                    else
                    {
                        status = Var.g_all_payment_refund_status.FirstOrDefault(q => q.Value == str).Key;
                    }                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmb_Payment_Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object obj = (object)e.AddedItems;
                string str = ((object[])(obj))[0].ToString();

                //获取status
                if (cmb_Payment_Mode.SelectedIndex <= 0)
                {
                    cmbPaymentMode_Key = "";
                }
                else
                {
                    cmbPaymentMode_Key = Var.g_all_payment_channel.FirstOrDefault(q => q.Value == str).Key;    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmbPaymentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object obj = (object)e.AddedItems;
                string str = ((object[])(obj))[0].ToString();

                //获取status
                if (cmbPaymentType.SelectedIndex <= 0)
                {
                    cmb_Payment_Type_Key = "";
                }
                else
                {
                    cmb_Payment_Type_Key = Var.g_all_payment_list.FirstOrDefault(q => q.Value == str).Key;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
