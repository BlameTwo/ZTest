using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System;
using ZTest.Tools.Interfaces;
using ChatGPT_GUI.Models;
using SimpleUI.Services;
using SimpleUI.Interface.AppInterfaces;

namespace ChatGPT_GUI.ViewModels;

public partial class ModelViewModel:ObservableRecipient
{
    public ModelViewModel(IShowDialogService showDialogService, IToastLitterMessage toastLitterMessage, ILocalSetting localSetting)
    {
        IsActive = true;
        this.ChatList = new ObservableCollection<ChatModel>();
        ShowDialogService = showDialogService;
        ToastLitterMessage = toastLitterMessage;
        LocalSetting = localSetting;
        RingVisibility = Visibility.Collapsed;
        StateMessage = "[等待A (用户)]";
        IsSend = true;
        ChatList = new();
    }

    [ObservableProperty]
    ModelType _ChatType;




    [RelayCommand]
    async void Loaded()
    {
        var str = (await LocalSetting.ReadConfig("KeyWord"));
        if (!string.IsNullOrWhiteSpace(str.ToString()))
        {
            OpenAIService = new OpenAIService(new OpenAiOptions() { ApiKey = str.ToString() });
            Check(this.ChatType);
        }
    }

    async void Check(ModelType type)
    {
        switch (type)
        {
            case ModelType.HuTao:
                await action(App.HuTaoText,
                    true, false);
                break;
            case ModelType.AiLi:
                await action(App.AiLiText,
                    true, false);
                break;
            case ModelType.Ying:
                await action(App.YingText,
                    true, false);
                break;
            case ModelType.ChatGpt:
                await action("你好,Gpt", true, false);
                break;
        }
    }


    [ObservableProperty]
    ObservableCollection<ChatModel> _ChatList;

    public IOpenAIService OpenAIService { get; set; }
    public IShowDialogService ShowDialogService { get; }
    public IToastLitterMessage ToastLitterMessage { get; }
    public ILocalSetting LocalSetting { get; }


    [RelayCommand(CanExecute = nameof(IsSendMethod))]
    async void Ask(string message)
    {
        await action(message, false, true);
    }

    

    bool IsSendMethod()
    {
        return IsSend;
    }


    [RelayCommand(CanExecute = nameof(IsSendMethod))]
    async void SetSystem(string message)
    {
        await action(message, true, true);
    }

    [ObservableProperty]
    Visibility _RingVisibility;

    [ObservableProperty]
    string _StateMessage;

    [ObservableProperty]
    private bool isSend;

    partial void OnIsSendChanged(bool value)
    {
        AskCommand.NotifyCanExecuteChanged();
        SetSystemCommand.NotifyCanExecuteChanged();
    }

    public async void refershtoken()
    {
        var str = (await LocalSetting.ReadConfig("KeyWord"));
        if (!string.IsNullOrWhiteSpace(str.ToString()))
            OpenAIService = new OpenAIService(new OpenAiOptions() { ApiKey = str.ToString() });
    }


    /// <summary>
    /// 执行问答
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="issystem">是否系统消息</param>
    /// <param name="isoutput">是否显示用户问题</param>
    /// <returns></returns>
    async Task action(string message, bool issystem, bool isoutput)
    {
        IsSend = false;
        RingVisibility = Visibility.Visible;
        StateMessage = "[等待B (AI)]";
        if (isoutput)
        {
            ChatList.Add(new ChatModel()
            {
                Type = ChatGPT_GUI.Models.ChatType.User,
                DateTime = DateTime.Now.AddSeconds(1),
                Message = message
                
            });
        }
        var messagelist = new List<OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage>();
        switch (this.ChatType)
        {
            case ModelType.HuTao:
                messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(App.HuTaoText));
                break;
            case ModelType.AiLi:
                messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(App.AiLiText));
                break;
            case ModelType.Ying:
                messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(App.YingText));
                break;
            case ModelType.ChatGpt:
                messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(message));
                    break;
        }
        foreach (var chat in ChatList)
        {
            switch (chat.Type)
            {
                case ChatGPT_GUI.Models.ChatType.User:
                    //用户的回答
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromUser(chat.Message));
                    break;
                case ChatGPT_GUI.Models.ChatType.AI:
                    //AI的回答
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromAssistant(chat.Message));
                    break;
                case ChatGPT_GUI.Models.ChatType.System:
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(chat.Message));
                    break;
            }
        }

        if (issystem)
            messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(message));
        else
            messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromUser(message));
        try
        {
            var result = await OpenAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
            {
                Messages = messagelist,
                Model = OpenAI.GPT3.ObjectModels.Models.ChatGpt3_5Turbo,
            });
            if (result.Successful)
            {
                string str = "";
                result.Choices.ForEach((val) => {
                    str += string.IsNullOrWhiteSpace(val.Message.Content) ? "" : val.Message.Content;
                });
                ChatList.Add(new ChatModel()
                {
                    Type = ChatGPT_GUI.Models.ChatType.AI,
                    DateTime = DateTime.Now.AddSeconds(1),
                    Message = str
                    ,ModelType = CheckModel()
                });
            }
            else
            {
                if (result.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                ToastLitterMessage.Show($"{result.Error.Message}");
            }
            RingVisibility = Visibility.Collapsed;
            StateMessage = "[等待A (用户)]";
            IsSend = true;
        }
        catch (Exception)
        {
            ToastLitterMessage.Show("代理或网络错误");
            RingVisibility = Visibility.Collapsed;
            StateMessage = "[等待A (用户)]";
            IsSend = true;
        }

    }


    ModelType CheckModel() => this.ChatType;


    public enum ModelType
    {
        HuTao, AiLi, Ying, ChatGpt
    }
}
