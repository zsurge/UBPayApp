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
using System.Windows.Shapes;

using System.Drawing;

using System.Runtime.InteropServices;

using System.Threading;
using System.Windows.Interop;

using System.Windows.Threading;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tesseract;

namespace UBPayApp
{
    /// <summary>
    /// PayWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PayWindow : Window
    {
        bool bRet = false;
        private static bool execute_Flag = false;

        private readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "UBPay.bmp";

        Thread QueryThreadProcess;

        public PayWindow()
        {
            InitializeComponent();
            baiduOcr = new BaiduOcr();

            bRet = GetNum(ref Money);
            if (bRet)
            {
                textBox1.Text = Money.ToString();
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(tBoxBarCode); }));
            }
            else
                textBox1.Text = "0.00";

            if (Var.PayChannel == 1)
            {
                this.Title = "微信/支付宝/银联支付";
                Var.channel = "";
                //btPay.Content = "微信/支付宝/银联支付";
            }
            else
            {
                this.Title = "新天支付";
                Var.channel = "xt";
                //btPay.Content = "新天支付";
            }
                        
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Var.bAlreadyOpen = false;
                //QueryThreadProcess.Abort();//关闭时结束线程
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //string OldMd5str = "";
        BaiduOcr baiduOcr;
        double Money = 0;

        private void SaveBmp()
        {

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            try
            {
                Bitmap bitmap00 = CapScreen.GetDesktopImage();
                Bitmap newImg = new Bitmap(Var.CaptureScreen_Width, Var.CaptureScreen_Height);
                Graphics g = Graphics.FromImage(newImg);
                g.DrawImage(bitmap00, 0, 0, new System.Drawing.Rectangle(Var.CaptureScreen_Left, Var.CaptureScreen_Top, newImg.Width, newImg.Height), GraphicsUnit.Pixel);

                newImg.Save(filePath);
                image1.Source = BitmapToBitmapImage(newImg);
            }
            catch (Exception ex)
            {
                LogManager.WriteLogTran(LogType.Message, "Exception =", ex.Message);
                LogManager.WriteLogTran(LogType.Message, "Exception =", ex.Source);
                LogManager.WriteLogTran(LogType.Message, "Exception =", ex.StackTrace);
            }

            //-------------------
            //Bitmap bitmap00 = CapScreen.GetDesktopImage();
            //Bitmap B = new Bitmap(Var.CaptureScreen_Width, Var.CaptureScreen_Height); //新建一个理想大小的图像文件
            //Graphics g = Graphics.FromImage(B);//实例一个画板的对象,就用上面的图像的画板
            //g.DrawImage(bitmap00, 0, 0);//把目标图像画在这个图像文件的画板上

            //B.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);//通过这个图像来保存

            //image1.Source = BitmapToBitmapImage(B);


            ////---------------------
            //Thread.Sleep(1000);
            //System.Drawing.Bitmap bitmap = new Bitmap(Var.CaptureScreen_Width, Var.CaptureScreen_Height);
            //using (System.Drawing.Graphics graphics = Graphics.FromImage(bitmap))
            //{
            //    graphics.CopyFromScreen(Var.CaptureScreen_Left, Var.CaptureScreen_Top, 0, 0, new System.Drawing.Size(Var.CaptureScreen_Width, Var.CaptureScreen_Height));
            //    bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
            //    image1.Source = BitmapToBitmapImage(bitmap);
            //}

        }

        private bool GetNum(ref double Money)
        {
            bool bRet = false;
            const string language = "amt";
            try
            {
                //Bitmap bitmap00 = CapScreen.GetDesktopImage();
                //Bitmap newImg = new Bitmap(Var.CaptureScreen_Width, Var.CaptureScreen_Height);
                //Graphics g = Graphics.FromImage(newImg);
                //g.DrawImage(bitmap00, 0, 0, new System.Drawing.Rectangle(Var.CaptureScreen_Left, Var.CaptureScreen_Top, newImg.Width, newImg.Height), GraphicsUnit.Pixel);

                //newImg.Save(filePath);
                //image1.Source = BitmapToBitmapImage(newImg);

                SaveBmp();


                //使用tesseract算法 2018.11.29
                if (File.Exists(filePath))
                {
                    var image = new Bitmap(filePath);
                    TesseractEngine ocr = new TesseractEngine("./tessdata", language);
                    ocr.SetVariable("tessedit_char_whitelist", "0123456789."); // If digit only  
                    var result = ocr.Process(image);
                    Money = Convert.ToDouble(result.GetText());
                    bRet = true;
                }
                else
                {
                    MessageBox.Show("未找到截图");
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLogTran(LogType.Message, "Exception =", ex.Message);
                LogManager.WriteLogTran(LogType.Message, "Exception =", ex.Source);
                LogManager.WriteLogTran(LogType.Message, "Exception =", ex.StackTrace);
            }
            return bRet;
        }

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

        private void btPay_Click(object sender, RoutedEventArgs e)
        {
            payment();
        }

        private void payment()
        {
            if (execute_Flag)
            {
                execute_Flag = false;
                this.Close();
                return;    //有可能关不到，这里退出
            }

            if (textBox1.Text.ToString() == "0.00")
                return;

            if (tBoxBarCode.Text.Trim().Replace(" ", "").Length < 18)
                return;

            double dMoney = 0;
            try
            {
                dMoney = Convert.ToDouble(textBox1.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("支付金额有错");
                return;
            }

            Dictionary<string, string> result = new Dictionary<string, string>();

            string order_id = string.Empty;
            string Barcode = tBoxBarCode.Text.Trim().Replace(" ", "");
            string msg = string.Empty;
            string tmp = string.Empty;

            PayInterface payInterface = new PayInterface();
            
            //Var.channel,需要在支付的时候赋值 modfiy 20181201
            bool bRet = payInterface.InterfaceAddOrder(Var.channel, Var.payment, dMoney.ToString(), Barcode, Var.subject, Var.body,Var.merchant_id, Var.store_id, Var.operator_id, Var.device_id, Var.gkey, out order_id,out result);

            //add 状态查询
            if (bRet)
            {
                //更新订单号
                Pay_Order.Content = order_id;

                //判定是不是支付失败 下单返回status=4的时候是下单失败了，要把失败的信息显示出来
                result.TryGetValue("status", out tmp);
                if (tmp.Contains("4"))
                {
                    result.TryGetValue("msg", out msg);
                    Pay_Status.Content = "status:" + tmp + msg;
                    return;
                }


                QueryThreadProcess = new Thread(new ParameterizedThreadStart(GetPayMentStatus));
                QueryThreadProcess.Start(order_id);
            }
            else
            {
                Pay_Status.Content = "交易失败，请重试";                
            }
        }

        private void GetPayMentStatus(object obj)
        {
            string order_id = obj as string;
            string msg = string.Empty;
            string tmp = string.Empty;
            string status = string.Empty;
            int i = 3;

            //Dictionary<string, string> describe = new Dictionary<string, string>();

            Dictionary<string, string> result = new Dictionary<string, string>();

            PayInterface payInterface = new PayInterface();

            while (true)
            {
                if (status.Contains("支付成功") || status.Contains("支付失败"))
                {
                    execute_Flag = true;
                    break;
                }

                if (payInterface.ApiOrderStatusQuery(order_id, Var.merchant_id, out result) == false)
                {
                    MessageBox.Show("查询订单状态失败，请手工查询");
                    return;
                }

                result.TryGetValue("status", out tmp);
                result.TryGetValue("msg", out msg);

                Var.g_all_payment_Order_Status.TryGetValue(tmp, out status);

                if (i-- == 0)
                {
                    break;
                }

                Thread.Sleep(2000);
            }

            execute_Flag = true;
            Dispatcher.Invoke(new Action(() =>
            {
                Pay_Status.Content = status + msg;
            }));

            //获取焦点，为支付成功关窗口做准备
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(tBoxBarCode); }));

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //tBoxBarCode 获取焦点
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,(Action)(() => { Keyboard.Focus(tBoxBarCode); }));            
        }

        private void tBoxBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            //按下回车键自动执行支付
            if (e.Key == Key.Enter)
            {
               payment();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //按下回车键自动执行支付
            if (e.Key == Key.Enter)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(tBoxBarCode); }));
            }
        }
    }
}
