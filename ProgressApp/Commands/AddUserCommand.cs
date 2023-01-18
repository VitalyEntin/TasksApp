using ProgressApp.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class AddUserCommand : CommandBase
    {
        private MainViewModel _mainViewModel;
        private UserMenuViewModel _userMenuViewModel;
        public AddUserCommand(MainViewModel mainViewModel, UserMenuViewModel userMenuViewModel)
        {
            _mainViewModel = mainViewModel;
            _userMenuViewModel = userMenuViewModel;
        }

        public override void Execute(object? parameter)
        {
            var addUserMenu = new AddUserMenu(_mainViewModel.MainWindow);
            addUserMenu.ShowDialog();
            if (addUserMenu.Result == true)
                _mainViewModel.AddUser(addUserMenu.Username);
            _userMenuViewModel.RefreshListing();
        }
    }
}
