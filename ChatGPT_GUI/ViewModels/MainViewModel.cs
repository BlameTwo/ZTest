using ChatGPT_GUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenAI.GPT3.Interfaces;
using System;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleUI.Services;
using ZTest.Tools.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3;
using SimpleUI.Interface.AppInterfaces;

namespace ChatGPT_GUI.ViewModels;

public partial class MainViewModel: ObservableRecipient {
    public MainViewModel(IShowDialogService showDialogService, IOpenAIService openAIService, ILocalSetting localSetting,IToastLitterMessage toastLitterMessage) {
        IsActive = true;
        this.ChatList = new ObservableCollection<ChatModel>();
        ShowDialogService = showDialogService;
        OpenAIService = openAIService;
        LocalSetting = localSetting;
        ToastLitterMessage = toastLitterMessage;
        OpenAIService = App.GetOpenAIService();
    }


    [RelayCommand]
    async void Loaded() {
        var str = (await LocalSetting.ReadConfig("KeyWord")).ToString();
        OpenAIService = new OpenAIService(new OpenAiOptions() { ApiKey = str });
        ToastLitterMessage.Show("你好，欢迎使用猫娘模拟器");
    }

    [ObservableProperty]
    ObservableCollection<ChatModel> _ChatList;

    public IOpenAIService OpenAIService { get; set; }
    public IShowDialogService ShowDialogService { get; }
    public ILocalSetting LocalSetting { get; }
    public IToastLitterMessage ToastLitterMessage { get; }

    [RelayCommand]
    async void Ask(string message) {
        await action(message, false);
    }

    [RelayCommand]
    void ShowSetting() {
        ShowDialogService.Show(App.GetSerivces<SettingDialog>(), "空的");
    }

    [RelayCommand]
    async void SetSystem(string message) {
        await action(message, true);
    }

    async Task action(string message,bool issystem) {
        ChatList.Add(new ChatModel() {
            Type = ChatType.User,
            DateTime = DateTime.Now.AddSeconds(1),
            Message = message
        });
        var messagelist = new List<OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage>();
        
        foreach (var chat in ChatList) {
            switch (chat.Type) {
                case ChatType.User:
                    //用户的回答
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromUser(chat.Message));
                    break;
                case ChatType.AI:
                    //AI的回答
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromAssistance(chat.Message));
                    break;
                case ChatType.System:
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(chat.Message));
                    break;
            }
        }
        if (issystem)
            messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(message));
        else
            messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromUser(message));
        var result = await OpenAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest() {
            Messages = messagelist,
            Model = OpenAI.GPT3.ObjectModels.Models.ChatGpt3_5Turbo
        });
        if (result.Successful) {
            string str = "";
            result.Choices.ForEach((val) => {
                str += string.IsNullOrWhiteSpace(val.Message.Content) ? "" : val.Message.Content;
            });
            ChatList.Add(new ChatModel() {
                Type = ChatType.AI,
                DateTime = DateTime.Now.AddSeconds(1),
                Message = str
            });
        }
        else {
            if (result.Error == null) {
                throw new Exception("Unknown Error");
            }
            ToastLitterMessage.Show($"{result.Error.Message}");
        }
    }
}
