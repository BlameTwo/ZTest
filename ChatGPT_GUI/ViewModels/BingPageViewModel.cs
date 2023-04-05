
using ChatGPT_GUI.Bing.Models;
using ChatGPT_GUI.Bing_Bot;
using ChatGPT_GUI.Bing_Bot.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using ZTest.Tools.Interfaces;
using static ChatGPT_GUI.Bing_Bot.BingChatSettings;

namespace ChatGPT_GUI.ViewModels;

public partial class BingPageViewModel:ObservableObject
{

    public BingPageViewModel(ILocalSetting localSetting)
    {
        LocalSetting = localSetting;
        BingChatClient = new(_option);
    }

    private object? bingcookie;


    [ObservableProperty]
    ObservableCollection<string> _ListChat=new();

    private CancellationTokenSource? chatCts;

    BingChatSettings _option { get; set; } = new();

    [RelayCommand]
    async void Loaded()
    {
        bingcookie = await LocalSetting.ReadConfig("BingCookie");
        if(bingcookie == null)
        {
            this.CardVisibility = Visibility.Visible;
            this.ContentVisibility = Visibility.Collapsed;
        }
        else
        {
            this.CardVisibility = Visibility.Collapsed;
            this.ContentVisibility = Visibility.Visible;
        }
        _option = new BingChatSettings()
        {
            Style = BingChatSettings.CharStyle.Creative,
            Token = bingcookie!.ToString()!
        };
        BingChatClient = new(_option);
    }

    [RelayCommand]
    async void SelectBing(string header)
    {
        CharStyle style = CharStyle.Creative;
        switch (header)
        {
            case "创造力":
                style = CharStyle.Creative;
                break;
            case "平衡感":
                style = CharStyle.Balanced;
                break;
            case "精准感":
                style = CharStyle.Precise;
                break;
        }
        _option = new BingChatSettings()
        {
            Style = style,
            Token = bingcookie!.ToString()!
        };
    }

    ConversationSession _oldrequest;

    [RelayCommand]
    async void Ask(string text)
    {
        chatCts = new();
        BingRequest request = new BingRequest(text) { Session = _oldrequest };
        var result = await BingChatClient.ChatAsync(request, chatCts.Token);
        _oldrequest = result.Session;
        ListChat.Add(result.Text);
    }

    [ObservableProperty]
    Visibility _ContentVisibility;

    [ObservableProperty]
    Visibility _CardVisibility;

    public ILocalSetting LocalSetting { get; }
    public BingChatClient BingChatClient { get; set; }
}
