using ChatGPT_GUI.ViewModels;
using OpenAI.GPT3.Interfaces;
using SimpleUI.Controls;

namespace ChatGPT_GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase {
        public MainWindow(MainViewModel vm) {
            InitializeComponent();
            this.DataContext = vm;
        }

    }
}
