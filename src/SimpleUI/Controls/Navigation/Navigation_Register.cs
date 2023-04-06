using SimpleUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleUI.Controls;

public partial class NavigationView
{
    /// <summary>
	/// 导航属性传递之父类属性，控件库内访问，不对外开放
	/// </summary>
	internal INavigationViewService NavigationParent
    {
        get { return (INavigationViewService)GetValue(NavigationParentProperty); }
        set { SetValue(NavigationParentProperty, value); }
    }


    internal static readonly DependencyProperty NavigationParentProperty = DependencyProperty.RegisterAttached(
            nameof(NavigationParent), typeof(INavigationViewService), typeof(NavigationView),
            new FrameworkPropertyMetadata(((INavigationViewService)null!), FrameworkPropertyMetadataOptions.Inherits));


    /// <summary>
    /// 获得T类型之上 的父类控件，通过NavigationParent的属性传递特性。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    internal static NavigationView? GetNavigationView<T>(T item)
        where T : DependencyObject, INavigationItemServices
    {
        /*
		 类型：静态方法
		 返回类型：NavigationView
		 在上述中定义了一个NavigationParent的依赖属性，此依赖属性后在后续属性中被设置成为了Inherits，
		 也就是可以被后续的子类控件继承的依赖属性。
		 本方法定义了一个T的泛型约束，约束他为有依赖属性和继承于导航项接口的类型。
		 */
        if (item.GetValue(NavigationParentProperty) is NavigationView nav)
        {
            /*
			 这里判断是否为NavigationView,因为NavigationView继承了接口，并且从元素树上找，一般子项都会在NavigationView之下。

			因为依赖属性属性传递的方式为继承传递，也就是说在NavigationView之下的所有模板子项控件，都可以找到NavigationParent附加属性
			并将其接口转换为NavigationView的实例返回到子项的请求中。
			 */
            return nav;
        }
        return null;
    }
}
