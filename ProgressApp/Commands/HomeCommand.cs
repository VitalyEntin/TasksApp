using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class HomeCommand : CommandBase
    {
        private MainViewModel _mainViewModel;

        public HomeCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object? parameter)
        {
            if(_mainViewModel.SelectedUser != null)
                _mainViewModel.ChangePresentedItem(_mainViewModel.SelectedUser.RootProject);            
        }
    }
}
