using System.Windows;
namespace SimpleUI.Controls;

public partial class TabSelect
{
    internal ITabSelect TabSelectParent
    {
        get { return (ITabSelect)GetValue(TabSelectProperty); }
        set { SetValue(TabSelectProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TabSelect.  This enables animation, styling, binding, etc...
    internal static readonly DependencyProperty TabSelectProperty = DependencyProperty.RegisterAttached(
        nameof(TabSelectParent), typeof(ITabSelect), typeof(TabSelect),
        new FrameworkPropertyMetadata(((ITabSelect)null!), FrameworkPropertyMetadataOptions.Inherits));


    internal static TabSelect GetTabSelectParent<T>(T t)
        where T: DependencyObject, ITabSelectItem
    {
        var result = t.GetValue(TabSelectProperty);
        if (t.GetValue(TabSelectProperty) is TabSelect control)
        {
            return control;
        }
        return null;
    }


}
