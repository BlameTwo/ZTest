using System.Runtime.InteropServices;
using System;
using System.Security;

namespace SimpleUI.Base.Interop;

public static class User
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetDC(IntPtr ptr);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);


    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);


    [DllImport("user32.dll", SetLastError = true)]
    public static extern int ReleaseDC(IntPtr window, IntPtr dc);

}
