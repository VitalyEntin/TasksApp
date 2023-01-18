using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class SelectUserCommand : CommandBase
    {
        UserMenuViewModel _userMenuViewModel;

        public SelectUserCommand(UserMenuViewModel userMenuViewModel)
        {
            _userMenuViewModel = userMenuViewModel;
        }

        public override void Execute(object? parameter)
        {
            _userMenuViewModel.SelectUser();
        }
    }
}
