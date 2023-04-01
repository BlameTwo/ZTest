using ChatGPT_GUI.ViewModels;
using SimpleUI.Controls;
using System;
using System.Windows.Automation.Peers;
using System.Windows.Interop;

namespace ChatGPT_GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase {
        public MainWindow(MainViewModel vm) {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.DataContext = vm;
        }
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {

            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            //SimpleUI.Win32.Win32.SetWindowLong(hwnd, (-20), 0x80);
            //SimpleUI.Win32.Win32.SetWindowLong(hwnd, GWL_STYLE, SimpleUI.Win32.Win32.GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            if (this.DataContext is MainViewModel vm) {
                vm.ToastLitterMessage.ShowOwner = this.grid;
                vm.ToastLitterMessage.ShowTime = TimeSpan.FromSeconds(5);
                vm.AppNavigationViewService.Init(this.Framebase, true);
            }
        }

        
    }
}
