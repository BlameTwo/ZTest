using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;

namespace SonsOfTheForesrtSave_GUI.ViewModels.ItemViewModel;

[INotifyPropertyChanged]
public partial class PackageBlockItemViewModel: ItemBlock {


    [RelayCommand]
    void AddData() {
        this.TotalCount++;
        OnPropertyChanged(nameof(TotalCount));
    }

    [RelayCommand]
    void RemoveData() {
        this.TotalCount--;
        OnPropertyChanged(nameof(TotalCount));
    }


}
