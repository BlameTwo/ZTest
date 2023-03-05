using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SonsOfTheForesrtSave_GUI.Models;
using SonsOfTheForesrtSave_GUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleUI.Utils.Controls.IconResources;

namespace SonsOfTheForesrtSave_GUI.ViewModels.ItemViewModel
{
    /// <summary>
    /// 快捷菜单VM
    /// </summary>
    public partial class PackageAddMenuViewModel:ObservableObject, IPackAddMenu {


        [ObservableProperty]
        string _title;

        [ObservableProperty]
        int number;

        [ObservableProperty]
        int id;

        [ObservableProperty]
        FontIconEnum icon;

        public AddMenuType Type { get; set; }



        [RelayCommand]
        public void Excute() {
            WeakReferenceMessenger.Default.Send<AddMenuMessage>(new AddMenuMessage() {
                DataVM = this,
                By = "无注意事项",
                Message = $"VM交互：{this.Title}"
            });
        }
    }

    public enum AddMenuType {
        Default,Single
    }
}
