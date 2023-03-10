using ChatGPT_GUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.GPT3.Extensions;
using OpenAI.GPT3.Interfaces;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Interface.AppInterfaces.Services;
using SimpleUI.Services;
using SimpleUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT_GUI; 
public static class HostRegister 
{
    public static IHostBuilder RegisterService(this IHostBuilder host) {
        host.ConfigureServices(services => 
        {
            services.AddOpenAIService(setting => {
                setting.ApiKey = "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";
            });
            services.AddSingleton<IToastLitterMessage, ToastLitterMessage>();
            services.AddSingleton<IShowDialogService, ShowDialogService>();
        });
        return host;
    }

    public static IHostBuilder RegisterView(this IHostBuilder host) {
        host.ConfigureServices(services => {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
        });
        return host;
    }
}

public partial class App {
    public static IOpenAIService GetOpenAIService() {
        if ((App.Current as App)!.Host.Services.GetService(typeof(IOpenAIService)) is not IOpenAIService service) {
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
