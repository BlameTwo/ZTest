using SimpleUI.Interface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using System.Linq;

namespace SimpleUI.Themes.Registry;

// 监听方法来自于
//https://github.com/Willy-Kimura/WindowsThemeListener

internal class ThemeRegistry<T>: IThemeRegistry<T>
    where T :Application
{
    internal ThemeRegistry(IThemeApply<T> themedata)
    {
        Themedata = themedata;
        _themeitem = new List<Tuple<string, string>>();
        //是否透明
        _themeitem.Add(new Tuple<string, string>(_themepath, "EnableTransparency"));
        //App的颜色（深色、浅色
        _themeitem.Add(new Tuple<string, string>(_themepath, "AppsUseLightTheme"));
        //系统的颜色（深色、浅色
        _themeitem.Add(new Tuple<string, string>(_themepath, "SystemUsesLightTheme"));
        //系统主题颜色
        _themeitem.Add(new Tuple<string, string>(_colorpath, "AccentColor"));
        //进行初始化旧的参数
        _themeitemvalues = _themeitem.ToDictionary(key => key, key => Microsoft.Win32.Registry.GetValue(key.Item1, key.Item2, null));
        
    }


    private void RegistryChanging(object? state)
    {
        foreach (Tuple<string,string> reg in _themeitem)
        {
            int newValue = (int)Microsoft.Win32.Registry.GetValue(reg.Item1, reg.Item2, null);
            int reg2 = (int)_themeitemvalues[reg];
            if (reg2 != newValue)
            {
                if(RegistryChangedEventHandler != null)
                {
                    RegistryChangedEventHandler.Invoke(this, new RegistryArgs(reg.Item1, reg.Item2, newValue));
                    _themeitemvalues[reg] = newValue;
                }
            }
        }
    }


    internal string _themepath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    internal string _colorpath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM";


    internal delegate void RegistryChangedHandler(ThemeRegistry<T> sender, RegistryArgs args);
    internal RegistryChangedHandler RegistryChangedEventHandler;


    event ThemeRegistry<T>.RegistryChangedHandler IThemeRegistry<T>.RegistryChanged
    {
        add { RegistryChangedEventHandler += value; }
        remove { RegistryChangedEventHandler -= value; }
    }

    List<Tuple<string, string>> _themeitem;

    private readonly Dictionary<Tuple<string, string>, object> _themeitemvalues;

    /// <summary>
    /// 计时器
    /// </summary>
    private Timer timer;
    /// <summary>
    /// 时间间隔
    /// </summary>
    private int _timeLine = 1000;
    private bool disposedValue;

    private IThemeApply<T> Themedata { get; }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                timer.Dispose();
            }

            disposedValue = true;
        }
    }

    // 
    // ~ThemeRegistry()
    // {
    //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
    //     Dispose(disposing: false);
    // }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    void RerershTimer()
    {
        timer = new Timer(RegistryChanging,null,0, _timeLine);
    }

    void IThemeRegistry<T>.Start()
    {
        this.RerershTimer();
    }

    void IThemeRegistry<T>.Restart()
    {
        this.RerershTimer();
    }

    List<RegistryArgs> IThemeRegistry<T>.Init()
    {
        List<RegistryArgs> args = new List<RegistryArgs>();
        foreach (var item in _themeitemvalues)
        {
            args.Add(new RegistryArgs(item.Key.Item1,item.Key.Item2,item.Value));
        }
        return args;
    }

    ThemeType IThemeRegistry<T>.GetSystemTheme()
    {
        var value = (int)Microsoft.Win32.Registry.GetValue(_themepath, "SystemUsesLightTheme",null);
        if(value == null)
            throw new ArgumentNullException("注册表让吃了");
        if (value == 1)
            return ThemeType.Light;
        else
            return ThemeType.Dark;
    }
}

public class RegistryArgs:EventArgs
{


    /// <summary>
    /// 注册表Key
    /// </summary>
    public string KeyName { get; }

    public RegistryArgs(string keyName, string valueName, object value)
    {
        KeyName = keyName;
        ValueName = valueName;
        Value = value;
    }

    /// <summary>
    /// 从 <see cref="KeyName"/> 这里获取的名称或值.
    /// </summary>
    public string ValueName { get; }

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; }
}