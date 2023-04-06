using SonsOfTheForesrtSave_GUI.ViewModels;
using System.Windows.Controls;

namespace SonsOfTheForesrtSave_GUI.Views;
/// <summary>
/// GameSettingPage.xaml 的交互逻辑
/// </summary>
public partial class GameSettingPage : Page {
    public GameSettingPage(GameSettingViewModel vm) {
        InitializeComponent();
        this.DataContext = vm;
    }
}
