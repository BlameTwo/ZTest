using AdbShell.Interfaces;
using AdbShell.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WSA_ADBShell.Services.Interfaces;
using WSA_ADBShell.ViewModels.ItemsViewModels;
using WSA_ADBShell.Views.Dialogs;

namespace WSA_ADBShell.ViewModels;

public partial class DevicesViewModel:ObservableObject
{
	public DevicesViewModel(IAdbManager adbManager,IWindowManager windowManager,
        IShowDialogService  showDialogService,IToastLitterMessage toastLitterMessage)
    {
        AdbManager = adbManager;
        WindowManager = windowManager;
        ShowDialogService = showDialogService;
        ToastLitterMessage = toastLitterMessage;
        this.DeviceStatesList = new();
        AdbManager.DevicesCountChanged += AdbManager_DevicesCountChanged;
    }

    [RelayCommand]
    void StopServer() => AdbManager.KillAdb();

    [RelayCommand]
    async Task StartServerAsync() => await AdbManager.StartAdb((mess) =>
    {

    },"5555");


    [RelayCommand]
    async Task ConnecWSA()
    {
        var result = await AdbManager.Connect();
        ToastLitterMessage.Show($"状态：{result.Success}  {result.Message}");
    }


    private void AdbManager_DevicesCountChanged(AdbShell.AdbManager adbManager, AdbShell.DevicesChangedArg deviceStateData)
    {
        switch (deviceStateData.State)
        {
            case AdbShell.DevicesChangedEnum.Add:
                deviceStateData.Data.ForEach((val) =>
                {
                    WindowManager.DispatcherChanged(() =>
                    {
                        this.DeviceStatesList.Add(val.ChildConvert<DeviceStateData, ADBManagerDevicesItemVM>());
                    });
                });
                break;
            case AdbShell.DevicesChangedEnum.Remove:
                deviceStateData.Data.ForEach((val) => 
                {
                    WindowManager.DispatcherChanged(() =>
                    {
                        foreach (var item in DeviceStatesList.ToArray())
                        {
                            if(val.DeviceName == item.DeviceName)
                                DeviceStatesList.Remove(item);
                        }
                    });
                });
                break;
            case AdbShell.DevicesChangedEnum.Changed:
                deviceStateData.Data.ForEach((val) =>
                {
                    WindowManager.DispatcherChanged(() =>
                    {
                        foreach (var item in DeviceStatesList.ToArray())
                        {
                            if (item.DeviceName != val.DeviceName) continue;
                            DeviceStatesList.Remove(item);
                            DeviceStatesList.Add(val.ChildConvert<DeviceStateData, ADBManagerDevicesItemVM>());
                        }
                    });
                });
                break;
            case AdbShell.DevicesChangedEnum.ClearAll:
                WindowManager.DispatcherChanged(() => DeviceStatesList.Clear());
                break;
        }
    }

    [RelayCommand]
    async void RefreshDevice()
    {
        DeviceStatesList.Clear();
        var list = await AdbManager.GetDevicesList();
        foreach (var item in list.Data)
        {
            ADBManagerDevicesItemVM vm
                = item.ChildConvert<DeviceStateData, ADBManagerDevicesItemVM>();
            DeviceStatesList.Add(vm);
        }
    }


    [RelayCommand]
    void SelectDevice(DeviceStateData deviceStateData)
    {
        if(deviceStateData == null) return;
        AdbManager.HotDevice = deviceStateData;
        WindowManager.SetTitleBarText(RunResource.GetAppName() + $"  [调试：{deviceStateData.DeviceName}]");
    }


    [RelayCommand]
    async void Loaded()
    {
        RefreshDevice();
    }


    [ObservableProperty]
    ObservableCollection<ADBManagerDevicesItemVM> _DeviceStatesList;

    public IAdbManager AdbManager { get; }
    public IWindowManager WindowManager { get; }
    public IShowDialogService ShowDialogService { get; }
    public IToastLitterMessage ToastLitterMessage { get; }
}
