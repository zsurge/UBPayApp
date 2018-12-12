using System;
using System.Windows.Media.Imaging;

namespace RisCaptureLib
{
    public class ScreenCaputredBitmapEventArgs : EventArgs
    {

        public System.Drawing.Bitmap bBitMap
        {
            get;
            private set;
        }
        
        public ScreenCaputredBitmapEventArgs(System.Drawing.Bitmap bmp)
        {
            bBitMap = bmp;
        }
    }
}
