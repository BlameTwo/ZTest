using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SonsOfTheForesrtSaveLib.Models;
using SonsOfTheForesrtSaveLib.SinglePlayer;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using ZTest.Tools;

namespace SonsOfTheForesrtSave_GUI.ViewModels;

public partial class StateSaveViewModel:ObservableObject
{
    PlayerStateData PlayerStateData = new();
    public StateSaveViewModel()
    {
        
    }

    [RelayCommand]
    async void Loaded()
    {
        var result =   await PlayerStateData.ReadSave();
        this.Version = result.Version;
        var data =  PlayerStateData.FormatData(result);
        this.ListData =  data.Data.ToObservableList();
    }

    [ObservableProperty]
    ObservableCollection<PlayerStateDataItem> _listData;

    

    [ObservableProperty]
    string _version;
}
