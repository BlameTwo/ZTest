using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SimpleUI.Controls
{
    public class Bar
    {

        public static double GetValue(DependencyObject obj)
        {
            return (double)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, double value)
        {
            obj.SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(double), typeof(Bar), new PropertyMetadata(0.0,(s,e)=>OnValueChanged(s,e)));

        private static void OnValueChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var control = (s as System.Windows.Controls.ProgressBar);
            DoubleAnimation da = new DoubleAnimation()
            {
                From= (double?)control.GetValue(ValueProperty),
                To = (double?)e.NewValue
                ,EasingFunction = new CubicEase()
                {
                     EasingMode = EasingMode.EaseInOut
                }
                ,Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            control.BeginAnimation(ProgressBar.ValueProperty, da);
        }
    }
}
