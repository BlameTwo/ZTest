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
        
}
