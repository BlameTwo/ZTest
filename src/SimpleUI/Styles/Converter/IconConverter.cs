using System;
using System.Globalization;
using System.Windows.Data;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Styles.Converter
{

    /// <summary>
    /// 通过反射获得unicode字符
    /// </summary>
    public class IconConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IconConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is FontIconEnum item)
            {
                FontIconResource tmp_Class = new();
                Type type = tmp_Class.GetType();
                System.Reflection.PropertyInfo[] properties = type.GetProperties();
                string name = item.ToString();
                foreach (var item2 in properties)
                {
                    var result = item2.Name;
                    if(result == name)
                    {
                        var rr =  item2.GetValue(tmp_Class)!.ToString();
                        return rr;   
                    }
                }
            }
            return "\uE783";
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
