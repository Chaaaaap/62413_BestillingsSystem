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
using DesktopClient.ViewModels;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for ItemOverviewView.xaml
    /// </summary>
    public partial class ItemOverviewView : UserControl
    {
        public ItemOverviewView()
        {
            InitializeComponent();
            //DataContext = new ItemOverviewViewModel(null);
        }
    }
}
