using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using static SimpleUI.Utils.WindowBackdropBase.BackgroundEnum;

namespace SimpleUI.Utils; 
public class WindowBackdropBase 
{
    public enum Type {
        Acrylic = 3,
        Mica = 2,
        Tabbed = 4
    }

    public Type BackType { get; set;     }
    

    public class BackgroundEnum {
        [Flags]
        public enum DWMWINDOWATTRIBUTE {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_SYSTEMBACKDROP_TYPE = 38
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS {
            public int cxLeftWidth;      // width of left border that retains its size
            public int cxRightWidth;     // width of right border that retains its size
            public int cyTopHeight;      // height of top border that retains its size
            public int cyBottomHeight;   // height of bottom border that retains its size
        };
    }

    public static void RefreshFrame(IntPtr ptr) {
        HwndSource mainWindowSrc = HwndSource.FromHwnd(ptr);
        mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);
        MARGINS margins = new MARGINS();
        margins.cxLeftWidth = -1;
        margins.cxRightWidth = -1;
        margins.cyTopHeight = -1;
        margins.cyBottomHeight = -1;
        WindowBackdropBase.BackgroundApply.ExtendFrame(mainWindowSrc.Handle, margins);
    }

    public static void RefreshDarkMode(IntPtr ptr, int par) {
        WindowBackdropBase.BackgroundApply.SetWindowAttribute(
            ptr,
            DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE,
            par);
    }

    public static class BackgroundApply {
        [DllImport("DwmApi.dll")]
        static extern int DwmExtendFrameIntoClientArea(
            IntPtr hwnd,
            ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

        public static int ExtendFrame(IntPtr hwnd, MARGINS margins) {
            return DwmExtendFrameIntoClientArea(hwnd, ref margins);
        }

        public static int SetWindowAttribute(IntPtr ptr, DWMWINDOWATTRIBUTE attribute, Type parameter) {
            var value = (int)parameter;
            var result = DwmSetWindowAttribute(ptr, attribute, ref value, Marshal.SizeOf<int>());
            return result;
        }

        public static int SetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, int parameter) {
            var result = DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
            return result;
        }
    }
}
