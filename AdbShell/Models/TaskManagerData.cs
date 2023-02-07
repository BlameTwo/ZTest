using AdbShell.Events;
using System;
using System.Diagnostics;
using System.Threading;

namespace AdbShell.Models;


public class TaskManagerData
{
    public event EventHandler Exited
    {
        add
        {
            ExitHandle += value;
        }
        remove
        {
            ExitHandle -= value;
        }
    }

    Process _process { get; set; }

    /// <summary>
    /// 进行输出
    /// </summary>
    public event TaskManagerDelegate OutputChanged 
    {
        add
        {
            TaskManagerHandle += value;
        }
        remove
        {
            TaskManagerHandle -= value;
        }
    }


    CancellationTokenSource TokenSource = null;

    public EventHandler ExitHandle;
    public TaskManagerDelegate TaskManagerHandle;


    public void Start()
    {
        
    }


    
    /// <summary>
    /// 取消当前任务
    /// </summary>
    public void Close()
    {
        TokenSource.Cancel();
        ExitHandle.Invoke(this,null);
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 副标题
    /// </summary>
    public string SubTitle { get; set; }
}
