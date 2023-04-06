using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Shell;
using System.Windows;
using SimpleUI.Win32;

namespace SimpleUI.Helper; 

public static class MicaHelper {

    public static void EnableMica(HwndSource source, bool darkThemeEnabled, Window win, bool WinChorme) {
        int trueValue = 0x01;
        int falseValue = 0x00;

        if (darkThemeEnabled)
            Mica.DwmSetWindowAttribute(source.Handle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)));
        else
            Mica.DwmSetWindowAttribute(source.Handle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));

        Mica.DwmSetWindowAttribute(source.Handle, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
        if (WinChorme) SetChrome(win, true);

    }








    public static void SetChrome(Window win, bool mica) {
        if (mica) {
            WindowChrome.SetWindowChrome(win, new WindowChrome() {
                //NonClientFrameEdges = NonClientFrameEdges.Bottom | NonClientFrameEdges.Left | NonClientFrameEdges.Right,
                GlassFrameThickness = new Thickness(-1),
                CaptionHeight = 36,
                ResizeBorderThickness = new Thickness(10),
                CornerRadius = new System.Windows.CornerRadius(0)
            });

        }
        else {
            WindowChrome.SetWindowChrome(win, new WindowChrome() {
                UseAeroCaptionButtons = true,
                GlassFrameThickness = new Thickness(1),
                CaptionHeight = 20,
                NonClientFrameEdges = NonClientFrameEdges.None
            });
        }

    }
}
