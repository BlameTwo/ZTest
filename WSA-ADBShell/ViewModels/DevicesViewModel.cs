using AdbShell.Interfaces;
using AdbShell.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WSA_ADBShell.ViewModels.ItemsViewModels;

namespace WSA_ADBShell.ViewModels;

public partial class DevicesViewModel:ObservableObject
{
	public DevicesViewModel(IAdbManager adbManager)
    {
        AdbManager = adbManager;
        this.DeviceStatesList = new();
        AdbManager.DevicesCountChanged += AdbManager_DevicesCountChanged;
    }


    private void AdbManager_DevicesCountChanged(AdbShell.AdbManager adbManager, AdbShell.DevicesChangedArg deviceStateData)
    {
        switch (deviceStateData.State)
        {
            case AdbShell.DevicesChangedEnum.Add:
                deviceStateData.Data.ForEach((val) => App.DispatcherChanged(()=>DeviceStatesList.Add(val.ChildConvert<DeviceStateData, ADBManagerDevicesItemVM>())));
                break;
            case AdbShell.DevicesChangedEnum.Remove:
                deviceStateData.Data.ForEach((val) => 
                {
                    App.DispatcherChanged(() =>
                    {
                        foreach (var item in DeviceStatesList.ToArray())
                        {
                            if (val.DeviceName == item.DeviceName) DeviceStatesList.Remove(val.ChildConvert<DeviceStateData, ADBManagerDevicesItemVM>());
                        }
                    });
                });
                break;
            case AdbShell.DevicesChangedEnum.Changed:
                deviceStateData.Data.ForEach((val) =>
                {
                    App.DispatcherChanged(() =>
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
                App.DispatcherChanged(() => DeviceStatesList.Clear());
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
    async void Loaded()
    {
        RefreshDevice();
    }


    [ObservableProperty]
    ObservableCollection<ADBManagerDevicesItemVM> _DeviceStatesList;

    public IAdbManager AdbManager { get; }
}
