using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace WSA_ADBShell;

public static class Tools
{
    public static ObservableCollection<T> ToObservable<T>(this List<T> list)
    {
        ObservableCollection<T> resultdata = new();
        foreach (T item in list)
        {
            resultdata.Add(item);
        }
        return resultdata;
    }


    /// <summary>
    /// 父类To子类，前提是子类中均不包含带参得构造函数
    /// </summary>
    /// <typeparam name="T1">父类</typeparam>
    /// <typeparam name="T2">子类</typeparam>
    /// <param name="data">数据</param>
    /// <returns>返回<typeparamref name="T2"/></returns>
    public static T2 ChildConvert<T1, T2>(this T1 data)
        where T1:class,new()
        where T2:T1,new()
    {
        T2 returnvalue = new T2();
        System.Reflection.PropertyInfo[] properties = data.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        foreach (var item in properties)
        {
            if (!(item.CanRead && item.CanWrite)) continue;
            item.SetValue(returnvalue, item.GetValue(data));
        }
        return returnvalue;
    }


}
