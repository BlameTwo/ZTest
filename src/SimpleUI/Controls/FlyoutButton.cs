using SimpleUI.Interface;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls;

public class FlyoutButton:ButtonBase,IControl
{
    public FlyoutButton()
    {
        IsOpen = false;
    }



    public object DropContent
    {
        get { return (object)GetValue(DropContentProperty); }
        set { SetValue(DropContentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DropContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DropContentProperty =
        DependencyProperty.Register("DropContent", typeof(object), typeof(FlyoutButton), new PropertyMetadata(null));





    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlyoutButton), new FrameworkPropertyMetadata(null));

    public FontIconEnum Icon
    {
        get { return (FontIconEnum)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(FlyoutButton), new PropertyMetadata(null));





    public bool IsOpen
    {
        get { return (bool)GetValue(IsOpenProperty); }
        set { SetValue(IsOpenProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register("IsOpen", typeof(bool), typeof(FlyoutButton), new PropertyMetadata(false));








    public PlacementMode Placement
    {
        get { return (PlacementMode)GetValue(PlacementProperty); }
        set { SetValue(PlacementProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(FlyoutButton), new PropertyMetadata(null));




    protected override void OnClick()
    {
        this.IsOpen = !this.IsOpen;
        base.OnClick();
    }
}
