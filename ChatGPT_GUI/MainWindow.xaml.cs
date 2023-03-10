using ChatGPT_GUI.ViewModels;
using OpenAI.GPT3.Interfaces;
using SimpleUI.Controls;

namespace ChatGPT_GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase {
        public MainWindow(IOpenAIService openAIService,MainViewModel vm) {
            InitializeComponent();
            OpenAIService = openAIService;
            OpenAIService.SetDefaultModelId(OpenAI.GPT3.ObjectModels.Models.Davinci);
            this.DataContext = vm;
        }

        public IOpenAIService OpenAIService { get; }
    }
}
