using Microsoft.Xaml.Behaviors.Layout;
using SimpleUI.Interface;
using SimpleUI.Services;
using System.Windows.Documents;
using System.Windows;
using SimpleUI.Helper;

namespace SimpleUI.Utils;

public class ShowDialogService : IShowDialogService
{
    public bool IsShow
    {
        get
        {
            //这里去判断一下是否有弹窗正在运行
            FrameworkElement element;
            AdornerDecorator decorator;
            element = WindowHelper.GetActiveWindow();
            decorator = WindowHelper.GetChild<AdornerDecorator>(element);
            AdornerLayer layer = decorator.AdornerLayer;
            var _container = new AdornerContainer(layer);
            return _container.Child == null ? true : false;
        }
    }

    public bool CloseDialog()
    {
        FrameworkElement element;
        AdornerDecorator decorator;
        element = WindowHelper.GetActiveWindow();
        decorator = WindowHelper.GetChild<AdornerDecorator>(element);
        AdornerLayer layer = decorator.AdornerLayer;
        var _container = new AdornerContainer(layer);
        if(_container.Child is IDialogHost host)
        {
            host.Close();
            return true;
        }
        return false;
    }

    public bool Show<T,T1>(T arg,T1 data) 
        where T : IDialogHost
    {
        if (IsShow)
        {
            //显示出对话框
            arg.Show();
            arg.ShowExDate<T1>(data);
            return true;
        }
        else
        {
            return false;
        }
    }
}
