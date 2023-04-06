using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface.AppInterfaces;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;

namespace SonsOfTheForesrtSave_GUI.ViewModels;
public partial class GameSettingViewModel:ObservableObject {
    public GameSettingViewModel(IToastLitterMessage toastLitterMessage)
    {
        ToastLitterMessage = toastLitterMessage;
    }

    [RelayCommand]
    async void Loaded()
    {
        ModelBase =  await SettingData.GetSave();
        Model = SettingData.FormatSave(ModelBase);
    }

    [ObservableProperty]
    ModelBase<GameSetupDataModel> _ModelBase;

    [ObservableProperty]
    SettingResultData _Model;

    [RelayCommand]
    void SaveData()
    {
        SettingData.WriteSave(ModelBase,Model);
        ToastLitterMessage.Show("操作完毕");
    }

    SettingData SettingData { get; set; } = new();
    public IToastLitterMessage ToastLitterMessage { get; }
}
