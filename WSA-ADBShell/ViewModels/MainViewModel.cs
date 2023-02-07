using AdbShell.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface.AppInterfaces;
using System;

namespace WSA_ADBShell.ViewModels;

public partial class MainViewModel:ObservableObject
{
	public MainViewModel(
        IAdbManager adbManager,
        IAppNavigationViewService appNavigationViewService,
        IToastLitterMessage toastLitterMessage)
	{
        AdbManager = adbManager;
        AppNavigationViewService = appNavigationViewService;
        ToastLitterMessage = toastLitterMessage;
    }



    [RelayCommand]
    void Nav(object type)
    {
        if (type == null) return;
        AppNavigationViewService.Navigation(App.GetService((Type)type),null);
    }


    public IAdbManager AdbManager { get; }
    public IAppNavigationViewService AppNavigationViewService { get; }
    public IToastLitterMessage ToastLitterMessage { get; }
}
