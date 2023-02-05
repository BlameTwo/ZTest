using AdbShell.Interfaces;
using AdbShell.Models;
using AndroidXml;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace AdbShell;

public partial class PackageManager : IPackageManager
{
    public PackageManager(ProcessManager processManager)
    {
        ProcessManager = processManager;
    }
    public string AAPT_Path { get; set; }
    public ProcessManager ProcessManager { get; }

    public async Task<ApkCoreData> ParsePackage(string path)
    {
        if (Path.GetExtension(path) != ".apk") return null;
        var result =  ProcessManager.GetProcess($"dump badging {path}", ProcessType.Aapt);
        return await Task.Run(async () =>
        {
            return await parsepackage(result, path);
        });
    }

}


