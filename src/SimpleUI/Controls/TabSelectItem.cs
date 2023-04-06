using System.Windows.Controls;
using System.Windows;
using SimpleUI.Interface;
using static SimpleUI.Utils.Controls.IconResources;
using System.Windows.Input;

namespace SimpleUI.Controls;

public class TabSelectItem:Control,ITabSelectItem,IControl
{
    public bool IsSelect
    {
        get { return (bool)GetValue(IsSelectProperty); }
        set { SetValue(IsSelectProperty, value); }
    }

    public static readonly DependencyProperty IsSelectProperty =
        DependencyProperty.Register("IsSelect", typeof(bool), typeof(TabSelectItem), new PropertyMetadata(false));


    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        var view = TabSelect.GetTabSelectParent(this)!;
        view.Select(this);
        base.OnMouseLeftButtonDown(e);
    }

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TabSelectItem), new PropertyMetadata(new CornerRadius(0)));


    public FontIconEnum Icon
    {
        get { return (FontIconEnum)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(TabSelectItem), new PropertyMetadata(null));


    public string Header
    {
        get { return (string)GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(TabSelectItem), new PropertyMetadata(""));



}
