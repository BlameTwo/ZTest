using AdbShell;
using AdbShell.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface.AppInterfaces;
using WSA_ADBShell.Services.Interfaces;

namespace WSA_ADBShell.ViewModels.ItemsViewModels;

[INotifyPropertyChanged]
public partial class ADBManagerDevicesItemVM: AdbShell.Models.DeviceStateData
{
    public ADBManagerDevicesItemVM()
    {
        TaskManager = App.GetService<ITaskManager>();
        ToastLitterMessage =App.GetService<IToastLitterMessage>();
    }

    public ITaskManager TaskManager { get; }
    public IToastLitterMessage ToastLitterMessage { get; }

    [RelayCommand]
    void ShowScropy()
    {
        var pro = App.GetService<ProcessManager>().GetProcess($"-s {this.DeviceName}", ProcessType.Scropy);
        TaskManager.TaskManagers.Add(new WSA_AdbShell.Models.TaskManagerData() { Title = "Scropy投屏", SubTitle = this.DeviceName,process = pro });
        ToastLitterMessage.Show("请前往管理中运行投屏任务");
    }
}
