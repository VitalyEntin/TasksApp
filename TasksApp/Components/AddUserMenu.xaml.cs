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
using System.Windows.Shapes;

namespace ProgressApp.Components
{
    /// <summary>
    /// Interaction logic for AddUserMenu.xaml
    /// </summary>
    public partial class AddUserMenu : Window
    {
        public bool Result { get; set; }
        public string Username { get; set; }
        public AddUserMenu(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Username = username.Text;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }
    }
}
