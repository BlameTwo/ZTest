using SimpleUI.Interface;
using SimpleUI.Themes.Registry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace SimpleUI.Themes;

public partial class ThemeApply<T> : IThemeApply<T>
    where T : Application
{
    Application _app;
    public T App
    {
        get => (T)_app;
        set => _app = value;
    }


    internal IThemeRegistry<T> Registry { get; }

    private bool _Isenable;
    public bool IsEnable
    {
        get => _Isenable;
        set
        {
            if (value)
                Registry.Start();
            else
                Registry.Dispose();
            _Isenable = value;
        }
    }

    public ThemeApply()
    {
        Registry = new ThemeRegistry<T>(this);
        Registry.RegistryChanged += Registry_RegistryChanged;
        Registry.Start();
    }

    ThemeType _AppTheme, _SystemTheme;
    bool transp;
    System.Drawing.Color _AccentColor;
    private void Registry_RegistryChanged(ThemeRegistry<T> sender, RegistryArgs args)
    {
        List<ThemeSettings> _options = new();
        switch (args.ValueName)
        {
            case "EnableTransparency":
                _options.Add(ThemeSettings.Transparency);
                transp = System.Convert.ToBoolean((int)args.Value);
                break;
            case "AppsUseLightTheme":
                _options.Add(ThemeSettings.AppMode);
                _AppTheme = (int)args.Value == 1 ? ThemeType.Light : ThemeType.Dark;
                break;
            case "SystemUsesLightTheme":
                _options.Add(ThemeSettings.WindowsMode);
                _SystemTheme = (int)args.Value == 1 ? ThemeType.Light : ThemeType.Dark;
                break;
            case "AccentColor":
                _options.Add(ThemeSettings.AccentColor);
                _AccentColor = ColorTranslator.FromWin32((int)args.Value);
                break;
        }

        if(ThemeChangedEventHandle!=null)
        //触发事件传输参数，请遵循_options里的变量进行修改。
            ThemeChangedEventHandle.Invoke(this, new ThemeChangedArgs(transp, _AccentColor
            , _AppTheme, _SystemTheme, _options));
    }

    public delegate void ThemeChangedHandle(ThemeApply<T> sender, ThemeChangedArgs args);
    public ThemeChangedHandle ThemeChangedEventHandle;

    public event ThemeApply<T>.ThemeChangedHandle OnThemeChanged
    {
        add
        {
            ThemeChangedEventHandle += value;
        }
        remove
        {
            ThemeChangedEventHandle -= value;
        }
    }


    public virtual bool Apply(ThemeType type,System.Drawing.Color? color=null)
    {
        try
        {
            if(type == ThemeType.Default)
            {
                SetDefaultTheme();
                return true;
            }
            else
            {
                var theme = App.Resources.MergedDictionaries[0] as Theme;
                theme.Type = type;
            }
            if (color != null)
            {
                foreach (var item in App.Resources.MergedDictionaries)
                {
                    //确定资源是否存在
                    if (item.Source.AbsoluteUri.StartsWith(SimpleUI.Utils.AppThemeData.ThemePath))
                    {
                        //如果等于Theme元数据
                        _AccentColor = (System.Drawing.Color)color;
                        System.Windows.Media.Color color2 = System.Windows.Media.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
                        //替换对于Default。Accent.Color的颜色
                        App.Resources["Default.Accent.Color"] = color2;
                        break;
                    }
                };
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    void SetDefaultTheme()
    {
        var result = Registry.Init();
        foreach (var item in result)
        {
            //应用颜色
            if(item.ValueName == "AppsUseLightTheme")
            {
                int value = (int)item.Value;
                if(value == 1)
                {
                    Apply(ThemeType.Light);
                    _AppTheme = ThemeType.Light;
                    continue;
                }
                else if(value == 0)
                {
                    Apply(ThemeType.Dark);
                    _AppTheme = ThemeType.Dark;
                    continue;
                }
            }

            //替换颜色
            if(item.ValueName == "AccentColor")
            {
                _AccentColor = ColorTranslator.FromWin32((int)item.Value);
                System.Windows.Media.Color color2 = System.Windows.Media.Color.FromArgb(_AccentColor.A, _AccentColor.R, _AccentColor.G, _AccentColor.B);
                //替换对于Default。Accent.Color的颜色
                App.Resources["Default.Accent.Color"] = color2;
            }
        }
    }
}

public class ThemeChangedArgs:EventArgs
{
    public bool TransparencyEnabled { get; private set; }

    public ThemeChangedArgs(
        bool transparencyEnabled,
        System.Drawing.Color accentColor, 
        ThemeType appMode, 
        ThemeType windowsMode, 
        List<ThemeSettings> settingsChanged)
    {
        TransparencyEnabled = transparencyEnabled;
        AccentColor = accentColor;
        AppMode = appMode;
        WindowsMode = windowsMode;
        SettingsChanged = settingsChanged;
    }

    public System.Drawing.Color AccentColor { get; private set; }

    public ThemeType AppMode { get; private set; }

    public ThemeType WindowsMode { get; private set; }

    public List<ThemeSettings> SettingsChanged { get; private set; } = new List<ThemeSettings>();
}

public enum ThemeSettings
{
    AppMode,

    WindowsMode,

    AccentColor,

    //AccentForeColor,

    Transparency
}
