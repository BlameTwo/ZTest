using SonsOfTheForesrtSaveLib;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SonsOfTheForesrtSave_GUI.Converter;
public class PackageIDConvert : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if(value is int val){
            return GetString(val);
        }
        return value;
    }

    string GetString(int id) {
        PackageManager.RefershPackage();
        foreach (var item in PackageManager.PackageData.Items) {
            if(item.ID == id) {
                return item.Name;
            }
        }
        return "未找到转换";
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        foreach (var item in PackageManager.PackageData.Items) {
            if(item.Name == value.ToString()) {
                return item.ID;
            }
        }
        return 0;
    }
}
