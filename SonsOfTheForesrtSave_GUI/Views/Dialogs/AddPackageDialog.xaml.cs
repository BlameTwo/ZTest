using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using SimpleUI.Controls;
using SimpleUI.Services;
using SonsOfTheForesrtSave_GUI.Models;
using SonsOfTheForesrtSave_GUI.ViewModels.DialogViewModel;
using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SonsOfTheForesrtSave_GUI.Views.Dialogs {
    /// <summary>
    /// AddPackageDialog.xaml 的交互逻辑
    /// </summary>
    [INotifyPropertyChanged]
    public partial class AddPackageDialog : DialogHost {
        public AddPackageDialog(AddPackageViewModel vm) {
            vm.dialogHost = this;
            this.DataContext = vm;
            InitializeComponent();
            
        }

        public override void ShowExDate<T>(T data) {
            base.ShowExDate(data);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
