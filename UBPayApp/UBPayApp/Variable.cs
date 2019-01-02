using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;


namespace UBPayApp
{
    class Var
    {
        public static string AppVer = "v1.0.1";

        public static int windowsType = 1;
        public static int sound = 1;
        public static int dispnotes = 1;
        public static int CaptureScreen_Left = 1;
        public static int CaptureScreen_Top = 1;
        public static int CaptureScreen_Width = 1;
        public static int CaptureScreen_Height = 1;
        public static int PayChannel = 1;
        public static int PaySetting = 1; //add 2018.10.6
        public static bool bAlreadyOpen = false;
        public static bool bLogin = false;

        public static string body = "";//add 2018.11.18
        public static string subject = "";//add 2018.11.18
        public static string LoginTime = "";//add 2018.11.18
        public static bool CloseMsgFlag = true;


        public static string gkey = "";//用户key
        public static string channel = "";//微信";
        public static string payment = "barcode";//扫码支付";
        public static string order_id = "";
        public static string refund_order_id = "";
        public static string store_id = "";//Var.g_User_Info.store_id;
        public static string operator_id = "1000000001";// Var.g_User_Info.id;
        public static string device_id = "";
        public static string device_name = "";

        public static string ltoken = null;

        public static string merchant_id = "";
        public struct User_Info
        {
            public string id;
            public string agent_id;
            public string merchant_id;
            public string store_id;
            public string username;
            public string phone;
            public string open_id;
            public string type;
            public string time_create;
            public string time_update;
            public string status;
        }
        public static User_Info g_User_Info = new User_Info();

        public struct agent_Info
        {
            public string id;
            public string pid;
            public string name;
            public string number;
            public string address;
            public string principal_name;
            public string principal_phone;
            public string contact_name;
            public string contact_phone;
            public string contact_email;
            public string time_create;
            public string time_update;
            public string status;
        }
        public static agent_Info g_Agent_Info = new agent_Info();

        public struct merchant_Info
        {
            public string id;
            public string agent_id;
            public string name;
            public string short_name;
            public string number;
            public string principal_name;
            public string principal_phone;
            public string contact_name;
            public string contact_phone;
            public string contact_email;
            public string address;
            public string time_create;
            public string time_update;
            public string status;
        }
        public static merchant_Info g_Merchant_Info = new merchant_Info();

        public struct store_Info
        {
            public string id;
            public string merchant_id;
            public string name;
            public string number;
            public string principal_name;
            public string principal_phone;
            public string address;
            public string time_create;
            public string time_update;
            public string status;
        }
        public static store_Info g_Store_Info = new store_Info();

        public struct StoreList_Info
        {
            public string id;
            public string merchant_id;
            public string name;
            public string number;
            public string principal_name;
            public string principal_phone;
            public string address;
            public string time_create;
            public string time_update;
            public string status;
        }
        public static StoreList_Info[] g_StoreList_Info = new StoreList_Info[128];
        public static int Get_StoreListCount = 0;

        public struct UserList_Info
        {
            public string id;
            public string username;
            public string phone;
        }
        public static UserList_Info[] g_UserList_Info = new UserList_Info[128];
        public static int Get_UserListCount = 0;

        public struct DeviceList_Info
        {
            public string id;
            public string name;
            public string number;
            public string type;
        }
        public static DeviceList_Info[] g_DeviceList_Info = new DeviceList_Info[128];
        public static int Get_DeviceListCount = 0;

        public struct all_payment_list
        {
            //接口返回的字段，都填入到下面
            public string payment_name;
        }
        //public static all_payment_list[] g_all_payment_list = new all_payment_list[128];
        //public static int all_payment_list_count = 0;

        //public struct all_payment_channel
        //{
        //    //接口返回的字段，都填入到下面
        //    public string payment_channel;
        //}
        //public static all_payment_channel[] g_all_payment_channel = new all_payment_channel[128];
        //public static int all_payment_channel_count = 0;

        //支付方式
        public static Dictionary<string, string> g_all_payment_channel = new Dictionary<string, string>();       

        //订单状态
        public static Dictionary<string, string> g_all_payment_Order_Status = new Dictionary<string, string>();

        //交易类型
        public static Dictionary<string, string> g_all_payment_list = new Dictionary<string, string>();

        //退款订单状态
        public static Dictionary<string, string> g_all_payment_refund_status = new Dictionary<string, string>();

        public struct all_tran_list
        {
            //接口返回的字段，都填入到下面
            public string id;
            public string order_id;
            public string out_order_id;
            public string out_username;
            public string out_user_id;
            public string agent_id;
            public string merchant_id;
            public string channel;
            public string payment;
            public string store_id;
            public string store_name;
            public string operator_id;
            public string operator_name;
            public string device_id;
            public string device_name;
            public string amount;
            public string actual_amount;
            public string refund_amount;
            public string body;
            public string subject;
            public string time_create;
            public string time_update;
            public string status;
            public string remark;
            public string _status;
            public string payment_name;
            public string channel_name;

        }
        public static all_tran_list[] g_all_tran_list = new all_tran_list[128];
        public static int all_tran_list_count = 0;

        public struct all_tran_sum
        {
            //接口返回的字段，都填入到下面
            public string id;
            public string agent_id;
            public string merchant_id;
            public string merchant_name;
            public string operator_id;
            public string time_create;
            public string date;
            public string count_weixin;
            public string sum_weixin;
            public string count_alipay;
            public string sum_alipay;
            public string count_unionpay;
            public string sum_unionpay;
            public string count_xt;
            public string sum_xt;
            public string count_xz_weixin;
            public string sum_xz_weixin;
            public string count_xz_alipay;
            public string sum_xz_alipay;
            public string count_sf_weixin;
            public string sum_sf_weixin;
            public string count_sf_alipay;
            public string sum_sf_alipay;
            public string count_all;
            public string sum_all;
        }
        public static all_tran_sum[] g_all_tran_sum = new all_tran_sum[128];
        public static int all_tran_sum_count = 0;

        //退款
        public struct all_tran_refund
        {
            //接口返回的字段，都填入到下面
            public string id;
            public string order_id;
            public string refund_order_id;
            public string refund_amount;            
            public string operator_id;
            public string operator_name;
            public string time_create;
            public string time_update;
            public string status;
            public string reason;
            public string remark;
            public string out_order_id;
            public string out_user_id;
            public string agent_id;
            public string amount;
            public string merchant_id;
            public string channel;
            public string payment;
            public string store_id;
            public string store_name;
            public string device_id;
            public string device_name;
            public string subject;
            public string body;
            public string _status;
            public string payment_name;
            public string channel_name;
        }
        public static all_tran_refund[] g_all_tran_refund = new all_tran_refund[128];
        public static int all_tran_refund_count = 0;

        //操作员统计列表        
        public struct _count_info
        {
            public string count;
            public string count_refund;
            public string sum_amount;
            public string sum_actual_amount;
            public string sum_refund_amount;
        }
        //public struct alipay_count_info
        //{

        //}
        //public struct unionpay_count_info
        //{
        //}
        //public struct xt_count_info
        //{
        //}
        public struct all_Order_Count
        {
            public _count_info weixin;
            public _count_info alipay; 
            public _count_info unionpay;
            public _count_info xt;
            public _count_info xz_weixin;
            public _count_info xz_alipay;
            public _count_info sf_weixin;
            public _count_info sf_alipay;
        }
        public static all_Order_Count g_all_Order_Count = new all_Order_Count();


        //订单详细信息
        public static DataRowView gOrderInfo { get; set; }


        public static ManualResetEvent mLoginCheckEvent = new ManualResetEvent(false);
        public static ManualResetEvent mChengeBarCodeEvent = new ManualResetEvent(false);
        public static int iCountTime = 0;
        public static int RefreshTime = 120; 
    }
}
