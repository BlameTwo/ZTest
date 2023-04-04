using BingChatApiLibs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using ZTest.Tools.Interfaces;

namespace ChatGPT_GUI.ViewModels;

public partial class BingPageViewModel:ObservableObject
{

    public BingPageViewModel(ILocalSetting localSetting)
    {
        LocalSetting = localSetting;
    }
    BingChatSettings _option { get; set; } = new();

    [RelayCommand]
    async void Loaded()
    {
        var bingcookie = await LocalSetting.ReadConfig("BingCookie");

        _option = new BingChatSettings()
        {
            Style = BingChatSettings.CharStyle.Creative,
            Token = bingcookie.ToString()
        };
    }

    [RelayCommand]
    async void SelectBing(string header)
    {

    }

    [ObservableProperty]
    Visibility _ContentVisibility;



    public ILocalSetting LocalSetting { get; }
}
