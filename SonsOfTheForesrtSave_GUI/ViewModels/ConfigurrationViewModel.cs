using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SonsOfTheForesrtSave_GUI.ViewModels; 
public partial class ConfigurrationViewModel :ObservableObject  {


    ConfigurationData ConfigurationData = new();

    [RelayCommand]
    async void Loaded() {
       this.Modeldata  = await  ConfigurationData.GetSave();
       var result =  ConfigurationData.FormatSave(Modeldata);
       Itemlist = ZTest.Tools.Observable.ToObservableList(result.Items);
    }

    [ObservableProperty]
    ObservableCollection<ConfigurrationResultDataItem> _itemlist;

    [ObservableProperty]
    ModelBase<PlayerStateDataModel> _modeldata;
}