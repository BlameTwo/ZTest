using SimpleUI.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleUI.Interface
{
    public interface IThemeApply<T>
        where T :Application
    {
        /// <summary>
        /// Theme的依赖对象
        /// </summary>
        T App { get; set; }


        /// <summary>
        /// 是否启动跟随系统
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 设置主题颜色
        /// </summary>
        /// <param name="type">主题</param>
        /// <param name="color">主题颜色</param>
        /// <returns>返回一个bool对象判断是否更改成功</returns>
        bool Apply(ThemeType type,System.Drawing.Color? color=null);
        public event SimpleUI.Themes.ThemeApply<T>.ThemeChangedHandle OnThemeChanged;
    }
}
