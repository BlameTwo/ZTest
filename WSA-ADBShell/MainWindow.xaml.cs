using CommunityToolkit.Mvvm.Input;
using SimpleUI.Controls;
using System;
using WSA_ADBShell.ViewModels;

namespace WSA_ADBShell;

public partial class MainWindow : WindowBase
{
    public MainWindow(MainViewModel vm)
    {
        this.DataContext = vm;
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }


    private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if(this.DataContext is MainViewModel vm)
        {
            vm.ToastLitterMessage.ShowOwner = this.grid;
            vm.ToastLitterMessage.ShowTime = TimeSpan.FromSeconds(2);
            vm.AppNavigationViewService.Init(this._frame, true);
        }
    }
}
