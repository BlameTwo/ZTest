using Bing_BOT.Enum;
using Bing_BOT.Services.Contract;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Bing_BOT.Models.VM
{
    [INotifyPropertyChanged]
    public partial class BingBotModel
    {
        [ObservableProperty]
        string _Text;

        public BingBotModel(CancellationToken cancellationToken, string userMessage, ConversationSession session, IBingChatClient client,BingChatType type)
        {
            Client = client;
            CancellationToken = cancellationToken;
            UserMessage = userMessage;
            Session = session; 
            ChatType = type;
            if (Client != null)
            {
                Client.MessageChanged += Client_MessageChanged;
            }
        }

        private void Client_MessageChanged(object sender, BingChangedArgs args, bool isopen)
        {
            if (isopen)
            {
                this.Text += args.Text;
            }
            else
            {
                Client.MessageChanged -= Client_MessageChanged;
                return;
            }
            
        }

        /// <summary>
        /// 用来被UI进行Selecter筛选
        /// </summary>
        public BingChatType ChatType { get; set; }

        public string UserMessage { get; set; }


        [RelayCommand]
        async void Loaded()
        {
            WeakReferenceMessenger.Default.Send<BingChatEvent>(new BingChatEvent() { IsOpen = false });
            if (ChatType == BingChatType.Bing)
            {
                await Client.ChatAsync(new BingRequest(UserMessage) { Session = Session }, CancellationToken);
                Client.MessageChanged -= Client_MessageChanged;
                WeakReferenceMessenger.Default.Send<BingChatEvent>(new BingChatEvent() { IsOpen = true });
            }
            else
            {
                Text = UserMessage;
            }
        }

        /// <summary>
        /// 当前回复的条目
        /// </summary>
        public ConversationSession Session { get; set; }

        /// <summary>
        /// 当前回复的主机
        /// </summary>
        public IBingChatClient Client { get; set; }


        public CancellationToken CancellationToken { get; }
    }
}
