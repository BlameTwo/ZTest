using System;
using System.Windows;
using System.Windows.Markup;

namespace SimpleUI.Styles;

[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public class SimpleStyle:ResourceDictionary
{
    /// <summary>
    /// 控件主题样式
    /// </summary>
    public StyleType Type
    {
        set
        {
            switch (value)
            {
                case StyleType.Default:
                    this.Source = new Uri($"{Utils.AppThemeData.StylePath}DefaultStyle.xaml", UriKind.Absolute);
                    break;
                case StyleType.Cyberpunk:
                    this.Source = new Uri($"{Utils.AppThemeData.StylePath}CyberpunkStyles/DefaultCyberpunkStyle.xaml", UriKind.Absolute);
                    break;
            }
        }
    }
}


/// <summary>
/// 样式，包含RDR，Cyberpunk，和默认样式
/// </summary>
public enum StyleType
{
    /// <summary>
    /// 暂时只包含默认
    /// </summary>
    Default,
    Cyberpunk
}
