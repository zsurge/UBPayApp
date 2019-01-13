using System;
using System.Collections.Generic;
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
using System.Threading;
using System.Windows.Threading;

namespace UBPayApp
{
    /// <summary>
    /// Refund.xaml 的交互逻辑
    /// </summary>
    public partial class Refund : Window
    {
        public string getOrderId { get; set; }
        public Refund()
        {
            InitializeComponent();
        }

        Thread QueryRefundStatusThreadProcess;

        private static bool execute_Flag = false;
        private static bool close_Flag = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lab_order_id.Content = getOrderId;

            //获取焦点，为支付成功关窗口做准备
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(tx_refund_amount); }));
        }

        private void btn_refund_Click(object sender, RoutedEventArgs e)
        {
            if (close_Flag)
            {             
                return;
            }
            close_Flag = true;

            if (execute_Flag)
            {
                execute_Flag = false;
                this.Close();
                return;  //有可能关不到，这里退出
            }            

            PayInterface payInterface = new PayInterface();
            if (tx_refund_amount.Text.Length == 0 && lab_order_id.Content != null)
            {
                
                return;
            }

            Dictionary<string, string> result = new Dictionary<string, string>();

            bool bRet = payInterface.ApiAddRefund(lab_order_id.Content.ToString(), tx_refund_amount.Text, Var.operator_id, Var.merchant_id, out result);

            //状态查询
            if (bRet)
            {           
                string str = string.Empty;
                result.TryGetValue("refund_order_id", out str);
                lab_refund_order_id.Content = str;

                QueryRefundStatusThreadProcess = new Thread(new ParameterizedThreadStart(GetRefundStatus));
                QueryRefundStatusThreadProcess.Start(str);                
            
            }
            else
            {
                
                MessageBox.Show("退款失败", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void GetRefundStatus(object obj)
        {
            string refund_order_id = obj as string;
            MediaPlayer media = new MediaPlayer();

            string msg = string.Empty;
            string tmp = string.Empty;
            string status = string.Empty;

            //int i = 3;

            Dictionary<string, string> result = new Dictionary<string, string>();


            PayInterface payInterface = new PayInterface();

            while (close_Flag)
            {
                Thread.Sleep(5000);

                if (payInterface.ApiRefundStatusQuery(refund_order_id, Var.merchant_id, out result) == false)
                {
                    if (Var.sound == 1)
                    {
                        media.Open(new Uri("./prompt/查询订单状态失败，请手工查询.mp3", UriKind.Relative));
                        media.Play();
                    }

                    execute_Flag = true;
                    MessageBox.Show("查询退款状态失败，请手工查询", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                result.TryGetValue("status", out tmp);
                result.TryGetValue("msg", out msg);

                Var.g_all_payment_refund_status.TryGetValue(tmp, out status);

                Dispatcher.Invoke(new Action(() =>
                {
                    lab_Refund_Status.Content = status;
                }));


                if (status.Contains("退款成功") || status.Contains("退款失败"))
                {
                    execute_Flag = true;
                    if (Var.sound == 1)
                    {
                        if (status.Contains("退款成功"))
                        {
                            media.Open(new Uri("./prompt/退款成功.mp3", UriKind.Relative));
                        }
                        else
                        {
                            media.Open(new Uri("./prompt/退款失败.mp3", UriKind.Relative));
                        }

                        media.Play();
                    }
                    break;
                }
            }

            execute_Flag = true;
            close_Flag = false;

            Dispatcher.Invoke(new Action(() =>
            {
                lab_Refund_Status.Content = status;
            }));

            
            //获取焦点，为支付成功关窗口做准备
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(btn_refund); }));

        }

        private void tx_refund_amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //下一个控制获取焦点
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(tx_refund_reason); }));
            }
        }

        private void tx_refund_reason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //下一个控制获取焦点
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(btn_refund); }));
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            close_Flag = false;
        }
    }//end class
}
