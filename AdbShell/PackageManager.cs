using AdbShell.Interfaces;
using AdbShell.Models;
using AndroidXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace AdbShell;

public partial class PackageManager : IPackageManager
{

    /// <summary>
    /// 线程token集合
    /// </summary>
    CancellationTokenSource tokensource = new();

    public PackageManager(ProcessManager processManager)
    {
        ProcessManager = processManager;
    }
    public string AAPT_Path { get; set; }
    public ProcessManager ProcessManager { get; }
    public Dictionary<string, string> Use_Permissons => 
        new Dictionary<string, string>().GetDefaultUsePermissons();

    Dictionary<string, string> IPackageManager.Use_Permissons { get; set; }

    public async Task<ApkCoreData> ParsePackage(string path)
    {
        if (Path.GetExtension(path) != ".apk") return null;
        var result =  ProcessManager.GetProcess($"dump badging {path}", ProcessType.Aapt);
        return await Task.Run(async () =>
        {
            return await parsepackage(result, path);
        });
    }


    public async Task<AdbCommandResult> InstallAsync(string apkpath, Action<string> MessageCallBack, DeviceStateData state, string parame = null)
    {
        string arg = $"-s {state.DeviceName} install \"{apkpath}\" {parame}";
        var result = ProcessManager.GetProcess(arg, ProcessType.Adb);
        return await Task.Run(async () =>
        {
            return await install(result, tokensource.Token, MessageCallBack);
        });
    }
}


