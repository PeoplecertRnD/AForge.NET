using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace CaptureScreen
{
    /// <summary>
    /// This class shall keep all the functionality for capturing 
    /// the desktop.
    /// </summary>
    public class CaptureScreen
    {
        /// <summary>
        /// 
        /// </summary>
        public enum CaptureRectangle
        {
            /// <summary></summary>
            ENTIRE_SCREEN,
            /// <summary></summary>
            PRIMARY_SCREEN
        }
        /// <summary></summary>
        public static Rectangle GetEntireDesktop()
        {
            Rectangle rec = Screen.PrimaryScreen.Bounds;
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                if (Screen.AllScreens[i].Bounds.X > rec.X && Screen.AllScreens[i].Bounds.Y == rec.Y)
                {
                    rec.Width += Screen.AllScreens[i].Bounds.Width;
                }
                else if (Screen.AllScreens[i].Bounds.X == rec.X && Screen.AllScreens[i].Bounds.Y > rec.Y)
                {
                    rec.Height += Screen.AllScreens[i].Bounds.Height;
                }
                else if (Screen.AllScreens[i].Bounds.X > rec.X && Screen.AllScreens[i].Bounds.Y > rec.Y)
                {
                    rec.Width += Screen.AllScreens[i].Bounds.Width;
                    rec.Height += Screen.AllScreens[i].Bounds.Height;
                }
                else if (Screen.AllScreens[i].Bounds.X < rec.X && Screen.AllScreens[i].Bounds.Y == rec.Y)
                {
                    rec.X = Screen.AllScreens[i].Bounds.X;
                    rec.Width += Screen.AllScreens[i].Bounds.Width;
                }
                else if (Screen.AllScreens[i].Bounds.X == rec.X && Screen.AllScreens[i].Bounds.Y < rec.Y)
                {
                    rec.Y = Screen.AllScreens[i].Bounds.Y;
                    rec.Height += Screen.AllScreens[i].Bounds.Height;
                }
                else if (Screen.AllScreens[i].Bounds.X < rec.X && Screen.AllScreens[i].Bounds.Y < rec.Y)
                {
                    rec.X = Screen.AllScreens[i].Bounds.X;
                    rec.Y = Screen.AllScreens[i].Bounds.Y;
                    rec.Width += Screen.AllScreens[i].Bounds.Width;
                    rec.Height += Screen.AllScreens[i].Bounds.Height;
                }
            }

            return rec;
        }

        #region Public Class Functions
        /// <summary></summary>
        public static Bitmap GetDesktopImage(CaptureRectangle captureRectangle)
        {
            float ScalingFactor = getScalingFactor();
            float scale = (ScalingFactor - 1) * 100;
            Rectangle rec = Screen.PrimaryScreen.Bounds;
            if (captureRectangle == CaptureRectangle.ENTIRE_SCREEN)
            {
                rec = GetEntireDesktop();
            }
            else rec = Screen.PrimaryScreen.Bounds;

            //In size variable we shall keep the size of the screen.
            SIZE size;

            //Variable to keep the handle to bitmap.
            IntPtr hBitmap;
            //Here we get the handle to the desktop device context.
            IntPtr hDC = PlatformInvokeUSER32.GetDC(PlatformInvokeUSER32.GetDesktopWindow());

            //Here we make a compatible device context in memory for screen device context.
            IntPtr hMemDC = PlatformInvokeGDI32.CreateCompatibleDC(hDC);

            //We pass SM_CXSCREEN constant to GetSystemMetrics to get the X coordinates of screen.
            //size.cx = PlatformInvokeUSER32.GetSystemMetrics(PlatformInvokeUSER32.SM_CXSCREEN);
          
            int xWidth = rec.Width + ((rec.Width * Convert.ToInt32(scale)) / 100);
            size.cx = xWidth;// rec.Width;

            //We pass SM_CYSCREEN constant to GetSystemMetrics to get the Y coordinates of screen.
            //size.cy = PlatformInvokeUSER32.GetSystemMetrics(PlatformInvokeUSER32.SM_CYSCREEN);
            int yHeight = rec.Height + ((rec.Height * Convert.ToInt32(scale)) / 100);
            size.cy = yHeight;// rec.Height;

            //We create a compatible bitmap of screen size using screen device context.
            hBitmap = PlatformInvokeGDI32.CreateCompatibleBitmap(hDC, size.cx, size.cy);

            //As hBitmap is IntPtr we can not check it against null. For this purspose IntPtr.Zero is used.
            if (hBitmap != IntPtr.Zero)
            {
                //Here we select the compatible bitmap in memeory device context and keeps the refrence to Old bitmap.
                IntPtr hOld = (IntPtr)PlatformInvokeGDI32.SelectObject(hMemDC, hBitmap);
                //We copy the Bitmap to the memory device context.
                PlatformInvokeGDI32.BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC, rec.X, rec.Y, PlatformInvokeGDI32.SRCCOPY);
                //We select the old bitmap back to the memory device context.
                PlatformInvokeGDI32.SelectObject(hMemDC, hOld);
                //We delete the memory device context.
                PlatformInvokeGDI32.DeleteDC(hMemDC);
                //We release the screen device context.
                PlatformInvokeUSER32.ReleaseDC(PlatformInvokeUSER32.GetDesktopWindow(), hDC);
                //Image is created by Image bitmap handle and stored in local variable.
                Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                //Release the memory to avoid memory leaks.
                PlatformInvokeGDI32.DeleteObject(hBitmap);
                //This statement runs the garbage collector manually.
                GC.Collect();
                //Return the bitmap 
                return bmp;
            }

            //If hBitmap is null retunrn null.
            return null;
        }


        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);



        private static float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, 10);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, 117);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }

        #endregion
    }
}
