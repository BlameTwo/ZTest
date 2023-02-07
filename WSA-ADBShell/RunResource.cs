using AdbShell;
using AdbShell.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Interface.AppInterfaces.Services;
using SimpleUI.Services;
using SimpleUI.Utils;
using WSA_ADBShell.Services;
using WSA_ADBShell.Services.Interfaces;
using WSA_ADBShell.ViewModels;
using WSA_ADBShell.ViewModels.DialogViewModels;
using WSA_ADBShell.Views;
using WSA_ADBShell.Views.Dialogs;

namespace WSA_ADBShell;

public static class RunResource
{
    public static IHostBuilder RegiserViews(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((content, service) =>
        {
            service.AddSingleton<MainWindow>();
            service.AddSingleton<APKManagerPage>();
            service.AddSingleton<DevicesPage>();
            service.AddTransient<InstallApkDialog>();
            service.AddSingleton<ManagerPage>();
        });
        return hostBuilder; 
    }


    public static IHostBuilder RegiserViewModels(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((content, service) =>
        {
            service.AddSingleton<MainViewModel>();
            service.AddSingleton<ManagerViewModel>();
            service.AddSingleton<APKManagerViewModel>();
            service.AddSingleton<DevicesViewModel>();
            service.AddTransient<InstallApkViewModel>();
        });
        return hostBuilder;
    }

    public static IHostBuilder RegiserService(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((content, service) =>
        {

            //进程服务
            service.AddSingleton<ProcessManager>();

            //ADB管理器
            service.AddSingleton<IAdbManager,AdbManager>();
            service.AddSingleton<ITaskManager,TaskManager>();

            //包管理服务
            service.AddSingleton<IPackageManager, PackageManager>();

        });
        return hostBuilder;
    }

    public static IHostBuilder RegiserExtend(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((content, service) =>
        {
            //导航服务
            service.AddSingleton<IAppNavigationViewService, AppNavigationViewService>();

            //对话框
            service.AddSingleton<IShowDialogService, ShowDialogService>();

            //小弹窗服务
            service.AddSingleton<IToastLitterMessage, ToastLitterMessage>();

            service.AddSingleton<IWindowManager, WindowManager>();
        });
        return hostBuilder;
    }


    public static string GetAppName() => "ADBShell-Toolkit";


}


