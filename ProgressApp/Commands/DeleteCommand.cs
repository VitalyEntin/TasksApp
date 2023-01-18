using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProgressApp
{
    public class DeleteCommand : CommandBase
    {
        private MainViewModel _mainViewModel;

        public DeleteCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_mainViewModel.SelectedProjectItem != null)
            {

                var messageBox = new DeleteMessageBox(_mainViewModel.MainWindow);
                messageBox.ShowDialog();

                if (messageBox.Result == true)
                {
                    _mainViewModel.RemoveProjectItem(_mainViewModel.SelectedProjectItem);
                }
            }

        }
    }
}
