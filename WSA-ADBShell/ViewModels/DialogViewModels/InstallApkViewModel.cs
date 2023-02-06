using AAPTForNet.Models;
using AdbShell.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WSA_ADBShell.ViewModels.DialogViewModels;

public partial class InstallApkViewModel:ObservableObject
{
    public InstallApkViewModel(IAdbManager adbManager,IPackageManager packageManager)
    {
        AdbManager = adbManager;
        PackageManager = packageManager;
        HotDeviceName = AdbManager.HotDevice.DeviceName;
    }

    [ObservableProperty]
    ApkInfo _Info;


    partial void OnInfoChanged(ApkInfo value)
    {
        if(value.Icon != null)
        {
            FileStream stream = new FileStream(Info.FullPath, FileMode.Open, FileAccess.Read);
            ZipArchive zipfile = new ZipArchive(stream);
            foreach (var item in zipfile.Entries)
            {
                if(value.Icon.IconName == item.FullName)
                {
                    Stream iconstream = item.Open();
                    Bitmap bit = new Bitmap(iconstream);
                    this.Icon = bit.BitmapToBitmapImage();
                    break;
                }
            }
        }
    }

    

    [ObservableProperty]
    string _HotDeviceName;

    [ObservableProperty]
    ImageSource _Icon;

    public IAdbManager AdbManager { get; }
    public IPackageManager PackageManager { get; }
}
