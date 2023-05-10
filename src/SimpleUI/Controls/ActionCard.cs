using SimpleUI.Interface;
using System.Windows;
using System.Windows.Controls;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls
{
    [TemplatePart(Name ="PRPA_ActionBth",Type =typeof(System.Windows.Controls.Button))]
    public class ActionCard:Control,IControl
    {

        static ActionCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActionCard), new FrameworkPropertyMetadata(typeof(ActionCard)));
        }

        public FontIconEnum Icon
        {
            get { return (FontIconEnum)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(ActionCard), new FrameworkPropertyMetadata(FontIconEnum.Accept, FrameworkPropertyMetadataOptions.Inherits));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                typeof(CornerRadius),
                typeof(ActionCard),
                new FrameworkPropertyMetadata(new CornerRadius(0)));


        public object Description
        {
            get { return (object)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(object), typeof(ActionCard), new FrameworkPropertyMetadata(null));




        public string SubTitle
        {
            get { return (string)GetValue(SubTitleProperty); }
            set { SetValue(SubTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubTitleProperty =
            DependencyProperty.Register("SubTitle", typeof(string), typeof(ActionCard), new PropertyMetadata(null));


        public string  Header
        {
            get { return (string )GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(ActionCard), new PropertyMetadata(null));


    }
}
