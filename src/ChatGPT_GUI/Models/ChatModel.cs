using ChatGPT_GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
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

    public ViewModels.ModelViewModel.ModelType ModelType { get; set; }

    public ChatType Type { get; set; }

    private Stream MS { get; set; } = null;
    
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
        VITSService.SaveWAV(false, MS);
    }

    [RelayCommand(CanExecute =nameof(IsConverttMethod))]
    async void Player()
    {
        IsVist = true;
        IsConvert = false;
        if(MS == null)
        {
            var result =await VITSService.GetStream(Message);
            if(result == null)
            {
                return;
                IsVist = false;
                IsConvert = true;
            } 
            MS = result;
        }
        MS.Position = 0;
        SoundPlayer player = new SoundPlayer(MS);
        player.Play();
        IsVist = false;
        IsConvert = true;
    }
}

public enum ChatType {
    User,AI,System
}
