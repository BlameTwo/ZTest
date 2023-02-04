using AdbShell.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdbShell.Interfaces;

/// <summary>
/// 请查看<see cref="AdbManager"/>的实现
/// </summary>
public interface IAdbManager
{

    //./aapt d badging bili.apk

    /// <summary>
    /// 进行链接
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public Task<AdbCommandResult> Connect(string address=null);
    /// <summary>
    /// 刷新状态
    /// </summary>
    /// <param name="flage">是否开启</param>
    /// <param name="address">新地址</param>
    /// <returns></returns>
    public Task<AdbCommandResult> Refersh(bool flage,string address=null);

    /// <summary>
    /// 获得调试的设备
    /// </summary>
    /// <returns></returns>
    public Task<bool> GetDevices();

    public Task<AdbCommandResult> InstallAsync(string apkpath, Action<string> MessageCallBack,string parame=null);


    public Task<AdbDataResult<List<DeviceStateData>>> GetDevicesList();

    /// <summary>
    /// 停止全部操作
    /// </summary>
    /// <returns></returns>
    public void Stop();
}
