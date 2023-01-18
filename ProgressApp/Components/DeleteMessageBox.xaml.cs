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

namespace ProgressApp
{
    /// <summary>
    /// Interaction logic for DeleteMessageBox.xaml
    /// </summary>
    public partial class DeleteMessageBox : Window
    {
        public bool Result { get; set; }
        public DeleteMessageBox(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result=false;
            Close();
        }
    }
}
