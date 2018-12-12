using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UBPayApp
{
    /// <summary>
    /// OrderDetails.xaml 的交互逻辑
    /// </summary>
    public partial class OrderDetails : Window
    {
        public DataRowView dr;
        public OrderDetails()
        {
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dr = Var.gOrderInfo;
            string str = dr["status"].ToString();           

            lable1.Content = dr["order_id"].ToString();
            lable2.Content = dr["out_order_id"].ToString();
            lable3.Content = dr["out_user_id"].ToString();
            lable4.Content = dr["out_username"].ToString();
            lable5.Content = dr["agent_id"].ToString();
            lable6.Content = dr["merchant_id"].ToString();
            lable7.Content = dr["channel_name"].ToString();
            lable8.Content = dr["payment_name"].ToString();
            lable9.Content = dr["store_id"].ToString();
            lable10.Content = dr["store_name"].ToString();
            lable11.Content = dr["operator_id"].ToString();
            lable12.Content = dr["operator_name"].ToString();
            lable13.Content = dr["device_id"].ToString();
            lable14.Content = dr["device_name"].ToString();
            lable15.Content = dr["amount"].ToString();
            lable16.Content = dr["actual_amount"].ToString();
            lable17.Content = dr["refund_amount"].ToString();
            lable18.Content = dr["body"].ToString();
            lable19.Content = dr["subject"].ToString();
            lable20.Content = dr["time_create"].ToString();
            lable21.Content = dr["time_update"].ToString();
            lable22.Content = Var.g_all_payment_Order_Status.FirstOrDefault(q => q.Value == str).Key;
            lable23.Content = dr["remark"].ToString();
        }
    }
}
