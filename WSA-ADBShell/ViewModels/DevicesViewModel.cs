using AdbShell.Interfaces;
using AdbShell.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace WSA_ADBShell.ViewModels;

public partial class DevicesViewModel:ObservableObject
{
	public DevicesViewModel(IAdbManager adbManager)
	{
        AdbManager = adbManager;
    }

    [RelayCommand]
    async void RefreshDevice()
    {
        var list = await AdbManager.GetDevicesList();
        this.DeviceStatesList = list.Data.ToObservable();
    }

    [ObservableProperty]
    ObservableCollection<DeviceStateData> _DeviceStatesList;

    public IAdbManager AdbManager { get; }
}
