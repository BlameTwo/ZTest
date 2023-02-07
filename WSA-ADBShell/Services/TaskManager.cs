using System.Collections.Generic;
using WSA_ADBShell.Services.Interfaces;
using WSA_ADBShell.ViewModels.ItemsViewModels;

namespace AdbShell;

internal class TaskManager : ITaskManager
{
    public List<ITaskToken> TaskManagers { get; set; } = new();

    public string CraeteToken<T>(T item)
        where T : ITaskToken
    {
        string one = this.TaskManagers.Count.ToString();
        string ts = item.TokenSource;
        string name = item.TokenName;
        return $"{one}-{name}-{ts}";
    }

    public bool RemoveToken<T>(T item) where T : ITaskToken
    {
        try
        {
            if(!TaskManagers.Contains(item))
                return false;
            TaskManagers.Remove(item);
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}
