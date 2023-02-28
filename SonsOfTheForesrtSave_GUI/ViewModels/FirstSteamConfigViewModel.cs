using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using SimpleUI.Interface;
using System.Globalization;
using System.IO;
using System.Windows;

namespace SonsOfTheForesrtSave_GUI.ViewModels;

public partial class FirstSteamConfigViewModel:ObservableObject
{

    public FirstSteamConfigViewModel(IWindowManager windowManager)
    {
        WindowManager = windowManager;
    }

    [RelayCommand]
    void SelectGamePath()
    {
        var dlg = new CommonOpenFileDialog();
        dlg.IsFolderPicker = true;
        if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
        {
            this.Path = dlg.FileName!;
            WindowManager.Show(nameof(MainWindow));
        }

    }

    public Window window { get; set; }

    [ObservableProperty]
    string _path;

    public IWindowManager WindowManager { get; }
    public IMessenger Messenger { get; }

    [RelayCommand(CanExecute = nameof(CheckPath))]
    void GoGame()
    {
        var win = App.GetService<MainWindow>();
        win.Show();
        window.Close();
    }


    bool CheckPath()
    {
        if (string.IsNullOrWhiteSpace(Path)) return true;
        if(File.Exists(System.IO.Path.Combine(Path, "PlayerStateSaveData.json")))
        {
            return true;
        }
        return false;
    }
    
}
