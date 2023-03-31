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

    public MainViewModel(IToastLitterMessage toastLitterMessage,IShowDialogService showDialogService, IOpenAIService openAIService, IAppNavigationViewService appNavigationViewService,ILocalSetting localSetting)
    {
        ToastLitterMessage = toastLitterMessage;
        ShowDialogService = showDialogService;
        OpenAIService = openAIService;
        AppNavigationViewService = appNavigationViewService;
        LocalSetting = localSetting;
        ModelName = "选择一个对话";
    }

    [RelayCommand]
    void ShowSetting()
    {
        ShowDialogService.Show(App.GetSerivces<SettingDialog>(), "空的");
    }

    [ObservableProperty]
    string _ModelName;

    public IToastLitterMessage ToastLitterMessage { get; }
    public IShowDialogService ShowDialogService { get; }
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
            ModelName = str;
        }
    }

}
