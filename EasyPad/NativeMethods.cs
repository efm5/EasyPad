using System.Runtime.InteropServices;
using System.Text;

namespace EasyPad {
   static partial class Program {
      internal static class NativeMethods {
#pragma warning disable IDE1006 // Naming Styles
         #region imports
         [DllImport("User32.dll")]
         public static extern IntPtr MonitorFromPoint([In] Point pt, [In] uint dwFlags);

         [DllImport("Shcore.dll")]
         public static extern IntPtr GetDpiForMonitor([In] IntPtr hmonitor, [In] DpiType dpiType,
            [Out] out uint dpiX, [Out] out uint dpiY);

         [DllImport("dwmapi.dll")]
         public static extern int DwmGetWindowAttribute(IntPtr hwnd, int attr, out RECT ptr, int size);

         [DllImport("user32.dll", CharSet = CharSet.Unicode)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

         [DllImport("User32.dll", CharSet = CharSet.Auto)]
         public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

         [DllImport("user32.dll")]
         static public extern IntPtr GetForegroundWindow();

         [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
         public static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

         [DllImport("user32.dll")]
         public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

         [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
         public static extern int GetWindowTextLength(IntPtr hWnd);

         [DllImport("user32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

         [DllImport("user32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool IsIconic(IntPtr hWnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool IsWindow(IntPtr hWnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool IsWindowVisible(IntPtr hWnd);

         [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
         public static extern int MessageBoxTimeout(IntPtr hWnd, String lpText, String lpCaption, uint uType, Int16 wLanguageId, Int32
            dwMilliseconds);

         [DllImport("user32")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

         [DllImport("user32", CharSet = CharSet.Unicode)]
         public static extern int RegisterWindowMessage(string message);

         [DllImport("User32.dll", CharSet = CharSet.Auto)]
         public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr SetActiveWindow(IntPtr hWnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetCursorPos(int X, int Y);

         [DllImport("User32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         static public extern bool SetForegroundWindow(IntPtr hwnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetProcessDPIAware();

         [DllImport("user32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y,
            int cx, int cy, uint uFlags);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);
         #endregion

         #region enumerators
         public enum DpiType {
            Effective = 0,
            Angular = 1,
            Raw = 2,
         }

         public enum DWMWINDOWATTRIBUTE {
            DWMWA_NCRENDERING_ENABLED,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
         }

         public enum ShowWindowEnum {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            //Maximize = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11
         };
         #endregion

         #region structures
         [StructLayout(LayoutKind.Sequential)]
         public struct WINDOWPLACEMENT {
            [Flags]
            public enum Flags : uint {
               WPF_ASYNCWINDOWPLACEMENT = 0x0004,
               WPF_RESTORETOMAXIMIZED = 0x0002,
               WPF_SETMINPOSITION = 0x0001
            }
            public uint length;
            public Flags flags;//uint flags;
            public uint showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public RECT rcNormalPosition;
         }

         [StructLayout(LayoutKind.Sequential)]
         public struct DEVMODE {
            //private const int CCHDEVICENAME = 0x20;
            //private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
         }

         [StructLayout(LayoutKind.Sequential)]
         public struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public readonly int Width() { return (Right - Left); }
            public readonly int Height() { return (Bottom - Top); }
         }
         #endregion

         #region variables
         public static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
         //public const int OPEN_EXISTING = 3;
         //public const int SW_SHOW = 5;
         //public const int FILE_SHARE_READ = 0x1;
         //public const int FILE_SHARE_WRITE = 0x2;
         //public const int FSCTL_LOCK_VOLUME = 0x00090018;
         //public const int FSCTL_DISMOUNT_VOLUME = 0x00090020;
         //public const int IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;
         //public const int IOCTL_STORAGE_MEDIA_REMOVAL = 0x002D4804;
         //public const int WM_USER = 0x400;
         //public const int WM_COPYDATA = 0x4A;
         //public const int WM_LBUTTONDBLCLK = 0x203;
         //public const int WM_PASTE = 0x302;
         public const int WM_SCROLL = 276; // Horizontal scroll
         public const int WM_VSCROLL = 277; // Vertical scroll
         public const int SB_LINEUP = 0; // Scrolls one line up
         public const int SB_LINELEFT = 0;// Scrolls one cell left
         public const int SB_LINEDOWN = 1; // Scrolls one line down
         public const int SB_LINERIGHT = 1;// Scrolls one cell right
         public const int SB_PAGEUP = 2; // Scrolls one page up
         public const int SB_PAGELEFT = 2;// Scrolls one page left
         public const int SB_PAGEDOWN = 3; // Scrolls one page down
         public const int SB_PAGERIGHT = 3; // Scrolls one page right
         public const int SB_PAGETOP = 6; // Scrolls to the upper left
         public const int SB_LEFT = 6; // Scrolls to the left
         public const int SB_PAGEBOTTOM = 7; // Scrolls to the upper right
         public const int SB_RIGHT = 7; // Scrolls to the right
         //public const int SB_ENDSCROLL = 8; // Ends scroll
         //public const int MOUSEEVENTF_LEFTDOWN = 0x02;
         //public const int MOUSEEVENTF_LEFTUP = 0x04;
         //public const uint GENERIC_READ = 0x80000000;
         //public const uint GENERIC_WRITE = 0x40000000;
         //public const uint SEE_MASK_INVOKEIDLIST = 12;
         #endregion
#pragma warning restore IDE1006 // Naming Styles
      }

      //efm5 this application is NOT single-launch – no Mutex
   }
}