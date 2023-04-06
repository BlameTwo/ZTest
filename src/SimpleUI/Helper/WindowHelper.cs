using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace SimpleUI.Helper;

public static class WindowHelper
{
    /// <summary>
    /// 获得主窗口
    /// </summary>
    /// <returns></returns>
    public static Window GetActiveWindow()
    {
        return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)!;
    }

    /// <summary>
    /// 获得一个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="d"></param>
    /// <returns></returns>
    public static T GetChild<T>(DependencyObject d) where T : DependencyObject
    {
        if (d == null) return default;
        if (d is T t) return t;

        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
        {
            var child = VisualTreeHelper.GetChild(d, i);

            var result = GetChild<T>(child);
            if (result != null) return result;
        }

        return default;
    }


    /// <summary>
    /// 获得一个元素，通过Name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="d"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T GetElment<T>(DependencyObject d, string name) where T : DependencyObject
    {
        if (d == null) return default;
        if (d is T t) return t;

        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
        {
            var child = VisualTreeHelper.GetChild(d, i);

            var result = GetChild<T>(child);
            if (result != null)
            {
                if (result is ContentControl content)
                {
                    if (content.Name == name)
                        return result;
                }
            }

        }

        return default;
    }
}
