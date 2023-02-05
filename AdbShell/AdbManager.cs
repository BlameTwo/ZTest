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

        //DevicesInit();

        timer = new Timer(refershdevicecount,null,1000,1000);
    }

    

    public ProcessManager ProcessManager { get; }

    public async Task<AdbCommandResult> Connect(string address = null)
    {
        string str = null;
        if (address is null) str = ADBServer;
        var result = ProcessManager.GetProcess($"connect {str}");
        return await connect(result, tokensource.Token,str);
    }


    private async Task<AdbCommandResult> connect(Process process, CancellationToken token, string connectname)
    {
        return await Task.Run(async () =>
        {
            if (token.IsCancellationRequested) return new AdbCommandResult() { Success = false, Message = "已经取消操作" };
            if (process == null) return new AdbCommandResult() { Success = false, Message = "已经取消操作" };
            string errormessage=null;
            process.Start();
            //是否到尾部流
            while (!process.StandardOutput.EndOfStream)
            {
                if (token.IsCancellationRequested) return new() { Success = false, Message = "已经取消操作" };
                var str = await process.StandardOutput.ReadLineAsync();
                if (str.StartsWith("failed to authenticate")
                    || str.StartsWith("cannot connect to"))
                {
                    return new AdbCommandResult() { Success = false, Message = "未检测到WSA在运行，请通过开始菜单启动WSA" };
                }
                else if (str.StartsWith("already connected"))
                {
                    _isConnect = true;
                    _connectname = connectname;
                    return new AdbCommandResult() { Success = true, Message = "WSA已经连接" };
                }
                else
                {
                    errormessage = str;
                }
            }
            return new() { Success = false, Message = $"Error:   {errormessage}" };
        });
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

    private async Task<AdbCommandResult> install(Process result, CancellationToken token,Action<string> action)
    {
        if (token.IsCancellationRequested) return new AdbCommandResult() { Success = false, Message = "已经取消操作" };
        if (result == null) return new AdbCommandResult() { Success = false, Message = "进程对象为空" };
        result.Start();
        while (!result.StandardOutput.EndOfStream)
        {
            if (token.IsCancellationRequested) return new() { Success = false, Message = "已经取消操作" };
            var line = await result.StandardOutput.ReadLineAsync();
            if (line.StartsWith("Performing Streamed Install"))
            {
                action.Invoke("正在安装…………");
            }
            else if (line.StartsWith("Success"))
            {
                action.Invoke("安装完毕");
                return new AdbCommandResult() { Success = true, Message = "安装完毕" };
            }
        }
        result.WaitForExit();
        return new AdbCommandResult() { Success = true, Message = "命令通道通过" };
        
    }

    public async Task<AdbCommandResult> InstallAsync(string apkpath, Action<string> MessageCallBack, string parame = null)
    {
        string arg = $"-s {_connectname} install \"{apkpath}\" {parame}";
        var result = ProcessManager.GetProcess(arg);
        return await Task.Run(async () =>
        {
            return await install(result, tokensource.Token, MessageCallBack);
        });
    }
}
