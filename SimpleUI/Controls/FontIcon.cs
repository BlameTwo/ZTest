using SimpleUI.Base;
using System.Windows;
using System.Windows.Media;

namespace SimpleUI.Controls
{
    /// <summary>
    /// 实现于Segoe Fluent Icons 这里
    /// </summary>
    public class FontIcon:Icon
    {
        static FontIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIcon),
                new System.Windows.FrameworkPropertyMetadata(typeof(FontIcon)));

            FontFamilyProperty.OverrideMetadata(typeof(FontIcon),
                new System.Windows.FrameworkPropertyMetadata(new FontFamily("Segoe Fluent Icons")));
        }
    }
}
