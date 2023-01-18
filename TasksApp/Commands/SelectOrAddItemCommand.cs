using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class SelectOrAddItemCommand : CommandBase
    {
        private ProjectItem _projectItem;
        private bool _islast;
        private ProjectListingItemViewModel _projectListingItemViewModel;
        private MainViewModel _mainViewModel;
        public SelectOrAddItemCommand(ProjectListingItemViewModel projectListingItemViewModel)
        {
            _projectItem = projectListingItemViewModel.ProjectListingItem;
            _islast = projectListingItemViewModel.IsLast;
            _projectListingItemViewModel = projectListingItemViewModel;
            _mainViewModel = projectListingItemViewModel.ProjectListingViewModel.MainViewModel;
        }
        public override void Execute(object? parameter)
        {
            if (_islast)
                _mainViewModel.AddSubItemToItem("New item", _projectItem);
            else
                _mainViewModel.ChangeSelectedItem(_projectItem);
        }
    }
}
