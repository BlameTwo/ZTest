using SimpleUI.Interface;
using SimpleUI.Services;
using SimpleUI.Utils.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Controls;

public class NavigationViewLine : Control,INavigationViewLine
{
    CornerRadius IControl.CornerRadius { get; set; }
    string INavigationItemServices.Tag { get; set; }
    IconResources.FontIconEnum INavigationItemServices.Icon { get; set; }
    bool INavigationItemServices.IsActive { get; set; }
    Type INavigationItemServices.TypeUri { get; set; }




    public double StrokeThickness
    {
        get { return (double)GetValue(StrokeThicknessProperty); }
        set { SetValue(StrokeThicknessProperty, value); }
    }

    // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register("StrokeThickness", typeof(double), typeof(NavigationViewLine), new PropertyMetadata(3.0));






}
