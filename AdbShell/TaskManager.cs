using AdbShell.Interfaces;
using AdbShell.Models;
using System.Collections.Generic;

namespace AdbShell;

internal class TaskManager : ITaskManager
{
    public List<TaskManagerData> TaskManagers { get; set; }
}
