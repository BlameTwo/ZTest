
using SimpleUI.Themes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SimpleUI.Interface;

internal interface IThemeRegistry<T>:IDisposable
    where T :Application
{
    internal void Start();
    internal void Restart();

    internal List<Themes.Registry.RegistryArgs> Init();

    internal ThemeType GetSystemTheme();


    /// <summary>
    /// 监听的注册表更改事件
    /// </summary>
    internal event SimpleUI.Themes.Registry.ThemeRegistry<T>.RegistryChangedHandler RegistryChanged;
}
