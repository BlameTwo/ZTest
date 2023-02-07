using AdbShell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WSA_ADBShell.ViewModels.ItemsViewModels;

[INotifyPropertyChanged]
public partial class ADBManagerDevicesItemVM: AdbShell.Models.DeviceStateData
{
    [RelayCommand]
    void ShowScropy()
    {
        var pro = App.GetService<ProcessManager>().GetProcess($"-s {this.DeviceName}", ProcessType.Scropy);
        pro.Start();
    }
}
