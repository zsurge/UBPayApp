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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace UBPayApp
{
    /// <summary>
    /// SetAmountPosition.xaml 的交互逻辑
    /// </summary>
    public partial class SetAmountPosition : Window
    {
        private readonly RisCaptureLib.ScreenCaputre screenCaputre = new RisCaptureLib.ScreenCaputre();
        private Size? lastSize;

        public SetAmountPosition()
        {
            InitializeComponent();
            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
        }

        IniFile ParmIni = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"/Parm.ini");


        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            //Show();
           // Focus();
        }

        private void OnScreenCaputred(object sender, RisCaptureLib.ScreenCaputredEventArgs e)
        {
            //set last size
            lastSize = new Size(e.Bmp.Width, e.Bmp.Height);

            //Show();
            //test
            var bmp = e.Bmp;
            Clipboard.SetImage(e.Bmp);
            lb_left.Content = screenCaputre.left;
            lb_top.Content = screenCaputre.top;
            lb_height.Content = screenCaputre.h;
            lb_width.Content = screenCaputre.w;

            ParmIni.IniWriteValue("CaptureScreen", "Left", screenCaputre.left.ToString());
            ParmIni.IniWriteValue("CaptureScreen", "Top", screenCaputre.top.ToString());
            ParmIni.IniWriteValue("CaptureScreen", "Width", screenCaputre.w.ToString());
            ParmIni.IniWriteValue("CaptureScreen", "Height", screenCaputre.h.ToString());

            Var.CaptureScreen_Left = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Left"));
            Var.CaptureScreen_Top = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Top"));
            Var.CaptureScreen_Width = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Width"));
            Var.CaptureScreen_Height = Convert.ToInt16(ParmIni.IniReadValue("CaptureScreen", "Height"));            
        }

        private void btn_SettingAmountPosition_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Thread.Sleep(300);
            screenCaputre.StartCaputre(30, lastSize);


        }

        private void SetAmountPage_Loaded(object sender, RoutedEventArgs e)
        {
            lb_left.Content = ParmIni.IniReadValue("CaptureScreen", "Left");
            lb_top.Content = ParmIni.IniReadValue("CaptureScreen", "Top");
            lb_height.Content = ParmIni.IniReadValue("CaptureScreen", "Width");
            lb_width.Content = ParmIni.IniReadValue("CaptureScreen", "Height");
        }
    }//end class


}
