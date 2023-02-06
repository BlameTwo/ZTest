using AdbShell.Interfaces;
using AdbShell.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AdbShell;

/// <summary>
/// 这是一个ADBManager管理器
/// 前往<see cref="IAdbManager"/>查看接口
/// </summary>
public partial class AdbManager: IAdbManager
{
    const string ADBServer = "127.0.0.1:58526";


    /// <summary>
    /// 线程token集合
    /// </summary>
    CancellationTokenSource tokensource = new();
    /// <summary>
    /// 是否连接
    /// </summary>
    bool _isConnect = false;

    string _connectname;

    public AdbManager(ProcessManager processManager)
    {
        ProcessManager = processManager;

        timer = new Timer(refershdevicecount,null,1000,1000);
    }

    
    public ProcessManager ProcessManager { get; }

    public async Task<AdbCommandResult> Connect(string address = null)
    {
        string str = null;
        if (address is null) str = ADBServer;
        var result = ProcessManager.GetProcess($"connect {str}", ProcessType.Adb);
        return await connect(result, tokensource.Token,str);
    }


    


    public Task<bool> GetDevices()
    {
        throw new NotImplementedException();
    }


    public Task<AdbCommandResult> Refersh(bool flage, string address = null)
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        tokensource.Cancel();
    }

    public async Task<bool> StartAdb(Action<string> message)
    {
        var pro = ProcessManager.GetProcess("start-server", ProcessType.Adb);
        return await Task.Run(async () =>
        {
            pro.Start();
            while (pro.StandardOutput.Peek()>-1)
            {
                var line = await pro.StandardOutput.ReadLineAsync();
                if(line.StartsWith("* daemon not running"))
                {
                    message.Invoke("ADB正在启动");
                }
                else if (line.StartsWith("* daemon started successfully"))
                {
                    message.Invoke("ADB启动成功!");
                    await Task.Delay(500);
                    return true;
                }
            }
            pro.WaitForExit();
            return false;
        });
    }


    public async Task<bool> KillAdb()
    {
        var pro = ProcessManager.GetProcess("kill-server", ProcessType.Adb);
        return await Task.Run(async () =>
        {
            pro.Start();
            var text = await pro.StandardOutput.ReadToEndAsync();
            pro.WaitForExit();
            //无输出后才是正常杀死adb进程
            if (string.IsNullOrWhiteSpace(text))
                return true;
            return false;
        });
    }

    

    

    
}
