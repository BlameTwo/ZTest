using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Controls;

public partial class WrapUniformListBox
{
    internal WrapUniformListBox ListBoxParent
    {
        get { return (WrapUniformListBox)GetValue(ListBoxParentProperty); }
        set { SetValue(ListBoxParentProperty, value); }
    }

    internal static readonly DependencyProperty ListBoxParentProperty = DependencyProperty.RegisterAttached(
        nameof(ListBoxParent), typeof(WrapUniformListBox), typeof(WrapUniformListBox),
        new FrameworkPropertyMetadata(null!, FrameworkPropertyMetadataOptions.Inherits)
        );

    internal static WrapUniformListBox GetWrapListBox<T>(T item)
        where T : ContentControl
    {
        if (item.GetValue(ListBoxParentProperty) is WrapUniformListBox wrap)
            return wrap;

        return null;
    }


    public delegate void SelectionDelegate(WrapUniformListBox sender,WrapUniformListBoxItem selectitem);

    SelectionDelegate SelectionHandle = null;

    public event SelectionDelegate SelectionChanged
    {
        add { SelectionHandle += value; }
        remove { SelectionHandle -= value; }
    }
}
