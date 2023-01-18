using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class EditCommand : CommandBase
    {
        private MainViewModel _mainViewModel;

        public EditCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object? parameter)
        {
            if(_mainViewModel.EditingTurnedOn==false)
            {
                _mainViewModel.EditingTurnedOn = true;
                _mainViewModel.SelectedProjectItemDetailsViewModel.EditEnabled = true;
            }
            else
            {
                _mainViewModel.SelectedProjectItemDetailsViewModel.EditEnabled = false;
                _mainViewModel.EditingTurnedOn = false;
                DataAccess.SaveItemEditToDB(_mainViewModel.SelectedProjectItem);
                _mainViewModel.MainProjectListingViewModel.RefreshListing();
                _mainViewModel.SecondaryProjectListingViewModel.RefreshListing();
            }
        }
    }
}
