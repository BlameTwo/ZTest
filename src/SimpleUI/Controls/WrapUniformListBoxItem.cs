using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Controls;

public class WrapUniformListBoxItem:ContentControl
{


    /// <summary>
    /// 是否选中
    /// </summary>
    public bool IsSelected
    {
        get { return (bool)GetValue(IsSelectedProperty); }
        set { SetValue(IsSelectedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register("IsSelected", typeof(bool), typeof(WrapUniformListBoxItem), new PropertyMetadata(null));




}
