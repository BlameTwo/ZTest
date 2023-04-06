using SimpleUI.Base;
using SimpleUI.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls
{
    public class NavigationViewItem:Control,INavigationItemServices
    {

        public new string Tag
        {
            get { return (string)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register("Tag", typeof(string), typeof(NavigationViewItem), new PropertyMetadata(""));



        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NavigationViewItem), new PropertyMetadata(default(CornerRadius)));




        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(default(bool)));



        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(NavigationViewItem), new PropertyMetadata(default(object)));


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // 获得属性传递之上的NavigationView。
            NavigationView view = NavigationView.GetNavigationView(this)!;
            view.Select(this);
            base.OnMouseLeftButtonDown(e);
        }




        public Type TypeUri
        {
            get { return (Type)GetValue(TypeUriProperty); }
            set { SetValue(TypeUriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TypeUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeUriProperty =
            DependencyProperty.Register("TypeUri", typeof(Type), typeof(NavigationViewItem), new PropertyMetadata(default(Type)));




        public FontIconEnum Icon
        {
            get { return (FontIconEnum)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontIconEnum), typeof(NavigationViewItem), new PropertyMetadata(FontIconEnum.Like));


    }
}
