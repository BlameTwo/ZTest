using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WSA_AdbShell.Models;
using WSA_ADBShell.Services.Interfaces;
using WSA_ADBShell.ViewModels.ItemsViewModels;

namespace WSA_ADBShell.ViewModels;

public partial class ManagerViewModel:ObservableObject
{
    public ManagerViewModel(ITaskManager taskManager)
    {
        TaskManager = taskManager;
        TaskList = new();
    }

    [RelayCommand]
    void Loaded()
    {
        foreach (var item in TaskManager.TaskManagers)
        {
            TaskList.Add(item.ChildConvert<TaskManagerData, TaskManagerItemVM>());
        }
    }

    [ObservableProperty]
    ObservableCollection<TaskManagerItemVM> _TaskList;

    public ITaskManager TaskManager { get; }
}
