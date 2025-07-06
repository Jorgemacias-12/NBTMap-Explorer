using NBTMap_Explorer.Helpers;
using NBTMap_Explorer.ViewModels;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace NBTMap_Explorer.Views
{
    public partial class BaseWindow : Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT { public int x; public int y; }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO { public POINT ptReserved; public POINT ptMaxSize; public POINT ptMaxPosition; public POINT ptMinTrackSize; public POINT ptMaxTrackSize; }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MONITORINFO { public int cbSize; public RECT rcMonitor; public RECT rcWork; public uint dwFlags; }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int left; public int top; public int right; public int bottom; }

        private const int WM_SYSCOMMAND = 0x0112;
        private const int WM_GETMINMAXINFO = 0x0024;

        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_RESTORE = 0xF120;

        private const int MONITOR_DEFAULTTONEAREST = 2;

        public BaseWindow()
        {
            InitializeComponent();

            ConfigWindow();
        }

        private void ConfigWindow()
        {
            StateChanged += (s, e) =>
            {
                if (DataContext is not null &&
                    DataContext is BaseWindowViewModel viewModel)
                {
                    viewModel.WindowState = WindowState;
                }
            };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var helper = new WindowInteropHelper(this);
            var source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_GETMINMAXINFO)
            {
                WmGetMinMaxInfo(hwnd, lParam);
                handled = true;
                return IntPtr.Zero;
            }

            handled = false;
            return IntPtr.Zero;
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO { cbSize = Marshal.SizeOf(typeof(MONITORINFO)) };
                GetMonitorInfo(monitor, ref monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        private void SendWindowCommand(int command)
        {
            var hwnd = new WindowInteropHelper(this).Handle;

            SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)command, IntPtr.Zero);
        }

        private void Min_Click(object sender, RoutedEventArgs e)
        {
            SendWindowCommand(SC_MINIMIZE);
        }

        private void Max_Click(object sender, RoutedEventArgs e)
        {
            var command = WindowState == WindowState.Maximized ? SC_RESTORE : SC_MAXIMIZE;
            SendWindowCommand(command);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SendWindowCommand(SC_CLOSE);
        }

        bool isPreseed = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isPreseed = !isPreseed;

            string themeToApply = isPreseed ? "Dark" : "Light";

            SystemTheme.ApplyTheme(themeToApply);
        }
    }
}