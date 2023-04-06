using SimpleUI.Base.Enum;
using SimpleUI.Interface;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Controls {

    [DefaultProperty(name:nameof(Content))]
    public class TipCard:Control,IControl {


        public CornerRadius CornerRadius {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TipCard), new PropertyMetadata(default(CornerRadius)));



        public TipCardType TipType {
            get { return (TipCardType)GetValue(TipTypeProperty); }
            set { SetValue(TipTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TipType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipTypeProperty =
            DependencyProperty.Register("TipType", typeof(TipCardType), typeof(TipCard), new PropertyMetadata(TipCardType.Sucess));



        public object Content {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(TipCard), new PropertyMetadata(null));


    }
}
