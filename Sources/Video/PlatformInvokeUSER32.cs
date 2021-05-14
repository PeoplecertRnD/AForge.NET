using System;
using System.Runtime.InteropServices;
namespace CaptureScreen
{
    /// <summary>
    /// This class shall keep the User32 APIs being used in 
    /// our program.
    /// </summary>
    public class PlatformInvokeUSER32
    {

        #region Class Variables
        /// <summary></summary>
        public const int SM_CXSCREEN = 0;
        /// <summary></summary>
        public const int SM_CYSCREEN = 1;
        #endregion

        #region Class Functions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="abc"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hDc"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        #endregion

        #region Public Constructor
        /// <summary></summary>
        public PlatformInvokeUSER32()
        {
            // 
            // TODO: Add constructor logic here
            //
        }
        #endregion
    }

    /// <summary>
    /// This structure shall be used to keep the size of the screen.
    /// </summary>
    public struct SIZE
    {
        /// <summary></summary>
        public int cx;
        /// <summary></summary>
        public int cy;
    }
}
