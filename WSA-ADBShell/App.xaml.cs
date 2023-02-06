using AAPTForNet;
using AdbShell;
using AdbShell.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using WSA_ADBShell.Services.Interfaces;

namespace WSA_ADBShell;

public partial class App : Application
{
    public IHost Host { get; }


    protected override void OnStartup(StartupEventArgs e)
    {
        var processman = GetService<ProcessManager>();
        processman.Adbpath = "D:\\platform-tools\\adb.exe";
        processman.Aaptpath = "D:\\platform-tools\\aapt.exe";
        AAPTool.AAPTpath = "D:\\platform-tools\\aapt.exe";
        var main = GetService<MainWindow>();
        GetService<IWindowManager>().Window = main;
        this.MainWindow = main;
        main.Show();
        base.OnStartup(e);
    }


    public App()
    {
        this.Host = 
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .RegiserViews()
            .RegiserViewModels()
            .RegiserService()
            .RegiserExtend()
            .Build();
    }
}

public partial class App
{
    /// <summary>
    /// 手动获得一个注册类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public static T GetService<T>()
    {
        if((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new System.Exception($"没有找到该类:{typeof(T)}");
        }
        return service;
    }

    public static object GetService(Type type)
    {
        var value = (App.Current as App)!.Host.Services.GetService(type);
        if (value != null)
        {
            return value;
        }
        throw new Exception($"这是一个严重的错误！并未在生命周期管理器中发现{type.FullName}");
    }
}
