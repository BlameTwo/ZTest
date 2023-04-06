using SonsOfTheForesrtSave_GUI.ViewModels;
using System.Windows.Controls;

namespace SonsOfTheForesrtSave_GUI.Views {
    /// <summary>
    /// ConfigurrationPage.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigurrationPage : Page {


        public ConfigurrationPage(ConfigurrationViewModel vm) {
            InitializeComponent();
            this.DataContext = vm;
        }

    }
}
