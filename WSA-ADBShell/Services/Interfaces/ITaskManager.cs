using AdbShell.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WSA_AdbShell.Models;

namespace WSA_ADBShell.Services.Interfaces;

public interface ITaskManager
{
    public List<TaskManagerData> TaskManagers { get; set; }
}
