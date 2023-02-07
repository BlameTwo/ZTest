using AdbShell.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdbShell.Events;

public delegate void TaskManagerDelegate(TaskManagerEvent taskManagerEvent);

public class TaskManagerEvent:EventArgs
{
    public object sender { get; set; }

    public TaskManagerEventArg Arg { get; set; }
}


public class TaskManagerEventArg
{
    /// <summary>
    /// 当前的最近输出
    /// </summary>
    public string Output { get; set; }

    /// <summary>
    /// 任务token
    /// </summary>
    public int Token { get; set; }

    /// <summary>
    /// 是否退出
    /// </summary>
    public string IsExit { get; set; }
    
    /// <summary>
    /// Task管理器
    /// </summary>
    public ITaskManager TaskManager { get; set; }
}
