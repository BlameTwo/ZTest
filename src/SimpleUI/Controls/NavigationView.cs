using SimpleUI.Controls.Navigation;
using SimpleUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleUI.Controls;


[TemplatePart(Name = "PART_MenuButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_MenuItems_TreeView",Type =typeof(ItemsControl))]
[TemplatePart(Name = "PART_FooterMenuItems_TreeView",Type =typeof(ItemsControl))]
[TemplatePart(Name = "MenuPanel",Type =typeof(Border))]
[TemplatePart(Name = "DockGrid",Type =typeof(Grid))]
[TemplateVisualState(GroupName= "PanelState",Name ="Open")]
[TemplateVisualState(GroupName ="PanelState",Name ="Close")]
public partial class NavigationView:ContentControl,INavigationViewService
{
    //定义一个委托，此委托有两个参数，一个是NavigationView，一个是选中事件的参数
    public delegate void NavigationSelectedEventHandler(NavigationView sender, NavigationViewSelectedChangedArgs args);

    private NavigationSelectedEventHandler SelectedHandler = null;

    /// <summary>
    /// 覆盖样式，静态方法
    /// </summary>
    static NavigationView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationView), new FrameworkPropertyMetadata(typeof(NavigationView)));
    }

    /// <summary>
    /// 构造函数，把当前父控件的NavigationParent设置为子级，这样通过子控件NavigationParent
    /// 可以获得父控件，这样就构成了可访问结构，重要在属性传递。
    /// </summary>
    public NavigationView()
    {
        //指定父级为子级，在此之下 的所有NavigationViewItem控件可访问
        NavigationParent = this;
        SelectionItem = null;
        this.MenuItems = new List<INavigationItemServices>();
        this.FooterMenuItems = new List<INavigationItemServices>(); ;
    }



    public ImageSource MenuBackImage
    {
        get { return (ImageSource)GetValue(MenuBackImageProperty); }
        set { SetValue(MenuBackImageProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MenuBackImage.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MenuBackImageProperty =
        DependencyProperty.Register("MenuBackImage", typeof(ImageSource), typeof(NavigationView), new PropertyMetadata(null));




    public object TitleRightContent
    {
        get { return (object)GetValue(TitleRightContentProperty); }
        set { SetValue(TitleRightContentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TitleRightContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleRightContentProperty =
        DependencyProperty.Register("TitleRightContent", typeof(object), typeof(NavigationView), new PropertyMetadata(null));




    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius",
            typeof(CornerRadius),
            typeof(NavigationView),
            new PropertyMetadata(default(CornerRadius)));



    public Style ItemContainerStyle
    {
        get { return (Style)GetValue(ItemContainerStyleProperty); }
        set { SetValue(ItemContainerStyleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ItemContainerStyle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemContainerStyleProperty =
        DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(NavigationView), new PropertyMetadata(null));



    public DataTemplate ItemTemplate
    {
        get { return (DataTemplate)GetValue(ItemTemplateProperty); }
        set { SetValue(ItemTemplateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(NavigationView), new PropertyMetadata(null));




    public Brush ContentBackground
    {
        get { return (Brush)GetValue(ContentBackgroundProperty); }
        set { SetValue(ContentBackgroundProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ContentBackgroundProperty =
        DependencyProperty.Register("ContentBackground", typeof(Brush), typeof(NavigationView), new PropertyMetadata(default(Brush)));



    internal void Select(INavigationItemServices item)
    {
        if (item is not NavigationViewItem) return;
        if(this.SelectionItem == null)
        {//如果当前选择项目为空
            this.SelectionItem = item;
            item.IsActive = true;
            if(SelectedHandler != null)
                SelectedHandler.Invoke(this, new NavigationViewSelectedChangedArgs(this.SelectionItem, this));
        }
        else if(this.SelectionItem != item)
        {//当前选择为新的
            this.SelectionItem.IsActive = false;
            this.SelectionItem = item;
            item.IsActive = true;
            if (SelectedHandler != null)
                SelectedHandler.Invoke(this, new NavigationViewSelectedChangedArgs(this.SelectionItem, this));
        }
    }




    /// <summary>
    /// 标记行为
    /// </summary>
    [Category("Behavior")]
    public event NavigationSelectedEventHandler SelectionChanged 
    {
        add
        {
            SelectedHandler += value;
        }
        remove
        {
            SelectedHandler -= value;
        }
    }



    public INavigationItemServices SelectionItem
    {
        get { return (INavigationItemServices)GetValue(SelectionItemProperty); }
        set { SetValue(SelectionItemProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SelectionItem.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectionItemProperty =
        DependencyProperty.Register("SelectionItem", typeof(INavigationItemServices), typeof(NavigationView),new PropertyMetadata(null));

    public override void OnApplyTemplate()
    {
        
        base.OnApplyTemplate();
        var tree = GetTemplateChild("PART_MenuItems_TreeView") as ItemsControl;
        var menubutton = GetTemplateChild("PART_MenuButton") as System.Windows.Controls.Button;
        Grid border = GetTemplateChild("DockGrid") as Grid;
        if(border != null)
        {
            border.PreviewMouseDown += Border_MouseLeftButtonDown;
        }
        if (menubutton != null)
        {
            menubutton.Click += Menubutton_Click;
        }
        ChangeVisualState(false);
    }

    private void Menubutton_Click(object sender, RoutedEventArgs e)
    {
        if (MenuState == MenuOpenState.Open)
            MenuState = MenuOpenState.Close;
        else if (MenuState == MenuOpenState.Close)
            MenuState = MenuOpenState.Open;
        this.ChangeVisualState(false);
    }

    private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (MenuState == MenuOpenState.Open)
            MenuState = MenuOpenState.Close;
    }

    private void ChangeVisualState(bool value)
    {
        switch (MenuState)
        {
            case MenuOpenState.Open:
                VisualStateManager.GoToState(this, "Open", value);
                break;
            case MenuOpenState.Close:
                VisualStateManager.GoToState(this, "Close", value);
                break;
        }
    }

    public List<INavigationItemServices> MenuItems
    {
        get { return (List<INavigationItemServices>)GetValue(MenuItemsProperty); }
        set { SetValue(MenuItemsProperty, value); }
    }

    public static readonly DependencyProperty MenuItemsProperty =
        DependencyProperty.Register("MenuItems", typeof(List<INavigationItemServices>), typeof(NavigationView), new PropertyMetadata(default(List<INavigationItemServices>)));


    public List<INavigationItemServices> FooterMenuItems
    {
        get { return (List<INavigationItemServices>)GetValue(FooterMenuItemsProperty); }
        set { SetValue(FooterMenuItemsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for FooterMenuItems.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FooterMenuItemsProperty =
        DependencyProperty.Register("FooterMenuItems", typeof(List<INavigationItemServices>), typeof(NavigationView), new PropertyMetadata(default(List<INavigationItemServices>)));








    public object NavigationHeader
    {
        get { return (object)GetValue(NavigationHeaderProperty); }
        set { SetValue(NavigationHeaderProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NavigationHeader.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NavigationHeaderProperty =
        DependencyProperty.Register("NavigationHeader", typeof(object), typeof(NavigationView), new FrameworkPropertyMetadata(null));






    public double MenuOpenLength
    {
        get { return (double)GetValue(MenuOpenLengthProperty); }
        set { SetValue(MenuOpenLengthProperty, value); }
    }

    public static readonly DependencyProperty MenuOpenLengthProperty =
        DependencyProperty.Register("MenuOpenLength", typeof(double), typeof(NavigationView), new PropertyMetadata(0.0));



    public MenuOpenState MenuState
    {
        get { return (MenuOpenState)GetValue(MenuStateProperty); }
        set 
        { 
            SetValue(MenuStateProperty, value);
            ChangeVisualState(false);
        }
    }

    public static readonly DependencyProperty MenuStateProperty =
        DependencyProperty.Register("MenuState", typeof(MenuOpenState), typeof(NavigationView), new FrameworkPropertyMetadata(null));



    public string PanelTitle
    {
        get { return (string)GetValue(PanelTitleProperty); }
        set { SetValue(PanelTitleProperty, value); }
    }
    //直接夹带私货
    public static readonly DependencyProperty PanelTitleProperty =
        DependencyProperty.Register("PanelTitle", typeof(string), typeof(NavigationView), new PropertyMetadata("SimpleUI欢迎你"));


}


public class NavigationViewSelectedChangedArgs: RoutedEventArgs
{
    public NavigationViewItem SelectedItem { get; }

    public NavigationViewSelectedChangedArgs(
        INavigationItemServices selectedItem, 
        object owner, 
        string tag = null)
    {
        SelectedItem = (NavigationViewItem?)selectedItem;
        Owner = owner;
        Tag = tag;
    }

    public object Owner { get; }

    public string Tag { get; }

}
