using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface.AppInterfaces;
using System;
using System.Windows.Media.Imaging;

namespace SonsOfTheForesrtSave_GUI.ViewModels;

public partial class MainViewModel:ObservableRecipient
{
    public MainViewModel(IAppNavigationViewService appNavigationViewService,IToastLitterMessage toastLitterMessage)
    {
        IsActive = true;
        AppNavigationViewService = appNavigationViewService;
        ToastLitterMessage = toastLitterMessage;
        
    }


    [RelayCommand]
    void Navigation(Type type)
    {
        if (type == null) return;
        var result = App.GetService(type);
        AppNavigationViewService.Navigation(result, null);
        
    }

    
    public IAppNavigationViewService AppNavigationViewService { get; }
    public IToastLitterMessage ToastLitterMessage { get; }
}
