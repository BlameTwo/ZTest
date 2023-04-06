
using Bing_BOT.Enum;
using Bing_BOT.Models;
using Bing_BOT.Models.VM;
using Bing_BOT.Services;
using Bing_BOT.Services.Contract;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ZTest.Tools.Interfaces;

namespace ChatGPT_GUI.ViewModels;

public partial class BingPageViewModel : ObservableRecipient, IRecipient<BingChatEvent>
{

    public BingPageViewModel(ILocalSetting localSetting)
    {
        IsActive = true;
        LocalSetting = localSetting;
        IsSend = true;

    }

    private object? bingcookie;


    [ObservableProperty]
    ObservableCollection<Bing_BOT.Models.VM.BingBotModel> _ListChat=new();

    private CancellationTokenSource? chatCts=new();

    private ICreateBingBot CreateBot { get; set; }
    private IBingChatClient Client { get; set; }

    private BingConversation Conversation { get; set; } = null;
    BingChatOption _option { get; set; }

    int NowId = 0;

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
        CreateBot = new CreateBingBot();
        _option = new BingChatOption( Bing_BOT.Enum.CharStyle.Creative,bingcookie.ToString()!,1000);
        Client = new BingChatClient();
        Client.Init(_option);
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
        _option = new BingChatOption(style, bingcookie.ToString()!, 1000);
        Conversation = await CreateBot.CreateBingConversation(bingcookie.ToString(), chatCts.Token);
    }

    ConversationSession _oldrequest;

    [RelayCommand(CanExecute =nameof(IsSend))]
    async void Ask(string text)
    {
        IsSend = false;
        ListChat.Add(new(chatCts.Token,text,null,null, BingChatType.User));
        await Task.Delay(500);
        ListChat.Add(new Bing_BOT.Models.VM.BingBotModel(chatCts.Token,text,new(NowId, Conversation.conversationId, Conversation.clientId, Conversation.conversationSignature),this.Client, BingChatType.Bing));
        NowId++;
    }

    void IRecipient<BingChatEvent>.Receive(BingChatEvent message)
    {
        IsSend = message.IsOpen;
    }


    [ObservableProperty]
    bool _IsSend;

    bool CheckSend() => IsSend;

    partial void OnIsSendChanged(bool value)
    {
        AskCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    Visibility _ContentVisibility;

    [ObservableProperty]
    Visibility _CardVisibility;

    public ILocalSetting LocalSetting { get; }
    public BingChatClient BingChatClient { get; set; }
}
