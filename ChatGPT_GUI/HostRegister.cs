using ChatGPT_GUI.Services;
using ChatGPT_GUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.GPT3.Extensions;
using OpenAI.GPT3.Interfaces;
using SimpleUI.Interface;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Interface.AppInterfaces.Services;
using SimpleUI.Services;
using SimpleUI.Themes;
using SimpleUI.Utils;
using ZTest.Tools.Interfaces;
using ZTest.Tools.Services;

namespace ChatGPT_GUI;
public static class HostRegister 
{
    public static IHostBuilder RegisterService(this IHostBuilder host) {
        host.ConfigureServices(services => 
        {
            services.AddOpenAIService((setting) => {
                setting.ApiKey = "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";
            });
            services.AddSingleton<IToastLitterMessage, ToastLitterMessage>();
            services.AddSingleton<IShowDialogService, ShowDialogService>();
            services.AddSingleton<IThemeApply<App>, ThemeApply<App>>();
            services.AddSingleton<ILocalSetting, LocalSetting>();
            services.AddSingleton<IAppNavigationViewService, AppNavigationViewService>();
            services.AddSingleton<IVITSService, VITSService>();
        });
        return host;
    }

    public static IHostBuilder RegisterView(this IHostBuilder host) {
        host.ConfigureServices(services => {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SettingDialog>();
            services.AddSingleton<SettingViewModel>();
            services.AddTransient<ModelPage>();
            services.AddTransient<ModelViewModel>();
        });
        return host;
    }
}

public partial class App {
    public static IOpenAIService GetOpenAIService() {
        if ((App.Current as App)!.Host.Services.GetRequiredService(typeof(IOpenAIService)) is not IOpenAIService service) {
            throw new System.Exception($"注册项目缺少{typeof(IOpenAIService)}");
        }
        return service;   
    }

    public static T GetSerivces<T>() {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service) {
            throw new System.Exception($"注册项目缺少{typeof(T)}");
        }
        return service;
    }
}
