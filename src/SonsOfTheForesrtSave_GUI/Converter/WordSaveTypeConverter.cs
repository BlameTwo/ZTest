using System;
using System.Globalization;
using System.Windows.Data;

namespace SonsOfTheForesrtSave_GUI.Converter;
public class WordSaveTypeConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is string str) {
            switch (str) {
                case "Normal":
                    return "默认";
                case "Peaceful":
                    return "简单";
                case "Hard":
                    return "平常";
                case "Custom":
                    return "困难";
            }
        }
        return "NULL";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is string str) {
            switch (str) {
                case "默认":
                    return "Normal";
                case "简单":
                    return "Peaceful";
                case "平常":
                    return "Hard";
                case "困难":
                    return "Custom";
            }
        }
        return "NULL";
    }
}
