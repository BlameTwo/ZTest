namespace AdbShell.Models;

/// <summary>
/// 使用apt命令读取安卓包后返沪的信息
/// </summary>
public class PackageApkData
{
    public string PackageName { get; set; }

    public string PackageVersionCode { get; set; }

    public string PackageVersion { get; set; }

    public string platformBuildVersionName { get; set; }

    public string compileSdkVersion { get; set; }

    public string compileSdkVersionCodename { get; set; }

    public string sdkVersion { get; set; }

    public string targetSdkVersion { get; set; }
}
