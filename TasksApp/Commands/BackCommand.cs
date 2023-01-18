using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class BackCommand : CommandBase
    {
        private MainViewModel _mainViewModel;

        public BackCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object? parameter)
        {
            if (_mainViewModel.PresentedProjectItem != null &&
                _mainViewModel.PresentedProjectItem.ParentProject != null &&
                _mainViewModel.PresentedProjectItem.ParentProject.ParentProject != null)
            {
                _mainViewModel.ChangePresentedItem(_mainViewModel.PresentedProjectItem.ParentProject);
            }

        }
    }
}
