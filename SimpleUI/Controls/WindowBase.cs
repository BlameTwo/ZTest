using System.Windows;

namespace SimpleUI.Controls
{
    [TemplatePart(Name = "MinBth", Type = typeof(Button))]
    [TemplatePart(Name = "MaxBth", Type = typeof(Button))]
    [TemplatePart(Name = "ExitBth", Type = typeof(Button))]
    public class WindowBase : Window
    {
        static WindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBase), new FrameworkPropertyMetadata(typeof(WindowBase)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var min = GetTemplateChild("MinBth") as System.Windows.Controls.Button;
            var max = GetTemplateChild("MaxBth") as System.Windows.Controls.Button;
            var exit = GetTemplateChild("ExitBth") as System.Windows.Controls.Button;
            min.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            max.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            };
            exit.Click += (s, e) =>
            {
                this.Close();
            };
        }

        public WindowBase()
        {
            this.StateChanged += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.BorderThickness = new Thickness(10);
                    MaxContent = "\uE923";
                }

                else if (this.WindowState == WindowState.Normal)
                {
                    this.BorderThickness = new Thickness(0);
                    MaxContent = "\uE922";
                }

            };
        }

        public string MaxContent
        {
            get { return (string)GetValue(MaxContentProperty); }
            set { SetValue(MaxContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxContentProperty =
            DependencyProperty.Register("MaxContent", typeof(string), typeof(WindowBase), new PropertyMetadata("\uE922"));
    }
}