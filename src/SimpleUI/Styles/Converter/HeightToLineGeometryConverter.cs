using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SimpleUI.Styles.Converter
{
    internal class HeightToLineGeometryConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 获取高度值
            double height = (double)value;
            // 创建一条竖线的几何图形，起点为 (10, 10)，终点为 (10, height - 10)
            LineGeometry line = new LineGeometry(new Point(10, 10), new Point(10, height - 10));
            // 返回几何图形
            return line;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 不需要反向转换
            throw new NotImplementedException();
        }
    }
}
