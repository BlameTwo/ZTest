using WSA_AdbShell.Models;
using System.Collections.Generic;
using WSA_ADBShell.Services.Interfaces;

namespace AdbShell;

internal class TaskManager : ITaskManager
{
    public List<TaskManagerData> TaskManagers { get; set; } = new();
}
