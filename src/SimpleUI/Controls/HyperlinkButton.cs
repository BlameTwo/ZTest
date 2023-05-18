using Microsoft.Xaml.Behaviors.Core;
using SimpleUI.Base;
using System.Diagnostics;
using System.Windows;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls;

public class HyperlinkButton:ButtonBase
{
    protected override void OnClick()
    {       
        base.OnClick();
        Process.Start("explorer.exe", Uri);
    }


    public string Uri
    {
        get { return (string)GetValue(UriProperty); }
        set { SetValue(UriProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Uri.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UriProperty =
        DependencyProperty.Register("Uri", typeof(string), typeof(HyperlinkButton), new PropertyMetadata(null));


    public FontIconEnum Icon
    {
        get { return (FontIconEnum)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(HyperlinkButton), new PropertyMetadata(null));

}
