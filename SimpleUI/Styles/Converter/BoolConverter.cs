using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleUI.Styles.Converter;
internal class BoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility vis)
            switch (vis)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Hidden:
                    return false;
                case Visibility.Collapsed:
                    return false;
            }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool bo)
            switch (bo)
            {
                case true:
                    return Visibility.Visible;
                case false:
                    return Visibility.Collapsed;
            }
        return Visibility.Collapsed;
    }
}
