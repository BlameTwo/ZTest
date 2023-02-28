using Microsoft.Extensions.Hosting;
using SimpleUI.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SonsOfTheForesrtSave_GUI
{
    public partial class App : Application
    {
        public IHost Host { get; set; }

        public App()
        {
            InitializeComponent();
            this.Startup += App_Startup;
            this.Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .RegisterView()
                .RegisterExtend()
                .RegisterViewModel()
                .Build();
        }


        


        private void App_Startup(object sender, StartupEventArgs e)
        {
            var windowmanager = App.GetService<IWindowManager>();
            windowmanager.Register<MainWindow>(nameof(SonsOfTheForesrtSave_GUI.MainWindow));
            windowmanager.Register<FirstSteamConfig>(nameof(FirstSteamConfig));


            var result = App.GetService<FirstSteamConfig>();
            this.MainWindow = result;
            result.Show();
        }
    }
}
