using SimpleUI.Base;
using SimpleUI.Helper;
using SimpleUI.Utils;
using System;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Interop;
using static SimpleUI.Utils.WindowBackdropBase;

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
            min = GetTemplateChild("MinBth") as System.Windows.Controls.Button;
            max = GetTemplateChild("MaxBth") as System.Windows.Controls.Button;
            exit = GetTemplateChild("ExitBth") as System.Windows.Controls.Button;
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



        public WindowBackdropBase WindowBackdrop {
            get { return (WindowBackdropBase)GetValue(WindowBackdropProperty); }
            set { SetValue(WindowBackdropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowBackdrop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowBackdropProperty =
            DependencyProperty.Register("WindowBackdrop", typeof(WindowBackdropBase), typeof(WindowBase), new PropertyMetadata(default(WindowBackdropBase)));

        private void OnBackdropChanged() {
            WindowInteropHelper mainWindowPtr = new WindowInteropHelper(this);
            WindowBackdropBase.RefreshFrame(mainWindowPtr.Handle);
            WindowBackdropBase.RefreshDarkMode(mainWindowPtr.Handle, 1);
            if (WindowBackdrop == null) return;
            switch (this.WindowBackdrop.BackType) {
                case WindowBackdropBase.Type.Acrylic:
                    BackgroundApply.SetWindowAttribute(mainWindowPtr.Handle, BackgroundEnum.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, WindowBackdropBase.Type.Acrylic);
                    break;
                case WindowBackdropBase.Type.Mica:
                    BackgroundApply.SetWindowAttribute(mainWindowPtr.Handle, BackgroundEnum.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, WindowBackdropBase.Type.Mica);
                    break;
                case WindowBackdropBase.Type.Tabbed:
                    BackgroundApply.SetWindowAttribute(mainWindowPtr.Handle, BackgroundEnum.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, WindowBackdropBase.Type.Tabbed);
                    break;
            }
            Win32.Win32.SetWindowLong(mainWindowPtr.Handle, Win32.Win32.GWL_STYLE, Win32.Win32.GetWindowLong(mainWindowPtr.Handle, Win32.Win32.GWL_STYLE) & ~Win32.Win32.WS_SYSMENU);
        }

        public Visibility MaxButtonVisibility
        {
            get { return (Visibility)GetValue(MaxButtonVisibilityProperty); }
            set { SetValue(MaxButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxButtonVisibilityProperty =
            DependencyProperty.Register("MaxButtonVisibility", typeof(Visibility), typeof(WindowBase), new PropertyMetadata(default(Visibility),MaxButtonVisChanged));

        private static void MaxButtonVisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is WindowBase visual && e.NewValue is Visibility value)
            {
                switch (value)
                {
                    case Visibility.Visible:
                        visual.ResizeMode = ResizeMode.CanResize;
                        break;
                    case Visibility.Hidden:
                        visual.ResizeMode = ResizeMode.NoResize;
                        break;
                    case Visibility.Collapsed:
                        visual.ResizeMode = ResizeMode.NoResize;
                        break;
                }
            }
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

            this.Loaded += WindowBase_Loaded;
            
        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                var handle = new WindowInteropHelper(this).Handle;
                var source = HwndSource.FromHwnd(handle);
                HwndSource source2 = HwndSource.FromHwnd(source.Handle);
                //source2.AddHook(HwndSourceHook);
                OnBackdropChanged();
            }
        }



        //private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        //{
        //    switch (msg)
        //    {
        //        case 0x0084:
        //            int x = lparam.ToInt32() & 0xffff;
        //            if (this.ResizeMode != ResizeMode.NoResize && (this.GetTemplateChild("MaxBth") as System.Windows.Controls.Button).IsMouseOver)
        //            {
        //                int y = lparam.ToInt32() >> 16;
        //                var DPI_SCALE = DpiHelper.LogicalToDeviceUnitsScalingFactorX;
        //                if (max != null)
        //                {
        //                    var rect = new Rect(max.PointToScreen(
        //                       new Point()),
        //                       new Size(max.Width * DPI_SCALE, max.Height * DPI_SCALE));
        //                    if (rect.Contains(new Point(x, y)))
        //                    {
        //                        handled = true;
        //                    }
        //                }
        //            }
        //            return new IntPtr(9);
        //    }

        //    return IntPtr.Zero;
        //}

        public object TitleBarRightContent
        {
            get { return (object)GetValue(TitleBarRightContentProperty); }
            set { SetValue(TitleBarRightContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleBarRightContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBarRightContentProperty =
            DependencyProperty.Register("TitleBarRightContent", typeof(object), typeof(WindowBase), new  FrameworkPropertyMetadata(null));




        public string MaxContent
        {
            get { return (string)GetValue(MaxContentProperty); }
            set { SetValue(MaxContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxContentProperty =
            DependencyProperty.Register("MaxContent", typeof(string), typeof(WindowBase), new PropertyMetadata("\uE922"));
        private System.Windows.Controls.Button? min;
        private System.Windows.Controls.Button? max;
        private System.Windows.Controls.Button? exit;
    }
}