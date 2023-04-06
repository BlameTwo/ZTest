using System.Collections.ObjectModel;

namespace ZTest.Tools
{
    public static class Observable
    {
        public static ObservableCollection<T> ToObservableList<T>(this List<T> list)
        {
            var value = new ObservableCollection<T>();
            list.ForEach((val)=>value.Add(val));
            return value;
        }

    }
}