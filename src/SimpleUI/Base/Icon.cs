using SimpleUI.Interface;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Base;

public class Icon: Control,IIcon
{
    public string Glyph
    {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GlyphProperty =
        DependencyProperty.RegisterAttached("Glyph", typeof(string), typeof(Icon), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));



    public object IconImage
    {
        get { return (object)GetValue(IconImageProperty); }
        set { SetValue(IconImageProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IconImage.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconImageProperty =
        DependencyProperty.Register("IconImage", typeof(object), typeof(Icon), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));


}
