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

namespace ProgressApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainWindowViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel = new MainViewModel();
            DataContext = MainWindowViewModel;
            MainWindowViewModel.MainWindow = this;
        }
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
