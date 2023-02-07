using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WSA_ADBShell.ViewModels;

namespace WSA_ADBShell.Views
{
    /// <summary>
    /// ManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class ManagerPage : Page
    {
        public ManagerPage(ManagerViewModel managerViewModel)
        {
            this.DataContext = managerViewModel;
            InitializeComponent();
        }
    }
}
