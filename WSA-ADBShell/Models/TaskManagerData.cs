using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Threading;
using WSA_AdbShell.Events;
using WSA_ADBShell.Services.Interfaces;

namespace WSA_AdbShell.Models;


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

    public Process process { get; set; }

    public ITaskManager TaskManager { get; set; }

    /// <summary>
    /// 任务分类
    /// </summary>
    public TaskManagerType TaskType { get; set; }



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


    public void StartProcess()
    {
        if (process == null) this.Close();
        TokenSource = new();
        process.Start();
        process.EnableRaisingEvents = true;
        process.BeginOutputReadLine();
        process.OutputDataReceived += (s, e) =>
        {
            if (TaskManagerHandle == null) return;
            TaskManagerHandle.Invoke(new TaskManagerEvent()
            {
                Arg = new TaskManagerEventArg()
                {
                    IsExit = false,
                    Output = e.Data
                }
            });
        };
        process.Exited += (s, e) =>
        {
            if (TaskManagerHandle == null) return;
            TaskManagerHandle.Invoke(new TaskManagerEvent()
            {
                Arg = new TaskManagerEventArg()
                {
                    IsExit = true,
                    Output = process.ExitCode.ToString()
                }
            });
            Close();
        };
    }


    
    /// <summary>
    /// 取消当前任务
    /// </summary>
    public void Close()
    {
        TokenSource.Cancel();
        if(ExitHandle != null)
            ExitHandle.Invoke(this,null);
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 副标题
    /// </summary>
    public string SubTitle { get; set; }
}


public enum TaskManagerType
{
    /// <summary>
    /// 屏幕任务
    /// </summary>
    Screen
}
