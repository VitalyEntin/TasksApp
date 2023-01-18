using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressApp
{
    public class UserMenuViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        private ObservableCollection<User> _usersList;
        private OpenUserMenuCommand _openUserMenuCommand;
        private User _selectedUser;
        public IEnumerable<User> UsersList => _usersList;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public ICommand AddUserCommand { get; }
        public ICommand SelectUserCommand { get; }

        public UserMenuViewModel(MainViewModel mainViewModel, OpenUserMenuCommand openUserMenuCommand)
        {
            _mainViewModel = mainViewModel;
            _openUserMenuCommand = openUserMenuCommand;
            _usersList = new ObservableCollection<User>();
            AddUserCommand = new AddUserCommand(mainViewModel, this);
            SelectUserCommand = new SelectUserCommand(this);

            RefreshListing();
        }
        public void RefreshListing()
        {
            _usersList.Clear();
            if (_mainViewModel.UsersList.Count > 0)
            {
                foreach (var user in _mainViewModel.UsersList)
                    _usersList.Add(user);
            }
            OnPropertyChanged(nameof(UsersList));
        }
        public void SelectUser()
        {
            _mainViewModel.ChangeUser(_selectedUser);
            _openUserMenuCommand.CloseWindow();
        }

    }
}
