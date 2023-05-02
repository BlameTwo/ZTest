using SimpleUI.Interface;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Controls;

/// <summary>
/// 一个自动控制子项宽度的ListBox，配合一个自定义的Panel使用
/// </summary>
public partial class WrapUniformListBox:ItemsControl,IControl
{
    public WrapUniformListBox()
    {
        this.ListBoxParent = this;
    }



    public WrapUniformListBoxItem SelectionItem
    {
        get { return (WrapUniformListBoxItem)GetValue(SelectionItemProperty); }
        set { SetValue(SelectionItemProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SelectionItem.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectionItemProperty =
        DependencyProperty.Register("SelectionItem", typeof(WrapUniformListBoxItem), typeof(WrapUniformListBox), new PropertyMetadata(null));




    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(WrapUniformListBox), new PropertyMetadata(null));



    public void Selection(WrapUniformListBoxItem item)
    {
        this.SelectionItem = item;
    }
}


