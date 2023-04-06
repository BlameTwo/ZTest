using SimpleUI.Interface;
using SimpleUI.Utils.Controls;
using System.Windows;
using System.Windows.Controls;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls
{
    public class Card : Control, IControl
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", 
                typeof(CornerRadius), 
                typeof(Card), 
                new FrameworkPropertyMetadata(new CornerRadius(0)));




        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(Card), new PropertyMetadata(null));





        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", 
                typeof(string), 
                typeof(Card), 
                new PropertyMetadata(""));




        public FontIconEnum Icon
        {
            get { return (FontIconEnum)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(Card), new PropertyMetadata(null));





        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register("HeaderHorizontalAlignment", 
                typeof(HorizontalAlignment), typeof(Card), 
                new PropertyMetadata(null));

    }
}