﻿using CommunityToolkit.Mvvm.ComponentModel;
using SimpleUI.Interface;
using ZTest.Tools.Interfaces;

namespace ChatGPT_GUI.ViewModels;
public partial class SettingViewModel:ObservableObject 
{
    public SettingViewModel(IThemeApply<App> themeApply,ILocalSetting localSetting)
    {
        ThemeApply = themeApply;
        LocalSetting = localSetting;
        Init();
    }

    private async void Init() {
        Keywrold = (await LocalSetting.ReadConfig("KeyWord")).ToString()!;
        Themestr = (await LocalSetting.ReadConfig("Theme")).ToString()!;
    }

    partial void OnThemestrChanged(string value) {
        if(value is string str) {
            switch (str) {
                case "浅色":
                    ThemeApply.Apply(SimpleUI.Themes.ThemeType.Light);
                    LocalSetting.SaveConfig("Theme", "浅色");
                    ThemeApply.IsEnable = false;
                    break;
                case "深色":
                    ThemeApply.Apply(SimpleUI.Themes.ThemeType.Dark);
                    LocalSetting.SaveConfig("Theme", "深色");
                    ThemeApply.IsEnable = false;
                    break;
                case "自动":
                    LocalSetting.SaveConfig("Theme", "自动");
                    ThemeApply.IsEnable = true;
                    break;
            }
            return;
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
    string _themestr;


}
