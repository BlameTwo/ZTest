using SimpleUI.Interface;
using System.Windows;
using System.Windows.Controls.Primitives;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls
{
    /// <summary>
    /// 按钮，继承于Base.ButtonBase
    /// </summary>
    public class Button : SimpleUI.Base.ButtonBase, IControl
    {
        static Button()
        {
            //这里是覆盖原样式和类，基本上扩展的UI不用写，这些基础的还是要协商的。
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button),
                new System.Windows.FrameworkPropertyMetadata(typeof(Button)));
        }



        public Visibility ContentVisiblity {
            get { return (Visibility)GetValue(ContentVisiblityProperty); }
            set { SetValue(ContentVisiblityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentVisiblity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentVisiblityProperty =
            DependencyProperty.Register("ContentVisiblity", typeof(Visibility), typeof(SimpleUI.Controls.Button), new PropertyMetadata(Visibility.Visible));

        public FontIconEnum Icon
        {
            get { return (FontIconEnum)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(Button), new PropertyMetadata(null));
    }
}