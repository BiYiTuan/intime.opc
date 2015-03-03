using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    public class WindowsTaskBarInfo
    {
        private static int dpi = 0;

        public static WindowsTaskBarInfo GetWindowsTaskBarInfo()
        {
            return new WindowsTaskBarInfo();
        }

        #region Properties

        /// <summary>
        /// Work area of the screen on which the taskbar is displayed taking autohide into acount
        /// This is the screen area excluding the taskbar and docked toolbars.
        /// </summary>
        public Rect WorkArea { get { return workArea; } }
        private readonly Rect workArea;

        public Rect ScreenArea { get { return screenArea; } }
        private readonly Rect screenArea;

        public Win32.TaskBarEdge Edge { get { return edge; } }
        private readonly Win32.TaskBarEdge edge;

        #endregion Properties

        #region constructor

        // protected constructor
        protected WindowsTaskBarInfo()
        {
            // if we can't get anything else
            screenArea = workArea = SystemParameters.WorkArea;
            edge = Win32.TaskBarEdge.Bottom;

            //Fixed 10536 because Win32.SHAppBarMessage works incorrectly when running IDVault as admin while login as limit user(user or guest group)
            Screen screen = GetScreenWithTaskBar();
            if (screen != null)
            {
                screenArea = new Rect(
                    ConvertPixelsToDIPixels(screen.Bounds.X),
                    ConvertPixelsToDIPixels(screen.Bounds.Y),
                    ConvertPixelsToDIPixels(screen.Bounds.Width),
                    ConvertPixelsToDIPixels(screen.Bounds.Height)
                    );

                workArea = new Rect(
                    ConvertPixelsToDIPixels(screen.WorkingArea.X),
                    ConvertPixelsToDIPixels(screen.WorkingArea.Y),
                    ConvertPixelsToDIPixels(screen.WorkingArea.Width),
                    ConvertPixelsToDIPixels(screen.WorkingArea.Height)
                    );
                edge = TaskBarLocation(screen);
            }

        }

        #endregion constructor

        /// <summary>
        /// Determine the screen the taskbar is located at (not always the primary screen for multiple monitors)
        /// </summary>
        /// <returns></returns>
        private Screen GetScreenWithTaskBar()
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen singleScreen in screens)
            {
                if (singleScreen != null)
                {
                    if (!ScreenAreaEqualsWorkingArea(singleScreen))
                    {
                        return singleScreen;
                    }
                }
            }
            return null;

        }

        /// <summary>
        /// Check if screen area equals working area.
        /// </summary>
        /// <param name="screen">screen</param>
        /// <returns>true means equals, otherwise not.</returns>
        private bool ScreenAreaEqualsWorkingArea(Screen screen)
        {
            bool equals = false;
            System.Drawing.Rectangle singleScreenArea = screen.Bounds;
            System.Drawing.Rectangle singleWorkingArea = screen.WorkingArea;
            if (singleScreenArea.Equals(singleWorkingArea))
            {
                equals = true;
            }
            return equals;
        }

        /// <summary>
        /// Get taskbar location for setting edge.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        private Win32.TaskBarEdge TaskBarLocation(Screen screen)
        {
            Win32.TaskBarEdge taskbarLoc = Win32.TaskBarEdge.Bottom;
            System.Drawing.Rectangle singleScreenArea = screen.Bounds;
            System.Drawing.Rectangle singleWorkingArea = screen.WorkingArea;
            bool leftAligned = (singleScreenArea.Left.Equals(singleWorkingArea.Left)) ? true : false;
            bool rightAligned = (singleScreenArea.Right.Equals(singleWorkingArea.Right)) ? true : false;
            bool topAligned = (singleScreenArea.Top.Equals(singleWorkingArea.Top)) ? true : false;
            bool bottomAligned = (singleScreenArea.Bottom.Equals(singleWorkingArea.Bottom)) ? true : false;

            if (!leftAligned)
            {
                taskbarLoc = Win32.TaskBarEdge.Left;
            }
            else if (!rightAligned)
            {
                taskbarLoc = Win32.TaskBarEdge.Right;
            }
            else if (!topAligned)
            {
                taskbarLoc = Win32.TaskBarEdge.Top;
            }
            else if (!bottomAligned)
            {
                taskbarLoc = Win32.TaskBarEdge.Bottom;
            }

            return taskbarLoc;
        }

        /// <summary>
        /// Convert the specified pixel value to a device-independent pixel value.
        /// </summary>
        /// <param name="pixels">number of device-dependent pixels</param>
        /// <returns>number of device-independent pixels for the current DPI</returns>
        public static double ConvertPixelsToDIPixels(double pixels)
        {
            return pixels * 96.0 / DPI;
        }

        /// <summary>
        /// Retrieve the current screen DPI (Dots-Per-Inch).
        /// </summary>
        private static int DPI
        {
            get
            {
                if (dpi == 0)
                {
                    HandleRef desktopHwnd = new HandleRef(null, IntPtr.Zero);
                    HandleRef desktopDC = new HandleRef(null, Win32.GetDC(desktopHwnd));

                    dpi = Win32.GetDeviceCaps(desktopDC, (int)Win32.DeviceCaps.LOGPIXELSX);

                    Win32.ReleaseDC(desktopHwnd, desktopDC);
                }
                return dpi;
            }
        }
    }
}
