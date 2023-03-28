using ChatGPT_GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp;
using System;
using System.IO;
using System.Media;
using System.Security.Permissions;
using System.Windows.Media;
using ZTest.Tools;
namespace ChatGPT_GUI.Models;

[INotifyPropertyChanged]
public partial class ChatModel {

    public ChatModel()
    {
        this.VITSService = App.GetSerivces<IVITSService>();
        IsConvert = true;
    }

    public IVITSService VITSService { get; }

    public DateTime DateTime { get; set; }  

    public string Message { get; set; }

    public ChatType Type { get; set; }

    private string MS { get; set; } = null;
    
    [ObservableProperty]
    bool _IsConvert;

    bool IsConverttMethod() => IsConvert;

    partial void OnIsConvertChanged(bool value)
    {
        PlayerCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute =nameof(IsConverttMethod))]
    async void Player()
    {
        IsConvert = false;
        if(MS == null)
        {
            MS= (await VITSService.GetBase64(Message)); 
        }
        SoundPlayer player = new SoundPlayer(MS.Convert());

        player.Play();
        IsConvert = true;
    }
}

public enum ChatType {
    User,AI,System
}
