using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimpleUI.Styles.Converter
{
    public class PercentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = System.Convert.ToDouble(values[0]);
            double min = System.Convert.ToDouble(values[1]);
            double max = System.Convert.ToDouble(values[2]);
            if (value < min)
            {
                return 0.ToString() + "%";
            }
            if (value > max)
            {
                return 100.ToString() + "%";
            }
            return (((value - min) / (max - min)) * 100d).ToString("0") + "%";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class EndAngleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = System.Convert.ToDouble(values[0]);
            double min = System.Convert.ToDouble(values[1]);
            double max = System.Convert.ToDouble(values[2]);
            if (value < min)
            {
                return 0d;
            }
            if (value > max)
            {
                return 360;
            }
            return ((value - min) / (max - min)) * 360;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
