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

namespace ProgressApp.Components
{
    /// <summary>
    /// Interaction logic for ProjectListing.xaml
    /// </summary>
    public partial class ProjectListing : UserControl
    {
        public ProjectListing()
        {
            InitializeComponent();
        }

        private void ListView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Decorator border = VisualTreeHelper.GetChild(ListView, 0) as Decorator;
            ScrollViewer scrollViewer = border.Child as ScrollViewer;

            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta / 10);
                e.Handled = true;
            }

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            if (listViewItem != null)
            {
                ProjectListingItemViewModel vm = listViewItem.DataContext as ProjectListingItemViewModel;

                if (vm != null)
                {
                    vm.ItemClickedCommand.Execute(null);
                }
            }
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            if (listViewItem != null)
            {
                ProjectListingItemViewModel vm = listViewItem.DataContext as ProjectListingItemViewModel;

                if (vm != null)
                {
                    vm.ItemDoubleClickedCommand.Execute(null);
                }
            }
        }
    }
}
