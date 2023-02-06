using System.Collections.Generic;

namespace AdbShell.Models;

public class ApkCoreData
{

    /// <summary>
    /// Package包解析版本信息和包信息
    /// </summary>
    public PackageApkData Package { get; set; }

    /// <summary>
    /// App显示名称
    /// </summary>
    public string AppName { get; set; }

    /// <summary>
    /// 中文名称（一般为空
    /// </summary>
    public string AppName_ZH {get;set;}

    /// <summary>
    /// 权限列表（原名
    /// </summary>
    public List<string> Permissons { get; set; }
}



