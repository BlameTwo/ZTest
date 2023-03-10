using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            this.MainWindow = main;
            main.Show();
            base.OnStartup(e);
        }
    }
}
