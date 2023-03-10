using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleUI.Interface;
using System.Windows.Controls;
using ZTest.Tools.Interfaces;

namespace ChatGPT_GUI.ViewModels;
public partial class SettingViewModel:ObservableObject 
{
    public SettingViewModel(IThemeApply<App> themeApply,ILocalSetting localSetting)
    {
        ThemeApply = themeApply;
        LocalSetting = localSetting;
    }

    partial void OnThemestrChanged(object value) {
        switch ((value as ComboBoxItem).Content.ToString()) {
            case "浅色":
                ThemeApply.Apply(SimpleUI.Themes.ThemeType.Light);
                LocalSetting.SaveConfig("Theme", "Light");
                break;
            case "深色":
                ThemeApply.Apply(SimpleUI.Themes.ThemeType.Dark);
                LocalSetting.SaveConfig("Theme", "Dark");
                break;
        }
    }

    partial void OnKeywroldChanged(string value) {
        LocalSetting.SaveConfig("KeyWord", value);
    }

    public IThemeApply<App> ThemeApply { get; }
    public ILocalSetting LocalSetting { get; }

    [ObservableProperty]
    string _keywrold;

    [ObservableProperty]
    object _themestr;


}
