using AdbShell.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSA_ADBShell.Views.Dialogs;

namespace WSA_ADBShell.ViewModels;

public partial class APKManagerViewModel:ObservableObject
{
	public APKManagerViewModel(IAdbManager adbManager,IPackageManager packageManager,IToastLitterMessage toastLitterMessage
        ,IShowDialogService showDialogService)
	{
        AdbManager = adbManager;
        PackageManager = packageManager;
        ToastLitterMessage = toastLitterMessage;
        ShowDialogService = showDialogService;
    }

    [RelayCommand]
    async void Loaded()
    {
        var result = await  PackageManager.ParsePackage("D:\\jd.apk");
    }


    [RelayCommand]
    void OpenApkInstall()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "apk file (*.apk)|*.apk",
        };
        if (dialog.ShowDialog() is true)
        {
            ShowDialogService.Show<InstallApkDialog, string>(App.GetService<InstallApkDialog>(), dialog.FileName);
        }
    }
    public IAdbManager AdbManager { get; }
    public IPackageManager PackageManager { get; }
    public IToastLitterMessage ToastLitterMessage { get; }
    public IShowDialogService ShowDialogService { get; }
}
