﻿using System;
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
using SyteLineDevTools.MVVM.Services;

namespace SyteLineDevTools.Views
{
    /// <summary>
    /// Interaction logic for Connections.xaml
    /// </summary>
    public partial class Connections : UserControl, IView
    {
        public Connections()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.Resolve(this.GetType());
        }
    }
}