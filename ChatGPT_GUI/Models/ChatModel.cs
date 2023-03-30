using ChatGPT_GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Media;
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

    [ObservableProperty]
    bool _IsVist;

    bool IsConverttMethod() => IsConvert;

    partial void OnIsConvertChanged(bool value)
    {
        PlayerCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    void SaveWMV()
    {
        if (MS == null) return;
        VITSService.SaveWAV(false, MS.Convert());
    }

    [RelayCommand(CanExecute =nameof(IsConverttMethod))]
    async void Player()
    {
        IsVist = true;
        IsConvert = false;
        if(MS == null)
        {
            var result =await VITSService.GetBase64(Message);
            if(result == "NONE" || string.IsNullOrWhiteSpace(result))
            {
                return;
            } 
            MS = result;
        }
        SoundPlayer player = new SoundPlayer(MS.Convert());

        player.Play();
        IsVist = false;
        IsConvert = true;
    }
}

public enum ChatType {
    User,AI,System
}
