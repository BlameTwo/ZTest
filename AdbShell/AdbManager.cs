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

    

    public async Task<AdbCommandResult> InstallAsync(string apkpath, Action<string> MessageCallBack, string parame = null)
    {
        string arg = $"-s {_connectname} install \"{apkpath}\" {parame}";
        var result = ProcessManager.GetProcess(arg,ProcessType.Adb);
        return await Task.Run(async () =>
        {
            return await install(result, tokensource.Token, MessageCallBack);
        });
    }
}
