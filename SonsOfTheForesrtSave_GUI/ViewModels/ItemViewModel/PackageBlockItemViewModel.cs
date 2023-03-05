using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SonsOfTheForesrtSave_GUI.Models;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;

namespace SonsOfTheForesrtSave_GUI.ViewModels.ItemViewModel;

[INotifyPropertyChanged]
public partial class PackageBlockItemViewModel: ItemBlock {

    [RelayCommand]
    void AddData() {
        chang(true);
    }

    void chang(bool flage) {
        PackageManager.PackageData.Items.ForEach((val) => {
            if (val.ID == this.ItemID) {
                if (val.Type == "Default") {
                    if (flage)
                        this.TotalCount++;
                    else
                        this.TotalCount--;
                    OnPropertyChanged(nameof(TotalCount));
                }
                else {
                    WeakReferenceMessenger.Default.Send<ItemViewModelShowTipMessage>(new ItemViewModelShowTipMessage() {
                        Source = this,
                        Message = $"{val.Name} 不可添加数量"
                    });
                }
            }
        });
    }


    [RelayCommand]
    void RemoveData() {
        chang(false);
    }


}
