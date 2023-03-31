using ChatGPT_GUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ZTest.Tools.Interfaces;
using ZTest.Tools.Services;

namespace ChatGPT_GUI.ViewModels;

public partial class MainViewModel: ObservableRecipient {

    public MainViewModel(IToastLitterMessage toastLitterMessage, IOpenAIService openAIService, IAppNavigationViewService appNavigationViewService,ILocalSetting localSetting)
    {
        ToastLitterMessage = toastLitterMessage;
        OpenAIService = openAIService;
        AppNavigationViewService = appNavigationViewService;
        LocalSetting = localSetting;
    }

    public IToastLitterMessage ToastLitterMessage { get; }
    public IOpenAIService OpenAIService { get; set; }
    public IAppNavigationViewService AppNavigationViewService { get; }
    public ILocalSetting LocalSetting { get; }

    [RelayCommand]
    void NavigationChanged(object type)
    {
        if(type is string str)
        {
            var page = App.GetSerivces<ModelPage>();
            AppNavigationViewService.Navigation(page, str);
        }
    }

}
