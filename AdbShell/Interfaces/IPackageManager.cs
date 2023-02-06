using AdbShell.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdbShell.Interfaces;

/// <summary>
/// 安装包管理器
/// </summary>
public interface IPackageManager
{

    /// <summary>
    /// 解析安装包
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Task<ApkCoreData> ParsePackage(string path);



    public Task<AdbCommandResult> InstallAsync(string apkpath, Action<string> MessageCallBack, DeviceStateData state, string parame = null);

    /// <summary>
    /// 权限列表
    /// </summary>
    public Dictionary<string,string> Use_Permissons { get; set; }
}
