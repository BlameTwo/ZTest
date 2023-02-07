using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using WSA_AdbShell.Models;
using WSA_ADBShell.Events.ViewModelsEvent;
using WSA_ADBShell.Services.Interfaces;
using WSA_ADBShell.ViewModels.ItemsViewModels;

namespace WSA_ADBShell.ViewModels;

public partial class ManagerViewModel:ObservableRecipient, IRecipient<TokenEvent>
{
    public ManagerViewModel(ITaskManager taskManager)
    {
        TaskManager = taskManager;
        TaskList = new();
        IsActive = true;
    }

    [RelayCommand]
    void Loaded()
    {
        foreach (var item in TaskManager.TaskManagers)
        {
            //筛选出已经显示的任务
            var list = TaskList.Where((val) => val.Token == item.Token)
                .ToList();
            if (list.Count == 0)
            {
                //为0则是没有找到显示项目
                TaskList.Add((TaskManagerItemVM)item);
            }
        }
    }

    public void Receive(TokenEvent message)
    {
        foreach (var item in TaskList.ToArray())
        {
            if(((ITaskToken)item).Token == message.RemoveTask.Token)
            {
                TaskList.Remove(item);
            }
        }
    }

    [ObservableProperty]
    ObservableCollection<TaskManagerItemVM> _TaskList;

    public ITaskManager TaskManager { get; }
}
