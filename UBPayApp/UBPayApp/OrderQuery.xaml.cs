using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
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
using System.Windows.Controls.Primitives;


namespace UBPayApp
{
    /// <summary>
    /// OrderQuery.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class OrderQuery : Page
    {
        public OrderQuery()
        {
            InitializeComponent();

            cmbPaymentMode_Key = "";
            cmb_Payment_Type_Key = "";
            //store_id_key = "";
            //operator_id_key = "";
            //device_id_key = "";
            status = "";


            today.IsChecked = true;
        }

        private ExportToExcel _excelHelper;
        int gOrder_Query_total = 0;
        PageInfo pageInfo = new PageInfo();

        Thread procThreadProcess = null;
        IniFile ParmIni;

        bool store_flag = true;
        bool user_flag = true;
        bool device_flag = true;

        private static string cmbPaymentMode_Key = string.Empty;
        private static string cmb_Payment_Type_Key = string.Empty;
        private static string store_id_key = string.Empty;
        private static string operator_id_key = string.Empty;
        private static string device_id_key = string.Empty;
        private static string status = string.Empty;


        //private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    datePicker1.Text = (DateTime.Parse(datePicker1.Text)).ToString("yyyy-MM-dd");
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
            dt.Columns.Add(new DataColumn("id", typeof(string)));
            dt.Columns.Add(new DataColumn("order_id", typeof(string)));
            dt.Columns.Add(new DataColumn("store_id", typeof(string)));
            dt.Columns.Add(new DataColumn("store_name", typeof(string)));
            dt.Columns.Add(new DataColumn("operator_id", typeof(string)));
            dt.Columns.Add(new DataColumn("operator_name", typeof(string)));
            dt.Columns.Add(new DataColumn("device_id", typeof(string)));


            dt.Columns.Add(new DataColumn("device_name", typeof(string)));
            dt.Columns.Add(new DataColumn("merchant_id", typeof(string)));
            dt.Columns.Add(new DataColumn("channel_name", typeof(string)));
            dt.Columns.Add(new DataColumn("payment_name", typeof(string)));
            dt.Columns.Add(new DataColumn("amount", typeof(string)));
            dt.Columns.Add(new DataColumn("actual_amount", typeof(string)));
            dt.Columns.Add(new DataColumn("refund_amount", typeof(string)));
            dt.Columns.Add(new DataColumn("subject", typeof(string)));


            dt.Columns.Add(new DataColumn("body", typeof(string)));
            dt.Columns.Add(new DataColumn("_status", typeof(string)));
            dt.Columns.Add(new DataColumn("remark", typeof(string)));
            dt.Columns.Add(new DataColumn("time_create", typeof(string)));

            //不显示，详细信息才显示
            dt.Columns.Add(new DataColumn("out_order_id", typeof(string)));
            dt.Columns.Add(new DataColumn("out_username", typeof(string)));
            dt.Columns.Add(new DataColumn("out_user_id", typeof(string)));
            dt.Columns.Add(new DataColumn("agent_id", typeof(string)));
            dt.Columns.Add(new DataColumn("channel", typeof(string)));
            dt.Columns.Add(new DataColumn("payment", typeof(string)));
            dt.Columns.Add(new DataColumn("time_update", typeof(string)));
            dt.Columns.Add(new DataColumn("status", typeof(string)));




            for (int i = 0; i < Var.all_tran_list_count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = Var.g_all_tran_list[i].id;
                dr["order_id"] = Var.g_all_tran_list[i].order_id;
                dr["store_id"] = Var.g_all_tran_list[i].store_id;
                dr["store_name"] = Var.g_all_tran_list[i].store_name;
                dr["operator_id"] = Var.g_all_tran_list[i].operator_id;
                dr["operator_name"] = Var.g_all_tran_list[i].operator_name;
                dr["device_id"] = Var.g_all_tran_list[i].device_id;

                dr["device_name"] = Var.g_all_tran_list[i].device_name;
                dr["merchant_id"] = Var.g_all_tran_list[i].merchant_id;
                dr["channel_name"] = Var.g_all_tran_list[i].channel_name;
                dr["payment_name"] = Var.g_all_tran_list[i].channel_name;
                dr["amount"] = Var.g_all_tran_list[i].amount;
                dr["actual_amount"] = Var.g_all_tran_list[i].actual_amount;
                dr["refund_amount"] = Var.g_all_tran_list[i].refund_amount;
                dr["subject"] = Var.g_all_tran_list[i].subject;

                dr["body"] = Var.g_all_tran_list[i].body;
                dr["_status"] = Var.g_all_tran_list[i]._status;
                dr["remark"] = Var.g_all_tran_list[i].remark;
                dr["time_create"] = Var.g_all_tran_list[i].time_create;

                //不显示，详细信息才显示
                dr["out_order_id"] = Var.g_all_tran_list[i].out_order_id;
                dr["out_username"] = Var.g_all_tran_list[i].out_username;
                dr["out_user_id"] = Var.g_all_tran_list[i].out_user_id;
                dr["agent_id"] = Var.g_all_tran_list[i].agent_id;
                dr["channel"] = Var.g_all_tran_list[i].channel;
                dr["payment"] = Var.g_all_tran_list[i].payment;
                dr["time_update"] = Var.g_all_tran_list[i].time_update;
                dr["status"] = Var.g_all_tran_list[i].status;

                dt.Rows.Add(dr);
            }
            
            return dt;
            //OrderQueryPage.ItemsSource = dt.DefaultView;
        }

        private DataTable ExcelHeader()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("id", typeof(string)));
            dt.Columns.Add(new DataColumn("订单编号", typeof(string)));
            dt.Columns.Add(new DataColumn("门店ID", typeof(string)));
            dt.Columns.Add(new DataColumn("门店名称", typeof(string)));
            dt.Columns.Add(new DataColumn("操作员ID", typeof(string)));
            dt.Columns.Add(new DataColumn("操作员名称", typeof(string)));
            dt.Columns.Add(new DataColumn("设备ID", typeof(string)));


            dt.Columns.Add(new DataColumn("设备名称", typeof(string)));
            dt.Columns.Add(new DataColumn("商户ID", typeof(string)));
            dt.Columns.Add(new DataColumn("支付方式", typeof(string)));
            dt.Columns.Add(new DataColumn("交易类型", typeof(string)));
            dt.Columns.Add(new DataColumn("订单金额", typeof(string)));
            dt.Columns.Add(new DataColumn("实际交易金额", typeof(string)));
            dt.Columns.Add(new DataColumn("退款金额", typeof(string)));
            dt.Columns.Add(new DataColumn("订单标题", typeof(string)));


            dt.Columns.Add(new DataColumn("商品信息", typeof(string)));
            dt.Columns.Add(new DataColumn("订单状态", typeof(string)));
            dt.Columns.Add(new DataColumn("订单备注", typeof(string)));
            dt.Columns.Add(new DataColumn("订单发起时间", typeof(string)));

            //不显示，详细信息才显示
            //dt.Columns.Add(new DataColumn("out_order_id", typeof(string)));
            //dt.Columns.Add(new DataColumn("out_username", typeof(string)));
            //dt.Columns.Add(new DataColumn("out_user_id", typeof(string)));
            //dt.Columns.Add(new DataColumn("agent_id", typeof(string)));
            //dt.Columns.Add(new DataColumn("channel", typeof(string)));
            //dt.Columns.Add(new DataColumn("payment", typeof(string)));
            //dt.Columns.Add(new DataColumn("time_update", typeof(string)));
            //dt.Columns.Add(new DataColumn("status", typeof(string)));




            for (int i = 0; i < Var.all_tran_list_count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = Var.g_all_tran_list[i].id;
                dr["订单编号"] = "'" + Var.g_all_tran_list[i].order_id;
                dr["门店ID"] = "'" + Var.g_all_tran_list[i].store_id;
                dr["门店名称"] = Var.g_all_tran_list[i].store_name;
                dr["操作员ID"] = "'" + Var.g_all_tran_list[i].operator_id;
                dr["操作员名称"] = Var.g_all_tran_list[i].operator_name;
                dr["设备ID"] = "'" + Var.g_all_tran_list[i].device_id;

                dr["设备名称"] = Var.g_all_tran_list[i].device_name;
                dr["商户ID"] = "'" + Var.g_all_tran_list[i].merchant_id;
                dr["支付方式"] = Var.g_all_tran_list[i].channel_name;
                dr["交易类型"] = Var.g_all_tran_list[i].channel_name;
                dr["订单金额"] = Var.g_all_tran_list[i].amount;
                dr["实际交易金额"] = Var.g_all_tran_list[i].actual_amount;
                dr["退款金额"] = Var.g_all_tran_list[i].refund_amount;
                dr["订单标题"] = Var.g_all_tran_list[i].subject;

                dr["商品信息"] = Var.g_all_tran_list[i].body;
                dr["订单状态"] = Var.g_all_tran_list[i]._status;
                dr["订单备注"] = Var.g_all_tran_list[i].remark;
                dr["订单发起时间"] = Var.g_all_tran_list[i].time_create;

                //不显示，详细信息才显示
                //dr["out_order_id"] = Var.g_all_tran_list[i].out_order_id;
                //dr["out_username"] = Var.g_all_tran_list[i].out_username;
                //dr["out_user_id"] = Var.g_all_tran_list[i].out_user_id;
                //dr["agent_id"] = Var.g_all_tran_list[i].agent_id;
                //dr["channel"] = Var.g_all_tran_list[i].channel;
                //dr["payment"] = Var.g_all_tran_list[i].payment;
                //dr["time_update"] = Var.g_all_tran_list[i].time_update;
                //dr["status"] = Var.g_all_tran_list[i].status;

                dt.Rows.Add(dr);
            }

            return dt;
            //OrderQueryPage.ItemsSource = dt.DefaultView;
        }

        /// <param name="iPage">每页多少条</param>
        /// <param name="Current">当前第几页</param>
        /// <param name="time_type">时间类型1当天 2昨天 3本周 4本月 为空则按照时间段查询</param>
        /// time_start:2018-06-12 00:00:00 开始时间
        /// time_end:2018-07-23 00:00:00 结束时间
        /// <param name="channel">支付方式 使用支付方式接口获取</param>
        /// <param name="payment">交易类型 使用交易类型接口获取</param>
        /// <param name="order_id">订单号</param>
        /// <param name="store_id">门店id 使用门店列表接口获取</param>
        /// <param name="operator_id">店员id 使用操作员列表接口获取</param>
        /// <param name="device_id">设备id 使用设备列表接口获取</param>
        /// <param name="merchant_id">商户id</param>
        /// <param name="status">通过订单状态接口获取</param>

        int page = 5;
        int current = 1;
        int time_type = 1;
        int gTotalPage = 0;
        int export = 0; //为1则返回所有记录

        

        private readonly RisCaptureLib.ScreenCaputre screenCaputre = new RisCaptureLib.ScreenCaputre();

        private void bt__OrderQuery_Click(object sender, RoutedEventArgs e)
        {
            OrderQueryPage.ItemsSource = null;
            OrderQueryPage.Items.Clear();

            string result = null;
            Var.order_id = tBoxInputID.Text.Trim();
            Var.g_all_tran_list = new Var.all_tran_list[128];
            Var.all_tran_list_count = 0;
            int tmpTotal = 0;
            export = 0;
            //加密通了，现在返回 商户号不可为空
            //PayInterface payInterface = new PayInterface();
            //payInterface.InterfaceAddOrder("barcode", "ord20180904220401", "0.1", "136029929406236668", "test", "testbody", "1000000001", "1000000001" "1000000001", "1000000001", "bf0ac37accfb6ed19d4676a54d6aaaf0", out result);

            //要改为获取矩形左上角坐标，和宽高，为后面自动截屏使用          
            //screenCaputre.StartCaputre(30);

            //默认是按当天来查询的     //删除，如果在界面上改时间，这里还是会从00开始       
            //SetDefaultTime();

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');


            if (PayApi.ApiGetOrder_List(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id, 
                store_id_key, operator_id_key, device_id_key, Var.merchant_id, status, export, out result, out Var.g_all_tran_list, 
                out Var.all_tran_list_count, out tmpTotal) == false)
            {
                return;
            }    


            gOrder_Query_total = tmpTotal;

            DisplayQueryPage.DataContext = pageInfo;

            if (gOrder_Query_total == 0)
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
            OrderQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
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
                btPreviousPage.IsEnabled = false;
                return;
            }

            pageInfo.CurrentPage = current;

            int tmpTotal = 0;
            string result = null;
            Var.order_id = tBoxInputID.Text.Trim();
            Var.g_all_tran_list = new Var.all_tran_list[128];
            Var.all_tran_list_count = 0;


            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');


            if (PayApi.ApiGetOrder_List(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id,
                store_id_key, operator_id_key, device_id_key, Var.merchant_id, status, export, out result, out Var.g_all_tran_list,
                out Var.all_tran_list_count, out tmpTotal) == false)
            {
                return;
            }

            OrderQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }

        private void btNextPage_Click(object sender, RoutedEventArgs e)
        {
            export = 0;
            if(current* page >= gOrder_Query_total)
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

            if (PayApi.ApiGetOrder_List(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id,
                store_id_key, operator_id_key, device_id_key, Var.merchant_id, status, export, out result, out Var.g_all_tran_list,
                out Var.all_tran_list_count, out tmpTotal) == false)
            {
                return;
            }

            OrderQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }

        private void btTargetPage_Click(object sender, RoutedEventArgs e)
        {
            int iTarget = int.Parse(this.txTargetPage.Text);
            export = 0;

            if (iTarget * page > gOrder_Query_total)
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
            Var.g_all_tran_list = new Var.all_tran_list[128];
            Var.all_tran_list_count = 0;
            int tmpTotal = 0;


            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');


            if (PayApi.ApiGetOrder_List(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id,
                store_id_key, operator_id_key, device_id_key, Var.merchant_id, status, export, out result, out Var.g_all_tran_list,
                out Var.all_tran_list_count, out tmpTotal) == false)
            {
                return;
            }

            OrderQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
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
                    MessageBox.Show("导出失败：" + ex.Message);
                }
                MessageBox.Show("导出成功");
            }
        }

        private void bt__exPortExcel_Click(object sender, RoutedEventArgs e)
        {
            //export = 0;
            string result = null;
            Var.g_all_tran_list = new Var.all_tran_list[128];
            Var.all_tran_list_count = 0;
            int tmpTotal = 0;


            //获取总条数
            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');


            //if (PayApi.ApiGetOrder_List(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id,
            //    store_id_key, operator_id_key, device_id_key, Var.merchant_id, status, export, out result, out Var.g_all_tran_list,
            //    out Var.all_tran_list_count, out tmpTotal) == false)
            //{
            //    return;
            //}

            //if (tmpTotal > 0)
            //{
                Var.g_all_tran_list = new Var.all_tran_list[1024*1024];

                export = 1;
                if (PayApi.ApiGetOrder_List(Var.ltoken, page, current, time_type, time_start, time_end, cmbPaymentMode_Key, cmb_Payment_Type_Key, Var.order_id,
                    store_id_key, operator_id_key, device_id_key, Var.merchant_id, status, export, out result, out Var.g_all_tran_list,
                    out Var.all_tran_list_count, out tmpTotal) == false)
                {
                    return;
                }

                exPortExcel();
            //}
            //else
            //{
            //    MessageBox.Show("无法导出，数据条目为空！");
            //}
            
        }

        private void DetailedInformation_Click(object sender, RoutedEventArgs e)
        {
            Var.gOrderInfo = OrderQueryPage.SelectedItem as DataRowView;
            OrderDetails child = new OrderDetails();
            child.ShowDialog();
        }

        private void updateInformation_Click(object sender, RoutedEventArgs e)
        {
            string order_id = string.Empty;            
            string status = string.Empty;
            string remark = string.Empty;
            string merchant_id = string.Empty;
            string tmp = string.Empty;
            string msg = string.Empty;
            PayInterface payInterface = new PayInterface();
            DataRowView dr = OrderQueryPage.SelectedItem as DataRowView;

            Dictionary<string, string> result = new Dictionary<string, string>();            

            //bool bret = PayApi.ApiGet_all_OrderStatus(Var.ltoken, out msg, out Var.g_all_payment_Order_Status);
            //if (!bret)
            //{
            //    MessageBox.Show("获取状态失败！");
            //}

            if (dr != null)
            {
                order_id = (dr["order_id"].ToString());
                merchant_id = (dr["merchant_id"].ToString());

                if (payInterface.ApiOrderStatusQuery(order_id, merchant_id, out result) == false)
                {
                    MessageBox.Show("查询失败");
                    return;
                }

                result.TryGetValue("status", out tmp);
                result.TryGetValue("msg", out remark);
                Var.g_all_payment_Order_Status.TryGetValue(tmp, out status);
                dr["_status"] = status;
                dr["remark"] = remark;
            }
            else
            {
                MessageBox.Show("操作失败");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultTime();

            ParmIni = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"/Parm.ini");
            string stemp = ParmIni.IniReadValue("Init", "store_id");
            int iPos = 0;

            // 门店
            cmb_store_id.Items.Add("所有门店");
            for (int i = 0; i < Var.Get_StoreListCount; i++)
            {
                cmb_store_id.Items.Add(Var.g_StoreList_Info[i].name);
                if (stemp.Trim() == Var.g_StoreList_Info[i].id)
                {
                    iPos = i;
                }
            }
            cmb_store_id.SelectedIndex = iPos+1;
            store_id_key = Var.g_StoreList_Info[cmb_store_id.SelectedIndex-1].id;
            store_flag = false;


            ////modify 20181104 在选择相应门店的时候，才列出相应店员
            //// 店员
            cmb_operator_id.Items.Clear();
            cmb_operator_id.Items.Add("所有店员");
            stemp = ParmIni.IniReadValue("Init", "operator_id");
            for (int i = 0; i < Var.Get_UserListCount; i++)
            {
                cmb_operator_id.Items.Add(Var.g_UserList_Info[i].username);
                if (stemp.Trim() == Var.g_UserList_Info[i].id)
                {
                    iPos = i;
                }
            }
            cmb_operator_id.SelectedIndex = iPos+1;
            operator_id_key = Var.g_UserList_Info[cmb_operator_id.SelectedIndex - 1].id;
            user_flag = false;



            //modify 20181104 在选择相应门店时，列出所有设备ID
            //// 设备
            cmb_device_id.Items.Clear();
            cmb_device_id.Items.Add("所有设备");
            stemp = ParmIni.IniReadValue("Init", "device_id");
            for (int i = 0; i < Var.Get_DeviceListCount; i++)
            {
                cmb_device_id.Items.Add(Var.g_DeviceList_Info[i].name);
                if (stemp.Trim() == Var.g_DeviceList_Info[i].id)
                {
                    iPos = i;
                }
            }
            cmb_device_id.SelectedIndex = iPos+1;
            device_id_key = Var.g_DeviceList_Info[cmb_device_id.SelectedIndex - 1].id;
            device_flag = false;

            //交易类型列表
            //for (int i = 0; i < Var.all_payment_list_count; i++)
            //{
            //    cmb_Payment_Type.Items.Add(Var.g_all_payment_list[i].payment_name);
            //}
            //cmb_Payment_Type.SelectedIndex = 0;

            cmb_Payment_Type.Items.Add("所有交易类型");
            foreach (KeyValuePair<string, string> pair in Var.g_all_payment_list)
            {
                cmb_Payment_Type.Items.Add(pair.Value);
            }

            //支付方式列表
            cmbPaymentMode.Items.Add("所有支付方式");
            foreach (KeyValuePair<string, string> pair in Var.g_all_payment_channel)
            {
                cmbPaymentMode.Items.Add(pair.Value);
            }
            //cmbPaymentMode.SelectedIndex = 0;

            //订单状态列表
            cmb_Order_Status.Items.Add("所有订单状态");
            foreach (KeyValuePair<string, string> pair in Var.g_all_payment_Order_Status)
            {
                cmb_Order_Status.Items.Add(pair.Value);
            }
            //cmb_Order_Status.SelectedIndex = 0;

            _excelHelper = new ExportToExcel();

        }

        private void GetUserListAndDisplay()
        {
            string result = string.Empty;
            //modify 20181104 放在设置时查询，不同的门店有不同的用户ID和设备ID
            Var.g_UserList_Info = new Var.UserList_Info[128];
            Var.Get_UserListCount = 0;
            if (PayApi.ApiGetUserList(store_id_key, Var.ltoken, out result, out Var.g_UserList_Info, out Var.Get_UserListCount) == false)
            {
                MessageBox.Show("获取店员列表：" + result);
                return;
            }
            else
            {
                cmb_operator_id.Items.Clear();
                cmb_operator_id.Items.Add("所有店员");
                for (int i = 0; i < Var.Get_UserListCount; i++)
                {
                    cmb_operator_id.Items.Add(Var.g_UserList_Info[i].username);
                }
                cmb_operator_id.SelectedIndex = 1;
            }
        }

        private void GetDeviceListAndDisplay()
        {
            string result = string.Empty;
            Var.g_DeviceList_Info = new Var.DeviceList_Info[128];
            Var.Get_DeviceListCount = 0;
            if (PayApi.ApiGetDeviceList(store_id_key, Var.ltoken, out result, out Var.g_DeviceList_Info, out Var.Get_DeviceListCount) == false)
            {
                MessageBox.Show("获取设备列表：" + result);
                return;
            }
            else
            {
                cmb_device_id.Items.Clear();
                cmb_device_id.Items.Add("所有设备");
                for (int i = 0; i < Var.Get_DeviceListCount; i++)
                {
                    cmb_device_id.Items.Add(Var.g_DeviceList_Info[i].name);
                }
                cmb_device_id.SelectedIndex = 1;
            }
        }

        private void cmb_store_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!store_flag)
            {
                if (cmb_store_id.SelectedIndex >= 1)
                {                    
                    store_id_key = Var.g_StoreList_Info[cmb_store_id.SelectedIndex-1].id;
                    GetUserListAndDisplay();
                    GetDeviceListAndDisplay();
                }
                else
                {
                    store_id_key = "";
                    cmb_operator_id.SelectedIndex = 0;
                    cmb_device_id.SelectedIndex = 0;
                }
            }
        }

        private void cmb_operator_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!user_flag)
            {
                if (cmb_operator_id.SelectedIndex <= 0)
                {
                    //Var.operator_id = Var.g_UserList_Info[0].id;
                    operator_id_key = "";
                    return;
                }
                else
                {
                    operator_id_key = Var.g_UserList_Info[cmb_operator_id.SelectedIndex - 1].id;  
                }
            }
        }

        private void cmb_device_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!device_flag)
            {
                if (cmb_device_id.SelectedIndex <= 0)
                {
                    device_id_key = "";
                    return;
                }
                else
                {
                    device_id_key = Var.g_DeviceList_Info[cmb_device_id.SelectedIndex - 1].id;   
                }
            }
        }

        private void cmb_Order_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object obj = (object)e.AddedItems;
                string str = ((object[])(obj))[0].ToString();

                //获取status
                if (cmb_Order_Status.SelectedIndex <= -1)
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
                        status = Var.g_all_payment_Order_Status.FirstOrDefault(q => q.Value == str).Key;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void cmbPaymentMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object obj = (object)e.AddedItems;
                string str = ((object[])(obj))[0].ToString();

                //获取status
                if (cmbPaymentMode.SelectedIndex <= 0)
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

        private void cmb_Payment_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object obj = (object)e.AddedItems;
                string str = ((object[])(obj))[0].ToString();

                //获取status
                if (cmb_Payment_Type.SelectedIndex <= 0)
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
    }//end class





    //public static class DataGridPlus
    //{
    //    /// <summary>
    //    /// 获取DataGrid控件单元格
    //    /// </summary>
    //    /// <param name="dataGrid">DataGrid控件</param>
    //    /// <param name="rowIndex">单元格所在的行号</param>
    //    /// <param name="columnIndex">单元格所在的列号</param>
    //    /// <returns>指定的单元格</returns>
    //    public static DataGridCell GetCell(this DataGrid dataGrid, int rowIndex, int columnIndex)
    //    {
    //        DataGridRow rowContainer = dataGrid.GetRow(rowIndex);
    //        if (rowContainer != null)
    //        {

    //            DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
    //            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
    //            if (cell == null)
    //            {
    //                dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[columnIndex]);
    //                cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
    //            }
    //            return cell;
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// 获取DataGrid的行
    //    /// </summary>
    //    /// <param name="dataGrid">DataGrid控件</param>
    //    /// <param name="rowIndex">DataGrid行号</param>
    //    /// <returns>指定的行号</returns>
    //    public static DataGridRow GetRow(this DataGrid dataGrid, int rowIndex)
    //    {
    //        DataGridRow rowContainer = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
    //        if (rowContainer == null)
    //        {
    //            dataGrid.UpdateLayout();
    //            dataGrid.ScrollIntoView(dataGrid.Items[rowIndex]);
    //            rowContainer = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
    //        }
    //        return rowContainer;
    //    }

    //    /// <summary>
    //    /// 获取父可视对象中第一个指定类型的子可视对象
    //    /// </summary>
    //    /// <typeparam name="T">可视对象类型</typeparam>
    //    /// <param name="parent">父可视对象</param>
    //    /// <returns>第一个指定类型的子可视对象</returns>
    //    public static T GetVisualChild<T>(Visual parent) where T : Visual
    //    {
    //        T child = default(T);
    //        int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
    //        for (int i = 0; i < numVisuals; i++)
    //        {
    //            Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
    //            child = v as T;
    //            if (child == null)
    //            {
    //                child = GetVisualChild<T>(v);
    //            }
    //            if (child != null)
    //            {
    //                break;
    //            }
    //        }
    //        return child;
    //    }
    //}
}
