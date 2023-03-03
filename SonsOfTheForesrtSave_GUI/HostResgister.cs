using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleUI.Interface;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Interface.AppInterfaces.Services;
using SonsOfTheForesrtSave_GUI.Services;
using SonsOfTheForesrtSave_GUI.ViewModels;
using SonsOfTheForesrtSave_GUI.Views;
using System;

namespace SonsOfTheForesrtSave_GUI;

public static class HostResgister
{
    public static IHostBuilder RegisterView(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<StateSavePage>();
            services.AddTransient<FirstSteamConfig>();
            services.AddTransient<UpdateView>();
            services.AddTransient<WorldSaveStatePage>();
            services.AddTransient<PackagePage>();
        });
        return builder;
    }

    public static IHostBuilder RegisterExtend(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IAppNavigationViewService, AppNavigationViewService>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IToastLitterMessage, ToastLitterMessage>();
        });
        return builder;
    }

    public static IHostBuilder RegisterViewModel(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTransient<FirstSteamConfigViewModel>();
            services.AddTransient<StateSaveViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<WorldSaveStateViewModel>();
            services.AddTransient<PackageViewModel>();
        });
        return builder;
    }
}

public partial class App
{
    public static T GetService<T>()
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new System.Exception($"注册项目缺少{typeof(T)}");
        }
        return service;
    }

    public static object GetService(Type type)
    {
        if ((App.Current as App)!.Host.Services.GetService(type) == null)
        {
            throw new System.Exception($"注册项目缺少{type}");
        }
        return (App.Current as App)!.Host.Services.GetService(type)!;
    }
}
