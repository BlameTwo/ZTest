using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.SinglePlayer;

namespace SonsOfTheForesrtSave_GUI.ViewModels; 
public partial class ConfigurrationViewModel :ObservableObject  {


    ConfigurationData ConfigurationData = new();

    [RelayCommand]
    async void Loaded() {

       this.Modeldata  = await  ConfigurationData.GetSave();
    }


    [ObservableProperty]
    ModelBase<PlayerStateDataModel> _modeldata;
}