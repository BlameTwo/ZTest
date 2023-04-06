﻿using SonsOfTheForesrtSave_GUI.ViewModels;
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

namespace SonsOfTheForesrtSave_GUI.Views
{
    /// <summary>
    /// WorldSaveStatePage.xaml 的交互逻辑
    /// </summary>
    public partial class WorldSaveStatePage : Page
    {
        public WorldSaveStatePage(WorldSaveStateViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
