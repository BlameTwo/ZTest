using AdbShell.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SimpleUI.Interface.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSA_ADBShell.ViewModels;

public partial class APKManagerViewModel:ObservableObject
{
	public APKManagerViewModel(IAdbManager adbManager,IPackageManager packageManager,IToastLitterMessage toastLitterMessage)
	{
        AdbManager = adbManager;
        PackageManager = packageManager;
        ToastLitterMessage = toastLitterMessage;
    }

    [RelayCommand]
    async void Loaded()
    {
        var result = await  PackageManager.ParsePackage("D:\\jd.apk");
    }


    [RelayCommand]
    async void OpenApkInstall()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "apk file (*.apk)|*.apk",
        };
        if (dialog.ShowDialog() is true)
        {
            var result =  await AdbManager.InstallAsync(dialog.FileName, (s) =>
            {
                //显示安装信息
                ToastLitterMessage.Show(s);
            });
            if (result != null && result.Success)
            {
                //显示安装信息
                ToastLitterMessage.Show("安装完成，请前往开始菜单查看该应用");
            }
        }
    }
    public IAdbManager AdbManager { get; }
    public IPackageManager PackageManager { get; }
    public IToastLitterMessage ToastLitterMessage { get; }
}
