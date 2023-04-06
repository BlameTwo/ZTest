using CommunityToolkit.Mvvm.Input;
using SimpleUI.Controls;
using SonsOfTheForesrtSave_GUI.ViewModels;
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

namespace SonsOfTheForesrtSave_GUI;

public partial class MainWindow : WindowBase
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        this.DataContext= vm;
        this.Closed += MainWindow_Closed;
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if(this.DataContext is MainViewModel vm)
        {
            vm.AppNavigationViewService.Init(this.framebase,true);
            vm.ToastLitterMessage.ShowOwner = this.grid;
            vm.ToastLitterMessage.ShowTime = TimeSpan.FromSeconds(3);
        }
    }


    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        System.Environment.Exit(0);
    }
}
