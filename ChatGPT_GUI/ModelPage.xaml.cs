using ChatGPT_GUI.ViewModels;
using SimpleUI.Interface;
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

namespace ChatGPT_GUI
{
    /// <summary>
    /// ModelPage.xaml 的交互逻辑
    /// </summary>
    public partial class ModelPage : Page,IPage
    {
        public ModelPage(ModelViewModel modelViewModel)
        {
            InitializeComponent();
            this.DataContext = modelViewModel;
        }

        public void NavigationLeaved(object data)
        {
            if(this.DataContext is ModelViewModel vm)
            {
                //在这里切换预注册对象
                switch (data.ToString())
                {
                    case "HuTao":
                        vm.ChatType = ModelViewModel.ModelType.HuTao;
                        vm.refershtoken();
                        break;
                    case "AiLi":
                        vm.ChatType = ModelViewModel.ModelType.AiLi;
                        vm.refershtoken();
                        break;
                    case "Ying":
                        vm.ChatType = ModelViewModel.ModelType.Ying;
                        vm.refershtoken();
                        break;
                }
            }
           
        }

        public void NavigationLoaded(object data)
        {

        }
    }
}
