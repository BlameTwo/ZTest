using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;

namespace SonsOfTheForesrtSave_GUI.ViewModels;
public partial class PackageViewModel : ObservableObject {
    PlayerPackageData package = new();

    [RelayCommand]
    async void Loaded() {
        this.PackageModel = await package.ReadSave();
        ItemsData = (package.FormatData(PackageModel.Data));
    }

    [ObservableProperty]
    ModelBase<PlayerPackageModelData> _PackageModel;

    [ObservableProperty]
    PlayerInventoryData _ItemsData;
}
