using System.Windows;
using System.Windows.Media;

namespace RisCaptureLib
{
    internal static class Config
    {
        public static Brush SelectionBorderBrush = new SolidColorBrush(Color.FromArgb(255, 49, 106, 196));
        public static Thickness SelectionBorderThickness = new Thickness(2.0); 

        public static Brush MaskWindowBackground = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
    }
}
