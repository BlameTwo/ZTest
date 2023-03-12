using ChatGPT_GUI.ViewModels;
using OpenAI.GPT3.Interfaces;
using SimpleUI.Controls;
using SimpleUI.Utils;
using SimpleUI.Utils.Backdrop;
using System;

namespace ChatGPT_GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase {
        public MainWindow(MainViewModel vm) {
            vm.ToastLitterMessage.ShowOwner = grid;
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.DataContext = vm;
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            if(this.DataContext is MainViewModel vm) {
                vm.ToastLitterMessage.ShowOwner = this.grid;
                vm.ToastLitterMessage.ShowTime = TimeSpan.FromSeconds(3);
            }
        }
    }
}
