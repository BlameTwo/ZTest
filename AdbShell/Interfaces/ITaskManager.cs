using AdbShell.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdbShell.Interfaces;

public interface ITaskManager
{
    public List<TaskManagerData> TaskManagers { get; set; }
}
