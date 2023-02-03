
using SimpleUI.Interface;
using System.Windows;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls;

public class Expander: System.Windows.Controls.Expander, IControl
{
    public Expander()
    {
    }

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Expander), new FrameworkPropertyMetadata(default(CornerRadius)));


    public string Header
    {
        get { return (string)GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(Expander), new PropertyMetadata(null));




    public object RightHeaderContent
    {
        get { return (object)GetValue(RightHeaderContentProperty); }
        set { SetValue(RightHeaderContentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for RightHeaderContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RightHeaderContentProperty =
        DependencyProperty.Register("RightHeaderContent", typeof(object), typeof(Expander), new FrameworkPropertyMetadata(null));



    public FontIconEnum Icon
    {
        get { return (FontIconEnum)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(Expander), new PropertyMetadata(null));
}
