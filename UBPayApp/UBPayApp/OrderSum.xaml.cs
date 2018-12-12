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
    public class PageInfo : INotifyPropertyChanged
    {
        private int _TotalPage = 0;
        private int _CurrentPage = 0;
        private int _TargetPage = 0;

        public int CurrentPage
        {
            set
            {
                _CurrentPage = value;
                if (PropertyChanged != null)//有改变
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }
            get
            {
                return _CurrentPage;
            }
        }

        public int TotalPage
        {
            set
            {
                _TotalPage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalPage"));
                }
            }
            get
            {
                return _TotalPage;
            }
        }

        public int TargetPage
        {
            set
            {
                _TargetPage = value;
                if (PropertyChanged != null)//有改变
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TargetPage"));
                }
            }
            get
            {
                return _TargetPage;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class OrderSum : Page 
    {
        public OrderSum()
        {
            InitializeComponent();

            today.IsChecked = true;
            //for (int i = 0; i < Var.all_payment_list_count; i++)
            //{
            //    cmbPaymentType.Items.Add(Var.g_all_payment_list[i].payment_name);
            //}
            //cmbPaymentType.SelectedIndex = 0;

            _excelHelper = new ExportToExcel();

        }
        private ExportToExcel _excelHelper;

        int gOrder_SumList_total = 0;
        PageInfo pageInfo = new PageInfo();
        

        //private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    datePicker1.Text = (DateTime.Parse(datePicker1.Text)).ToString("yyyy-MM-dd");
        //}

        private void set_label_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }

        //private void tBoxInputID_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (tBoxInputID.Text.Length > 0)
        //        lbInputID.Content = "";
        //    else
        //        lbInputID.Content = "搜索关键字";
        //}

        //List<Var.all_tran_sum> gall_tran_sum = new List<Var.all_tran_sum>();
        //private void LoadDispVisitor()
        //{ 
        //    for (int i = 0; i < page; i++)
        //    {
        //        Var.all_tran_sum tmp_sum = new Var.all_tran_sum();
        //        tmp_sum.id = Var.g_all_tran_sum[i].id;
        //        tmp_sum.agent_id = Var.g_all_tran_sum[i].agent_id;
        //        tmp_sum.merchant_id = Var.g_all_tran_sum[i].merchant_id;
        //        tmp_sum.merchant_name = Var.g_all_tran_sum[i].merchant_name;
        //        tmp_sum.operator_id = Var.g_all_tran_sum[i].operator_id;
        //        tmp_sum.time_create = Var.g_all_tran_sum[i].time_create;
        //        tmp_sum.date = Var.g_all_tran_sum[i].date;
        //        tmp_sum.count_weixin = Var.g_all_tran_sum[i].count_weixin;
        //        tmp_sum.sum_weixin = Var.g_all_tran_sum[i].sum_weixin;
        //        tmp_sum.count_alipay = Var.g_all_tran_sum[i].count_alipay;
        //        tmp_sum.sum_alipay = Var.g_all_tran_sum[i].sum_alipay;
        //        tmp_sum.count_unionpay = Var.g_all_tran_sum[i].count_unionpay;
        //        tmp_sum.sum_unionpay = Var.g_all_tran_sum[i].sum_unionpay;
        //        tmp_sum.count_xt = Var.g_all_tran_sum[i].count_xt;
        //        tmp_sum.sum_xt = Var.g_all_tran_sum[i].sum_xt;
        //        tmp_sum.count_xz_weixin = Var.g_all_tran_sum[i].count_xz_weixin;
        //        tmp_sum.sum_xz_weixin = Var.g_all_tran_sum[i].sum_xz_weixin;
        //        tmp_sum.count_xz_alipay = Var.g_all_tran_sum[i].count_xz_alipay;
        //        tmp_sum.sum_xz_alipay = Var.g_all_tran_sum[i].sum_xz_alipay;
        //        tmp_sum.count_sf_weixin = Var.g_all_tran_sum[i].count_sf_weixin;
        //        tmp_sum.sum_sf_weixin = Var.g_all_tran_sum[i].sum_sf_weixin;
        //        tmp_sum.count_sf_alipay = Var.g_all_tran_sum[i].count_sf_alipay;
        //        tmp_sum.sum_sf_alipay = Var.g_all_tran_sum[i].sum_sf_alipay;
        //        tmp_sum.count_all = Var.g_all_tran_sum[i].count_all;
        //        tmp_sum.sum_all = Var.g_all_tran_sum[i].sum_all;

        //        gall_tran_sum.Add(tmp_sum);
        //    }
        //    OrderSumQueryPage.ItemsSource = gall_tran_sum.Skip(current * 10).Take(page);
        //}
        private DataTable LoadDispVisitor()
        {
      
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("id", typeof(string)));
            dt.Columns.Add(new DataColumn("date", typeof(string)));
            dt.Columns.Add(new DataColumn("count_weixin", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_weixin", typeof(string)));
            dt.Columns.Add(new DataColumn("count_alipay", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_alipay", typeof(string)));
            dt.Columns.Add(new DataColumn("count_unionpay", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_unionpay", typeof(string)));
            dt.Columns.Add(new DataColumn("count_xt", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_xt", typeof(string)));
            dt.Columns.Add(new DataColumn("count_xz_weixin", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_xz_weixin", typeof(string)));
            dt.Columns.Add(new DataColumn("count_xz_alipay", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_xz_alipay", typeof(string)));
            dt.Columns.Add(new DataColumn("count_sf_weixin", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_sf_weixin", typeof(string)));
            dt.Columns.Add(new DataColumn("count_sf_alipay", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_sf_alipay", typeof(string)));
            dt.Columns.Add(new DataColumn("count_all", typeof(string)));
            dt.Columns.Add(new DataColumn("sum_all", typeof(string)));

            for (int i = 0; i < Var.all_tran_sum_count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = Var.g_all_tran_sum[i].id;
                dr["date"] = Var.g_all_tran_sum[i].date;
                dr["count_weixin"] = Var.g_all_tran_sum[i].count_weixin;
                dr["sum_weixin"] = Var.g_all_tran_sum[i].sum_weixin;
                dr["count_alipay"] = Var.g_all_tran_sum[i].count_alipay;
                dr["sum_alipay"] = Var.g_all_tran_sum[i].sum_alipay;
                dr["count_unionpay"] = Var.g_all_tran_sum[i].count_unionpay;

                dr["sum_unionpay"] = Var.g_all_tran_sum[i].sum_unionpay;
                dr["count_xt"] = Var.g_all_tran_sum[i].count_xt;
                dr["sum_xt"] = Var.g_all_tran_sum[i].sum_xt;
                dr["count_xz_weixin"] = Var.g_all_tran_sum[i].count_xz_weixin;
                dr["sum_xz_weixin"] = Var.g_all_tran_sum[i].sum_xz_weixin;
                dr["count_xz_alipay"] = Var.g_all_tran_sum[i].count_xz_alipay;
                dr["sum_xz_alipay"] = Var.g_all_tran_sum[i].sum_xz_alipay;

                dr["count_sf_weixin"] = Var.g_all_tran_sum[i].count_sf_weixin;
                dr["sum_sf_weixin"] = Var.g_all_tran_sum[i].sum_sf_weixin;
                dr["count_sf_alipay"] = Var.g_all_tran_sum[i].count_sf_alipay;
                dr["sum_sf_alipay"] = Var.g_all_tran_sum[i].sum_sf_alipay;
                dr["count_all"] = Var.g_all_tran_sum[i].count_all;
                dr["sum_all"] = Var.g_all_tran_sum[i].sum_all;

                dt.Rows.Add(dr);
            }

            return dt;
            //OrderSumQueryPage.ItemsSource = dt.DefaultView;
        }

        private DataTable ExcelHeader()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("id", typeof(string)));
            dt.Columns.Add(new DataColumn("日期", typeof(string)));
            dt.Columns.Add(new DataColumn("微信笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("微信金额", typeof(string)));
            dt.Columns.Add(new DataColumn("支付宝笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("支付宝金额", typeof(string)));
            dt.Columns.Add(new DataColumn("银联笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("银联金额", typeof(string)));
            dt.Columns.Add(new DataColumn("新天笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("新天金额", typeof(string)));
            dt.Columns.Add(new DataColumn("现在微信笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("现在微信金额", typeof(string)));
            dt.Columns.Add(new DataColumn("现在支付宝笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("现在支付宝金额", typeof(string)));
            dt.Columns.Add(new DataColumn("上福微信笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("上福微信金额", typeof(string)));
            dt.Columns.Add(new DataColumn("上福支付宝笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("上福支付宝金额", typeof(string)));
            dt.Columns.Add(new DataColumn("总笔数", typeof(string)));
            dt.Columns.Add(new DataColumn("总金额", typeof(string)));

            for (int i = 0; i < Var.all_tran_sum_count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = Var.g_all_tran_sum[i].id;
                dr["日期"] = Var.g_all_tran_sum[i].date;
                dr["微信笔数"] = "'" + Var.g_all_tran_sum[i].count_weixin;
                dr["微信金额"] = "'" + Var.g_all_tran_sum[i].sum_weixin;
                dr["支付宝笔数"] = "'" + Var.g_all_tran_sum[i].count_alipay;
                dr["支付宝金额"] = "'" + Var.g_all_tran_sum[i].sum_alipay;
                dr["银联笔数"] = "'" + Var.g_all_tran_sum[i].count_unionpay;

                dr["银联金额"] = "'" + Var.g_all_tran_sum[i].sum_unionpay;
                dr["新天笔数"] = "'" + Var.g_all_tran_sum[i].count_xt;
                dr["新天金额"] = "'" + Var.g_all_tran_sum[i].sum_xt;
                dr["现在微信笔数"] = "'" + Var.g_all_tran_sum[i].count_xz_weixin;
                dr["现在微信金额"] = "'" + Var.g_all_tran_sum[i].sum_xz_weixin;
                dr["现在支付宝笔数"] = "'" + Var.g_all_tran_sum[i].count_xz_alipay;
                dr["现在支付宝金额"] = "'" + Var.g_all_tran_sum[i].sum_xz_alipay;

                dr["现在支付宝金额"] = "'" + Var.g_all_tran_sum[i].count_sf_weixin;
                dr["上福微信金额"] = "'" + Var.g_all_tran_sum[i].sum_sf_weixin;
                dr["上福支付宝笔数"] = "'" + Var.g_all_tran_sum[i].count_sf_alipay;
                dr["上福支付宝金额"] = "'" + Var.g_all_tran_sum[i].sum_sf_alipay;
                dr["总笔数"] = "'" + Var.g_all_tran_sum[i].count_all;
                dr["总金额"] = "'" + Var.g_all_tran_sum[i].sum_all;

                dt.Rows.Add(dr);
            }

            return dt;
            //OrderSumQueryPage.ItemsSource = dt.DefaultView;
        }

        /// <param name="iPage">每页多少条</param>
        /// <param name="Current">当前第几页</param>
        /// <param name="time_type">时间类型1当天 2昨天 3本周 4本月 为空则按照时间段查询</param>
        /// time_start:2018-06-12 00:00:00 开始时间
        /// time_end:2018-07-23 00:00:00 结束时间
        /// <param name="operator_id">店员id 使用操作员列表接口获取</param>
        /// <param name="merchant_id">商户id</param>

        int page = 5;//每页显示条目数
        int current = 1;//当前页的索引
        int time_type = 1;
        int gTotalPage = 0;
        int export = 0; //为1则返回所有记录



        private void bt__OrderSum_Click(object sender, RoutedEventArgs e)
        {
            OrderSumQueryPage.ItemsSource = null;
            OrderSumQueryPage.Items.Clear();
            string result = null;
            Var.g_all_tran_sum = new Var.all_tran_sum[128];
            Var.all_tran_sum_count = 0;
            int tmpTotal = 0;
            export = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetOrder_SumList(Var.ltoken, page, current, time_type, time_start, time_end,Var.operator_id, Var.merchant_id, export,out result, out Var.g_all_tran_sum, out Var.all_tran_sum_count,out tmpTotal) == false)
            {
                return;
            }

            gOrder_SumList_total = tmpTotal;

            DisplayPage.DataContext = pageInfo;

            if (gOrder_SumList_total == 0)
            {
                pageInfo.CurrentPage = 0;
            }
            else
            {
                pageInfo.CurrentPage = current;
            }

            pageInfo.TargetPage = 0;
            pageInfo.TotalPage = gTotalPage = tmpTotal % page == 0 ? tmpTotal / page : tmpTotal / page + 1;
           
            OrderSumQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
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
                return;
            }
            pageInfo.CurrentPage = current;

            int tmpTotal = 0;
            string result = null;
            Var.g_all_tran_sum = new Var.all_tran_sum[128];
            Var.all_tran_sum_count = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetOrder_SumList(Var.ltoken, page, current, time_type, time_start, time_end, Var.operator_id, Var.merchant_id, export, out result, out Var.g_all_tran_sum, out Var.all_tran_sum_count, out tmpTotal) == false)
            {
                return;
            }

            //LoadDispVisitor();
            OrderSumQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }

        private void btNextPage_Click(object sender, RoutedEventArgs e)
        {
            export = 0;
            if (current* page > gOrder_SumList_total)
            {
                return; 
            }
            else
            {
                current++;
            }
            pageInfo.CurrentPage = current;

            string result = null;
            Var.g_all_tran_sum = new Var.all_tran_sum[128];
            Var.all_tran_sum_count = 0;
            int tmpTotal = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetOrder_SumList(Var.ltoken, page, current, time_type, time_start, time_end, Var.operator_id, Var.merchant_id, export, out result, out Var.g_all_tran_sum, out Var.all_tran_sum_count, out tmpTotal) == false)
            {
                return;
            }

            //LoadDispVisitor();
            OrderSumQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
        }

        private void btTargetPage_Click(object sender, RoutedEventArgs e)
        {
            int iTarget = int.Parse(this.txTargetPage.Text);
            export = 0;

            if (iTarget * page > gOrder_SumList_total)
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
            Var.g_all_tran_sum = new Var.all_tran_sum[128];
            Var.all_tran_sum_count = 0;
            int tmpTotal = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            if (PayApi.ApiGetOrder_SumList(Var.ltoken, page, current, time_type, time_start, time_end, Var.operator_id, Var.merchant_id, export, out result, out Var.g_all_tran_sum, out Var.all_tran_sum_count, out tmpTotal) == false)
            {
                return;
            }

            //LoadDispVisitor();
            OrderSumQueryPage.ItemsSource = LoadDispVisitor().DefaultView;
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
            Var.g_all_tran_sum = new Var.all_tran_sum[128];
            Var.all_tran_sum_count = 0;
            int tmpTotal = 0;

            string time_end = datePicker1.Value.ToString().Replace('/', '-');
            string time_start = datePicker2.Value.ToString().Replace('/', '-');

            //if (PayApi.ApiGetOrder_SumList(Var.ltoken, page, current, time_type, time_start, time_end, Var.operator_id, Var.merchant_id, export, out result, out Var.g_all_tran_sum, out Var.all_tran_sum_count, out tmpTotal) == false)
            //{
            //    return;
            //}

            //if (tmpTotal > 0)
            //{
                Var.g_all_tran_sum = new Var.all_tran_sum[1024];

                export = 1;
                if (PayApi.ApiGetOrder_SumList(Var.ltoken, page, current, time_type, time_start, time_end, Var.operator_id, Var.merchant_id, export, out result, out Var.g_all_tran_sum, out Var.all_tran_sum_count, out tmpTotal) == false)
                {
                    return;
                }

                exPortExcel();
            //}
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultTime();
        }
    }
}
