using SonsOfTheForesrtSaveLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SonsOfTheForesrtSaveLib;

/// <summary>
/// 物品管理类
/// </summary>
public static class PackageManager {

    static readonly string FileName = "PackageSource.json";
    public static PackageData PackageData { get; set; }
    

    /// <summary>
    /// 获得当前可执行的所有物品
    /// </summary>
    /// <returns>物品信息</returns>
    public static void RefershPackage() {
        using StreamReader stream = new StreamReader(FileName);
        PackageData data = JsonSerializer.Deserialize<PackageData>(stream.ReadToEnd());
        PackageData = data;
    }


}
