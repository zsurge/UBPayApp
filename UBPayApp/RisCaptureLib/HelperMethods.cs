using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PixelFormat=System.Drawing.Imaging.PixelFormat;

namespace RisCaptureLib
{
    internal static class HelperMethods
    {
        public static Rect ToRect(this Rectangle rectangle)
        {
            return new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static Rectangle ToRectangle(this Rect rect)
        {
            return new Rectangle((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height);
        }

        public static Rect GetRectContainsAllScreens()
        {
            var rect = Rect.Empty;
            foreach (Screen screen in Screen.AllScreens)
            {
                rect.Union(screen.Bounds.ToRect());
            }

            return rect;
        }

        public static Bitmap GetScreenSnapshot()
        {
            try
            {
                Rectangle rc = SystemInformation.VirtualScreen;
                var bitmap = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);

                using (Graphics memoryGrahics = Graphics.FromImage(bitmap))
                {
                    memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                }

                return bitmap;
            }
// ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
// ReSharper restore EmptyGeneralCatchClause
            {
                
            }

            return null;
            

        }

        public static BitmapSource ToBitmapSource(this Bitmap bmp)
        {
            BitmapSource returnSource;

            try
            {
                returnSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(),IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                returnSource = null;
            }

            return returnSource;

        }

        public static T GetAncestor<T>(this DependencyObject element)
        {
            while (!(element == null || element is T))
            {
                element = VisualTreeHelper.GetParent(element);
            }


            if ((element != null) && (element is T))
            {
                return (T)(object)element;
            }

            return default(T);
        }


        public static T GetRenderTransform<T>(this UIElement element) where T : Transform
        {
            if (element.RenderTransform.Value.IsIdentity)
            {
                element.RenderTransform = CreateSimpleTransformGroup();
            }

            if (element.RenderTransform is T)
            {
                return (T)element.RenderTransform;
            }

            if (element.RenderTransform is TransformGroup)
            {
                var group = (TransformGroup)element.RenderTransform;

                foreach (var t in group.Children)
                {
                    if (t is T)
                    {
                        return (T)t;
                    }
                }
            }

            throw new NotSupportedException("Can not get instance of " + typeof(T).Name + " from " + element + "'s RenderTransform : " + element.RenderTransform);
        }

        public static TransformGroup CreateSimpleTransformGroup()
        {
            var group = new TransformGroup();

            //notes that : the RotateTransform must must be the first one in this group
            group.Children.Add(new RotateTransform());
            group.Children.Add(new TranslateTransform());
            group.Children.Add(new ScaleTransform());
            group.Children.Add(new SkewTransform());

            return group;
        }

        public static bool IsNormalNumber(this Double d)
        {
            return !Double.IsInfinity(d) &&
                   !Double.IsNaN(d) &&
                   !Double.IsNegativeInfinity(d) &&
                   !Double.IsPositiveInfinity(d);
        }

    }
}
