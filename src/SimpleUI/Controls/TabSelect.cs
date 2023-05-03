using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using SimpleUI.Interface;

namespace SimpleUI.Controls;


public partial class TabSelect : ItemsControl, ITabSelect,IControl
{
    public TabSelect()
    {
        TabSelectParent = this;
    }

    



    public Style ItemStyle
    {
        get { return (Style)GetValue(ItemStyleProperty); }
        set { SetValue(ItemStyleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ItemStyle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemStyleProperty =
        DependencyProperty.Register("ItemStyle", typeof(Style), typeof(TabSelect), new PropertyMetadata(null));




    public DataTemplate ItemTemplate
    {
        get { return (DataTemplate)GetValue(ItemTemplateProperty); }
        set { SetValue(ItemTemplateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(TabSelect), new PropertyMetadata(null));



    public ITabSelectItem SelectItem
    {
        get { return (ITabSelectItem)GetValue(SelectItemProperty); }
        set { SetValue(SelectItemProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SelectItem.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectItemProperty =
        DependencyProperty.Register("SelectItem", typeof(ITabSelectItem), typeof(TabSelect), new PropertyMetadata(default(ITabSelectItem)));

    public delegate void TabSelectSelectChangedHandler(TabSelect sender, TabSelectArgs args);

    private TabSelectSelectChangedHandler SelectHeader = null;


    [Category("Behavior")]
    public event TabSelectSelectChangedHandler SelectChanged
    {
        add
        {
            SelectHeader += value;
        }
        remove
        {
            SelectHeader -= value;
        }
    }




    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TabSelect), new PropertyMetadata(new CornerRadius(0)));



    

    internal void Select(ITabSelectItem item)
    {
        if(this.SelectItem != null)
        {
            this.SelectItem.IsSelect = false;
            this.SelectItem = null;
            this.SelectItem = item;
            item.IsSelect = true;
            if (SelectHeader != null)
                SelectHeader.Invoke(this, new TabSelectArgs(new List<TabSelectItem>() { (TabSelectItem)item},item.Tag));
        }
        else
        {
            this.SelectItem = item;
            item.IsSelect = true;
            if (SelectHeader != null)
                SelectHeader.Invoke(this, new TabSelectArgs(new List<TabSelectItem>() { (TabSelectItem)item }, item.Tag));
        }
        
    }



}

public class TabSelectArgs: RoutedEventArgs
{
    public List<TabSelectItem> AddItems { get; }

    public TabSelectArgs(List<TabSelectItem> addItems, object tag = null)
    {
        AddItems = addItems;
        Tag = tag;
    }

    public object Tag { get; }

}
