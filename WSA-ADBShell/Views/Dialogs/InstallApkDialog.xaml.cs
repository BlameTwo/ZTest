using SimpleUI.Controls;
using WSA_ADBShell.ViewModels.WindowsViewModels;
using AAPTForNet;
namespace WSA_ADBShell.Views.Dialogs
{
    /// <summary>
    /// InstallApkWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InstallApkDialog : DialogHost
    {
        public InstallApkDialog(InstallApkViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }

        public override void ShowExDate<T>(T data)
        {
            if(data is string str)
            {
                var res =  AAPTForNet.AAPTool.Decompile(str);
            }
        }
    }
}
