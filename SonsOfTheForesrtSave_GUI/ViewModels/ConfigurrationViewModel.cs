using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SonsOfTheForesrtSave_GUI.ViewModels.ItemViewModel;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZTest.Tools;

namespace SonsOfTheForesrtSave_GUI.ViewModels;
public partial class ConfigurrationViewModel : ObservableObject {


    ConfigurationData ConfigurationData = new();

    [RelayCommand]
    async void Loaded() {
        Itemlist = new();
        this.Modeldata = await ConfigurationData.GetSave();
        var result = ConfigurationData.FormatSave(Modeldata);

        foreach (var item in result.Items) {
            Itemlist.Add(item.ChildConvert<ConfigurrationResultDataItem, ConfigurationItemViewModel>());
        }
    }


    [RelayCommand]
    void SaveData() {
        List<ConfigurrationResultDataItem> list = new();
        foreach (var item in Itemlist) {
            list.Add(item);
        }

        ConfigurationData.WriteSave(this.Modeldata, new ConfigurrationResultData() {
            Items = list
        });
    }



    [ObservableProperty]
    ObservableCollection<ConfigurationItemViewModel> _itemlist;


    [ObservableProperty]
    ModelBase<PlayerStateDataModel> _modeldata;

}