using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace SimpleUI.Themes
{
    [Localizability(LocalizationCategory.Ignore)]
    [Ambient]
    [UsableDuringInitialization(true)]
    public class Theme: ResourceDictionary
    {
        public Theme()
        {
            var result = this.Type;
        }

        private ThemeType _type;

        public ThemeType Type
        {
            get => _type;
            set
            {
                switch (value)
                {
                    //浅色主题
                    case ThemeType.Light:
                        this.Source = new Uri($"{Utils.AppThemeData.ThemePath}Light.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.Dark:
                        //深色主题
                        this.Source = new Uri($"{Utils.AppThemeData.ThemePath}Dark.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.Cyberpunk:
                        //赛博朋克颜色
                        this.Source = new Uri($"{Utils.AppThemeData.ThemePath}Cyberpunk.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.Default:
                        break;
                }
                _type= value;
            }
        }
    }


    public enum ThemeType
    {
        Light, Dark,Default, Cyberpunk
    }
}