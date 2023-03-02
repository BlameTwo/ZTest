using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using SimpleUI.Interface;
using System.IO;
using SonsOfTheForesrtSaveLib;
using System.Windows;
using SimpleUI.Interface.AppInterfaces;

namespace SonsOfTheForesrtSave_GUI.ViewModels;

public partial class FirstSteamConfigViewModel:ObservableObject
{

    public FirstSteamConfigViewModel(IWindowManager windowManager,IToastLitterMessage toastLitterMessage)
    {
        WindowManager = windowManager;
        ToastLitterMessage = toastLitterMessage;
    }

    [RelayCommand]
    void SelectGamePath()
    {
        var dlg = new CommonOpenFileDialog();
        dlg.IsFolderPicker = true;
        if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
        {
            this.Path = dlg.FileName!;
            SaveConfig.DirPath = dlg.FileName!;
        }
    }

    public Window window { get; set; }


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoGameCommand))]
    string _path;

    public IWindowManager WindowManager { get; }
    public IToastLitterMessage ToastLitterMessage { get; }
    public IMessenger Messenger { get; }

    [RelayCommand(CanExecute = nameof(CheckPath))]
    void GoGame()
    {
        if (CheckPath())
        {
            var win = App.GetService<MainWindow>();
            win.Show();
            window.Close();
        }
        else
        {
            ToastLitterMessage.Show("错误的地址，请重新选择");
        }

    }


    bool CheckPath()
    {
        if (string.IsNullOrWhiteSpace(Path)) return false;
        if(File.Exists(System.IO.Path.Combine(Path, "PlayerStateSaveData.json")))
        {
            return true;
        }
        return false;
    }
    
}
