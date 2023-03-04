using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface.AppInterfaces;
using SonsOfTheForesrtSave_GUI.ViewModels.ItemViewModel;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ZTest.Tools;

namespace SonsOfTheForesrtSave_GUI.ViewModels;
public partial class PackageViewModel : ObservableObject {
    public PackageViewModel(IToastLitterMessage toastLitterMessage)
    {
        ToastLitterMessage = toastLitterMessage;
    }

    PlayerPackageData package = new();

    [RelayCommand]
    async void Loaded() {
        this.PackageModel = await package.ReadSave();
        ItemsData = (package.FormatData(PackageModel.Data));
        BlockItem = new();
        foreach (var item in ItemsData.ItemInstanceManager.ItemBlocks) {
            this.BlockItem.Add(item.ChildConvert<ItemBlock, PackageBlockItemViewModel>());
        }
    }

    [RelayCommand]
    void CopyJson() {
        Clipboard.SetDataObject(this.PackageModel.Data.Data);
        ToastLitterMessage.Show("已复制存档Json");
    }

    [RelayCommand]
    void SaveData() {
        List<ItemBlock> PackBlock = new();
        foreach (var item in this.BlockItem) {
            ItemBlock block = item;
            PackBlock.Add(block);
        }
        package.SaveData(this.PackageModel, new PlayerInventoryData() {
            EquippedItems = this.ItemsData.EquippedItems,
            QuickSelect = this.ItemsData.QuickSelect,
            Version = this.PackageModel.Version,
            ItemInstanceManager = new ItemInstanceManager() {
                Version = this.ItemsData.ItemInstanceManager.Version,
                ItemBlocks = PackBlock
            }
        });
        ToastLitterMessage.Show("已保存背包存档");

    }

    [ObservableProperty]
    ModelBase<PlayerPackageModelData> _PackageModel;

    [ObservableProperty]
    PlayerInventoryData _ItemsData;

    [ObservableProperty]
    ObservableCollection<PackageBlockItemViewModel> _BlockItem;

    public IToastLitterMessage ToastLitterMessage { get; }
}
