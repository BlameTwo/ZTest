using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using static ChatGPT_GUI.ViewModels.ModelViewModel;

namespace ChatGPT_GUI.Resources.Converter;

public class ImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is ModelType type)
        {
            switch (type)
            {
                case ModelType.HuTao:
                    return new BitmapImage(new Uri("pack://application:,,,/ChatGPT_GUI;component/Resources/aislogo.jpg"));
                case ModelType.Ying:
                    return new BitmapImage(new Uri("pack://application:,,,/ChatGPT_GUI;component/Resources/ying.png"));
                case ModelType.AiLi:
                    return new BitmapImage(new Uri("pack://application:,,,/ChatGPT_GUI;component/Resources/aili.png"));
            }
        }

        return new BitmapImage();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
