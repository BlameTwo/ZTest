using System.IO;
using System.Windows.Media.Imaging;

namespace WSA_ADBShell;

public static class StreamTools
{
    public static BitmapImage BitmapToBitmapImage(this System.Drawing.Bitmap bitmap)
    {
        MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        BitmapImage bit3 = new BitmapImage();
        bit3.BeginInit();
        bit3.StreamSource = ms;
        bit3.EndInit();
        return bit3;
    }
}
