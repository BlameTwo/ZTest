using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Base
{

    /// <summary>
    /// 基础控件的基类
    /// </summary>
    public class ControlBase : Control
    {
        /// <summary>
        /// 控件基类的圆角依赖属性
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                typeof(CornerRadius),
                typeof(ControlBase),
                new PropertyMetadata(new CornerRadius(0)));

    }
}

