﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SimpleUI.Interface.AppInterfaces;
using SimpleUI.Services;
using SonsOfTheForesrtSave_GUI.Models;
using SonsOfTheForesrtSave_GUI.ViewModels.ItemViewModel;
using SonsOfTheForesrtSave_GUI.Views.Dialogs;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using ZTest.Tools;

namespace SonsOfTheForesrtSave_GUI.ViewModels;
public partial class PackageViewModel : ObservableRecipient, IRecipient<AddMenuMessage>,IRecipient<ItemViewModelShowTipMessage>,IRecipient<DialogAddPackage> {
    public PackageViewModel(IToastLitterMessage toastLitterMessage,IShowDialogService showDialogService)
    {
        ToastLitterMessage = toastLitterMessage;
        ShowDialogService = showDialogService;
        this.IsActive = true;
        Init();
    }

    private void Init() {
        BlockItem = new();
        this.AddMenuItems = new ObservableCollection<PackageAddMenuViewModel>() {
            new PackageAddMenuViewModel(){ Icon = SimpleUI.Utils.Controls.IconResources.FontIconEnum.Add, Id = 364, Number = 100,Type = AddMenuType.Default, Title = "铅块子弹+100"},
            new PackageAddMenuViewModel(){ Icon = SimpleUI.Utils.Controls.IconResources.FontIconEnum.Add, Id = 362, Number = 100,Type = AddMenuType.Default, Title = "9毫米+100"},
            new PackageAddMenuViewModel(){ Icon = SimpleUI.Utils.Controls.IconResources.FontIconEnum.Add, Id = 437, Number = 6, Type = AddMenuType.Default,Title = "药品补满"},
            new PackageAddMenuViewModel(){ Icon = SimpleUI.Utils.Controls.IconResources.FontIconEnum.Add, Id = 451, Number = 10,Type = AddMenuType.Default, Title = "莴苣+10"},
            new PackageAddMenuViewModel(){ Icon = SimpleUI.Utils.Controls.IconResources.FontIconEnum.Add, Id = 527, Number = 6,Type = AddMenuType.Default, Title = "电池+6"},
            new PackageAddMenuViewModel(){ Icon = SimpleUI.Utils.Controls.IconResources.FontIconEnum.Add, Id = 358, Number = 1,Type = AddMenuType.Single, Title = "获得霰弹枪"},
        };
    }

    PlayerPackageData package = new();

    [RelayCommand]
    async void Loaded() {
        this.PackageModel = await package.ReadSave();
        ItemsData = (package.FormatData(PackageModel.Data));
        BlockItem.Clear();
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

    [RelayCommand]
    void RefershBig() {
        LoadedCommand.Execute(null);
    }

    [RelayCommand]
    void AddPackageItem() {
        ShowDialogService.Show<AddPackageDialog,string>(App.GetService<AddPackageDialog>(),"增加物品");
    }

    public void Receive(AddMenuMessage message) {
        bool flage=false;
        int flageindex = -1;
        for (int i = 0; i < BlockItem.Count; i++) {
            if (BlockItem[i].ItemID == message.DataVM.Id) {
                flage = true;
                flageindex = i;
            }
        }
        if (flage) {
            BlockItem[flageindex].TotalCount += message.DataVM.Number;
        }
        else {
            var model = new PackageBlockItemViewModel() {
                ItemID = message.DataVM.Id,
                TotalCount = message.DataVM.Number,
                UniqueItems=null
            };
            if (message.DataVM.Type == AddMenuType.Single) {
                model.UniqueItems = new();
            }
            this.BlockItem.Add(model);
        }
        ToastLitterMessage.Show("操作执行完毕");
    }


    public void Receive(ItemViewModelShowTipMessage message) {
        Debug.WriteLine($"{message.GetType()} 消息错误");
        ToastLitterMessage.Show(message.Message);
    }

    public void Receive(DialogAddPackage message) {
        var model = new PackageBlockItemViewModel() {
             ItemID = message.Data.ID,
             TotalCount= message.Number,
             UniqueItems=null
        };
        if(message.Data.Type == "Single") {
            model.UniqueItems = new();
        }
        this.BlockItem.Add(model);
    }

    [ObservableProperty]
    ModelBase<PlayerPackageModelData> _PackageModel;

    [ObservableProperty]
    PlayerInventoryData _ItemsData;

    [ObservableProperty]
    ObservableCollection<PackageBlockItemViewModel> _BlockItem;

    [ObservableProperty]
    ObservableCollection<PackageAddMenuViewModel> _AddMenuItems;
    public IToastLitterMessage ToastLitterMessage { get; }
    public IShowDialogService ShowDialogService { get; }
}
