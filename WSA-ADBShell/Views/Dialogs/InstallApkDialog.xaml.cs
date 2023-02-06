using SimpleUI.Controls;
using AAPTForNet;
using WSA_ADBShell.ViewModels.DialogViewModels;
using AdbShell;
using CommunityToolkit.Mvvm.Input;
using AdbShell.Interfaces;
using System.Diagnostics;
using SimpleUI.Interface.AppInterfaces;

namespace WSA_ADBShell.Views.Dialogs
{
    /// <summary>
    /// InstallApkWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InstallApkDialog : DialogHost
    {
        public InstallApkDialog(InstallApkViewModel vm,IPackageManager packageManager,IToastLitterMessage toastLitterMessage)
        {
            this.DataContext = vm;
            InitializeComponent();
            PackageManager = packageManager;
            ToastLitterMessage = toastLitterMessage;
            this.probar.Height= 0;
            this.probar.IsIndeterminate = false;
            closebth.IsEnabled = true;
            gobth.IsEnabled = true;
        }

        public IPackageManager PackageManager { get; }
        public IToastLitterMessage ToastLitterMessage { get; }

        public override void ShowExDate<T>(T data)
        {
            if(data is string str && this.DataContext is InstallApkViewModel vm)
            {
                var res =  AAPTForNet.AAPTool.Decompile(str);
                vm.Info = res;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if(this.DataContext is InstallApkViewModel vm)
            {
                closebth.IsEnabled = false;
                gobth.IsEnabled = false;
                this.probar.IsIndeterminate = true;
                this.probar.Height = 5;
                var result = await PackageManager.InstallAsync(vm.Info.FullPath, (s) =>
                {
                    Debug.WriteLine(s);
                },
                vm.AdbManager.HotDevice);
                if (result.Success)
                    ToastLitterMessage.Show("安装完毕！");
                else
                    ToastLitterMessage.Show("安装失败，请检查日志！");

                this.Close();
                this.probar.IsIndeterminate = false;
            }
        }
    }
}
