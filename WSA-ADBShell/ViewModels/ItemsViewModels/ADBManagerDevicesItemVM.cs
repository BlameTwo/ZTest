using AdbShell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface.AppInterfaces;
using WSA_AdbShell.Models;
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
        var result = new TaskManagerItemVM() { TaskType = TaskManagerType.Screen, Title = "Scropy投屏", SubTitle = this.DeviceName, process = pro };
        result.Init(TaskManager);
        TaskManager.TaskManagers.Add(result);
        ToastLitterMessage.Show($"请前往管理中运行任务");
    }
}
