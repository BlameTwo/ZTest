using AdbShell.Models;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdbShell;

public partial class PackageManager
{

    Regex rxone = new Regex("'.*?\'");
    private async Task<ApkCoreData> parsepackage(Process result, string path)
    {
        if (result == null) return null;
        result.Start();
        ApkCoreData data = new();
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
            }
        }
        result.WaitForExit();
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
}
