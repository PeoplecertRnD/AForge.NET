using System;
using System.Runtime.InteropServices;
namespace CaptureScreen
{
    /// <summary>
    /// This class shall keep the GDI32 APIs being used in our program.
    /// </summary>
    public class PlatformInvokeGDI32
    {

        #region Class Variables
        /// <summary>
        /// 
        /// </summary>
        public const int SRCCOPY = 13369376;
        #endregion

        #region Class Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDc"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDc"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hdcDest"></param>
        /// <param name="xDest"></param>
        /// <param name="yDest"></param>
        /// <param name="wDest"></param>
        /// <param name="hDest"></param>
        /// <param name="hdcSource"></param>
        /// <param name="xSrc"></param>
        /// <param name="ySrc"></param>
        /// <param name="RasterOp"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="bmp"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        #endregion

        #region Public Constructor
        /// <summary>
        /// 
        /// </summary>
        public PlatformInvokeGDI32()
        {
            // 
            // TODO: Add constructor logic here
            //
        }
        #endregion

    }
}
