using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class ChangeItemStatusCommand : CommandBase
    {
        private ProjectItemDetailsViewModel _projectItemDetailsViewModel;
        private ProjectItem.Status _status;

        public ChangeItemStatusCommand(ProjectItem.Status status, ProjectItemDetailsViewModel projectItemDetailsViewModel)
        {
            _status = status;
            _projectItemDetailsViewModel = projectItemDetailsViewModel;
        }

        public override void Execute(object? parameter)
        {
            if(_projectItemDetailsViewModel.SelectedProjectItem != null)
            {
                if(_status == ProjectItem.Status.InProgress)
                { 
                    if(_projectItemDetailsViewModel.SelectedProjectItem.ItemStatus == ProjectItem.Status.InProgress)
                        _projectItemDetailsViewModel.SelectedProjectItem.SetItemStatus(ProjectItem.Status.Planned);
                    else _projectItemDetailsViewModel.SelectedProjectItem.SetItemStatus(ProjectItem.Status.InProgress);
                }
                if (_status == ProjectItem.Status.Completed)
                {
                    if (_projectItemDetailsViewModel.SelectedProjectItem.ItemStatus == ProjectItem.Status.Completed)
                        _projectItemDetailsViewModel.SelectedProjectItem.SetItemStatus(ProjectItem.Status.InProgress);
                    else _projectItemDetailsViewModel.SelectedProjectItem.SetItemStatus(ProjectItem.Status.Completed);
                }
                _projectItemDetailsViewModel.MainViewModel.MainProjectListingViewModel.RefreshListing();
                _projectItemDetailsViewModel.MainViewModel.SecondaryProjectListingViewModel.RefreshListing();
                _projectItemDetailsViewModel.UpdateStatusButtons();
            }
        }
    }
}
