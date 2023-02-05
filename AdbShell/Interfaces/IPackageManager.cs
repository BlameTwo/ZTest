using AdbShell.Models;
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
}
