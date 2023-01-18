using ProgressApp.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class OpenUserMenuCommand : CommandBase
    {
        private MainViewModel _mainViewModel;
        private UserMenuViewModel _userMenuViewModel;
        private UserMenu userMenuWindow;
        public OpenUserMenuCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _userMenuViewModel = new UserMenuViewModel(mainViewModel, this);
        }
        public override void Execute(object? parameter)
        {
            userMenuWindow = new UserMenu();
            userMenuWindow.DataContext = _userMenuViewModel;
            userMenuWindow.Owner = _mainViewModel.MainWindow;
            userMenuWindow.ShowDialog();
        }

        public void CloseWindow()
        {
            userMenuWindow?.Close();
        }
    }
}
