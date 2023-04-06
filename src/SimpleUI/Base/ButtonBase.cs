using System.Windows;

namespace SimpleUI.Base
{

    public class ButtonBase : System.Windows.Controls.Primitives.ButtonBase
    {



        /// <summary>
        /// 按钮基类的圆角依赖属性
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
                typeof(ButtonBase),
                new PropertyMetadata(new CornerRadius(0)));




    }
}

