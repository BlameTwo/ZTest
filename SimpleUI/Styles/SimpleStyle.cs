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
            }
        }
    }
}


public enum StyleType
{
    /// <summary>
    /// 暂时只包含默认
    /// </summary>
    Default
}
