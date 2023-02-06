using AdbShell.Models;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdbShell;

public partial class PackageManager
{

    Regex rxone = new Regex("'.*?\'");
    private async Task<ApkCoreData> parsepackage(Process result, string path)
    {
        if (result == null) return null;
        result.Start();
        ApkCoreData data = new() { Permissons = new() };
        while (result.StandardOutput.Peek() > -1)
        {
           string line = await result.StandardOutput.ReadLineAsync();
           if (line.StartsWith("package"))      //如果为package包开头，则为package基础信息
           {
                data.Package = Parsepackagestr(line);
           }else if(line.StartsWith("sdkVersion")
                || line.StartsWith("targetSdkVersion"))
            {
                data.Package = parsepackagesdkversion(line, data.Package);
            }else if (line.StartsWith("application:"))
            {
                var applicationlist =  line.Substring(line.IndexOf(":") + 1).Split(' ');
                foreach (var item in applicationlist)
                {
                    if(string.IsNullOrWhiteSpace(item)) continue;
                    if (line.StartsWith("label")) data.AppName = rxone.Match(item).Value;
                }
            }else if (line.StartsWith("uses-permission:"))
            {

            }
        }
        result.WaitForExit();
        return data;
    }

    private ApkCoreData parsePackagepermissons(string line, ApkCoreData data)
    {
        //加权限原名
        data.Permissons.Add(rxone.Match(line).Value);
        return data;
    }

    private PackageApkData parsepackagesdkversion(string line,PackageApkData data)
    {
        string value = rxone.Match(line).Value;
        if (value == null) return data;
        if (line.StartsWith("sdkVersion"))
            data.sdkVersion = value;
        else if (line.StartsWith("targetSdkVersion"))
            data.targetSdkVersion = value;
        return data;
    }

    private PackageApkData Parsepackagestr(string line, PackageApkData data =null)
    {
        if (data == null) data = new();
        var text = line.Substring(line.IndexOf(":") + 1).Split(' ');
        foreach (var item in text)
        {
            if (string.IsNullOrWhiteSpace(item)) continue;
            string value = rxone.Match(item).Value;
            switch (item.Substring(0,item.IndexOf("=")))
            {
                case "name":
                    data.PackageName = value;
                    break;
                case "versionCode":
                    data.PackageVersionCode = value;
                    break;
                case "versionName":
                    data.PackageVersion = value;
                    break;
                case "platformBuildVersionName":
                    data.platformBuildVersionName = value;
                    break;
                case "compileSdkVersion":
                    data.compileSdkVersion= value;
                    break;
                case "compileSdkVersionCodename":
                    data.compileSdkVersionCodename= value;
                    break;
            }
        }
        return data;
    }


    private async Task<AdbCommandResult> install(Process result, CancellationToken token, Action<string> action)
    {
        if (token.IsCancellationRequested) return new AdbCommandResult() { Success = false, Message = "已经取消操作" };
        if (result == null) return new AdbCommandResult() { Success = false, Message = "进程对象为空" };
        result.Start();
        while (result.StandardOutput.Peek() > -1)
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
}
