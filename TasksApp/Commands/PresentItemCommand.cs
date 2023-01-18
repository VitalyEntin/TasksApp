using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class PresentItemCommand : CommandBase
    {
        private ProjectListingItemViewModel _projectListingItemViewModel;

        public PresentItemCommand(ProjectListingItemViewModel projectListingItemViewModel)
        {
            _projectListingItemViewModel = projectListingItemViewModel;
        }

        public override void Execute(object? parameter)
        {
            _projectListingItemViewModel.ProjectListingViewModel.MainViewModel.ChangePresentedItem(_projectListingItemViewModel.ProjectListingItem);
        }
    }
}
