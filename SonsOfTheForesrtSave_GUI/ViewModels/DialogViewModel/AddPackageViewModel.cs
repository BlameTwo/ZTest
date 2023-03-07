using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SimpleUI.Interface;
using SonsOfTheForesrtSave_GUI.Models;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models;
using SonsOfTheForesrtSaveLib.SinglePlayer;
using System.Collections.Generic;
using System.Linq;

namespace SonsOfTheForesrtSave_GUI.ViewModels.DialogViewModel;
public partial class AddPackageViewModel:ObservableObject {

    [ObservableProperty]
    List<PackageDataItem> _SelectItems;

    [ObservableProperty]
    PackageDataItem _selectitem;

    private PlayerPackageData player = new();

    public AddPackageViewModel()
    {
        SelectItems = new();
    }


    public IDialogHost dialogHost { get; set; }

    partial void OnSelectitemChanged(PackageDataItem value) {
        if (value.Type == "Single") {
            Maxnumber = 1;
            Number = 1;
        }
        else {
            Number = 1;
            Maxnumber = 100;
        }
        Type = value.Type;

    }

    [RelayCommand]
    void ADD() {
        WeakReferenceMessenger.Default.Send<DialogAddPackage>(new DialogAddPackage() {
            Number = this.Number,
            Data = Selectitem,
            MaxNumber = Maxnumber,
            Type = this.Type
        });
        WeakReferenceMessenger.Default.Send<ItemViewModelShowTipMessage>(new() {
             Message= "添加成功",Source = this
        });
        dialogHost.Close();
    }

    [RelayCommand]
    void Refersh() => LoadedCommand.Execute(null);

    private string Type { get; set; }

    [ObservableProperty]
    int _maxnumber;

    [ObservableProperty]
    int _number;


    [RelayCommand]
    async void Loaded() {
        PackageManager.RefershPackage();
        var packdata = PackageManager.PackageData;
        var savedata = await player.ReadSave();
        var blackdata = player.FormatData(savedata.Data);
        if (blackdata.ItemInstanceManager.ItemBlocks == null) this.SelectItems = packdata.Items;
        IEnumerable<int> hashpackdata = packdata.Items.Select(o=>o.ID);
        IEnumerable<int> hashsavedata = blackdata.ItemInstanceManager.ItemBlocks.Select(o=>o.ItemID);
        var list2 = hashpackdata.Except(hashsavedata);
        foreach (var item in list2) {
            var i = packdata.Items.Where((val) => {
                if(val.ID == item) {
                    return true;
                }
                return false;
            });
            foreach (var item3 in i)
                SelectItems.Add(item3);
        }
    }


}
