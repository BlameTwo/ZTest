using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleUI.Utils.Controls
{
    public class ImageCornerRadius
    {


        public static CornerRadius GetImageCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(ImageCornerRadiusProperty);
        }

        public static void SetImageCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(ImageCornerRadiusProperty, value);
        }

        public static readonly DependencyProperty ImageCornerRadiusProperty =
            DependencyProperty.RegisterAttached("ImageCornerRadius", typeof(CornerRadius), typeof(ImageCornerRadius), new PropertyMetadata(new CornerRadius(0), CornerRadiusChanged));

        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Image img)
            {
                if (img.OpacityMask == null)
                {
                    Border border = new Border();
                    border.Background = Brushes.White;
                    VisualBrush brush = new VisualBrush();
                    brush.Visual = border;
                    img.OpacityMask = brush;
                    img.SizeChanged += delegate
                    {
                        border.Width = img.ActualWidth;
                        border.Height = img.ActualHeight;
                    };
                }
                if (img.OpacityMask is VisualBrush vb)
                    if (vb.Visual is Border bd)
                        bd.CornerRadius = (CornerRadius)e.NewValue;
            }
        }
    }
}
