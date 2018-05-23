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

namespace DraftTool.UI.View
{
    /// <summary>
    /// Interaction logic for DraftView.xaml
    /// </summary>
    public partial class DraftView : UserControl
    {
        public DraftView()
        {
            InitializeComponent();
        }

        private void AvailableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChosenList.UnselectAll();
        }

        private void ChosenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AvailableList.UnselectAll();
        }
    }
}
