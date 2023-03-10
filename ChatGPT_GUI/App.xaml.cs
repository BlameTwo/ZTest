using Microsoft.Extensions.Hosting;
using SimpleUI.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZTest.Tools.Interfaces;

namespace ChatGPT_GUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public IHost Host { get;}


        public App()
        {
            InitializeComponent();
            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .RegisterService()
                .RegisterView()
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e) {
            var main = App.GetSerivces<MainWindow>();
            App.GetSerivces<IThemeApply<App>>().App = this;
            App.GetSerivces<ILocalSetting>().InitSetting();
            this.MainWindow = main;
            main.Show();
            base.OnStartup(e);
        }
    }
}
