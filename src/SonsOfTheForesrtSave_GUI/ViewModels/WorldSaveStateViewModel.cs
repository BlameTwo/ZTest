using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib;
using System.Windows.Media.Imaging;
using System;
using System.Windows;
using SimpleUI.Interface.AppInterfaces;
using System.Windows.Documents;
using System.Collections.Generic;

namespace SonsOfTheForesrtSave_GUI.ViewModels;
public partial class WorldSaveStateViewModel:ObservableObject {

    SonsOfTheForesrtSaveLib.SinglePlayer.GameWorldStateData _gameword = new();
    public WorldSaveStateViewModel(IToastLitterMessage toastLitterMessage)
    {
        ToastLitterMessage = toastLitterMessage;
        this.WordSaveType = new List<string>() {
            "Normal",
            "Peaceful",
            "Hard",
            "Custom"
        };
    }

    [ObservableProperty]
    ModelBase<GameWorldStateResultModel> _gameworldstateresult;

    [ObservableProperty]
    GameWorldStateResultData _savedata;

    [ObservableProperty]
    BitmapImage _bitmap;

    public IToastLitterMessage ToastLitterMessage { get; }

    [RelayCommand]
    void CopyClipBoard() {
        Clipboard.SetDataObject(Gameworldstateresult.Data.Data);
        ToastLitterMessage.Show("已写入粘贴板");
    }

    [RelayCommand]
    void SaveData() {
        _gameword.WriteSave(this.Gameworldstateresult,this.Savedata);
        ToastLitterMessage.Show("已保存存档");
    }

    [ObservableProperty]
    List<string> _WordSaveType;

    [RelayCommand]
    async void Loaded() {
        if (SonsOfTheForesrtSaveLib.SaveConfig.DirPath != null)
            Bitmap = new(new Uri(SonsOfTheForesrtSaveLib.SaveConfig.IconPath));
        Gameworldstateresult = await  _gameword.ReadSave();
        this.Savedata = _gameword.FormatData(this.Gameworldstateresult.Data);
    }
}
