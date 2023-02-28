using SimpleUI.Controls;
using SonsOfTheForesrtSave_GUI.ViewModels;

namespace SonsOfTheForesrtSave_GUI;

public partial class FirstSteamConfig : WindowBase
{
    public FirstSteamConfig(FirstSteamConfigViewModel vm)
    {
        InitializeComponent();
        vm.window = this;
        this.DataContext = vm;

    }
}
