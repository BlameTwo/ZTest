using SimpleUI.Interface;
using SimpleUI.Services;
using SimpleUI.Utils.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Controls;

public class NavigationViewHeader:Control,INavigationViewHeader
{
    CornerRadius IControl.CornerRadius { get; set; }
    string INavigationItemServices.Tag { get; set; }
    IconResources.FontIconEnum INavigationItemServices.Icon { get; set; }
    bool INavigationItemServices.IsActive { get; set; }
    Type INavigationItemServices.TypeUri { get; set; }



    public string  Header
    {
        get { return (string )GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string ), typeof(NavigationViewHeader), new PropertyMetadata(""));


}
