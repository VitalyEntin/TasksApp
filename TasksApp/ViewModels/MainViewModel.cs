using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ProgressApp
{
    public class MainViewModel : ViewModelBase
    {
        private List<User> _usersList;
        private User? _selectedUser;
        private List<ProjectItem> _selectedUserProjectItems;

        private ProjectItem? _presentedProjectItem;
        private ProjectItem? _presentedProjectSubItem;
        private ProjectItem? _selectedProjectItem;

        private ProjectListingViewModel _mainProjectListingViewModel;
        private ProjectListingViewModel _secondaryProjectListingViewModel;
        private ProjectItemDetailsViewModel _selectedProjectItemDetailsViewModel;


        private bool _editingTurnedOn;

        public Window MainWindow { get; set; }
        public List<User> UsersList => _usersList;
        public User? SelectedUser => _selectedUser;
        public List<ProjectItem> UserProjectItems => _selectedUserProjectItems;
        public ProjectItem? PresentedProjectItem
        {
            get { return _presentedProjectItem; }
            set
            {
                _presentedProjectItem = value;
                _mainProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectItem);

                OnPropertyChanged(nameof(PresentedProjectItem));
                OnPropertyChanged(nameof(MainProjectListingViewModel));
                OnPropertyChanged(nameof(PresentedProjectSubItem));
            }
        }
        public ProjectItem? PresentedProjectSubItem
        {
            get { return _presentedProjectSubItem; }
            set
            {
                _presentedProjectSubItem = value;
                _secondaryProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectSubItem);

                OnPropertyChanged(nameof(PresentedProjectSubItem));
                OnPropertyChanged(nameof(SecondaryProjectListingViewModel));
                OnPropertyChanged(nameof(SelectedProjectItemDetailsViewModel));
            }
        }
        public ProjectItem? SelectedProjectItem => _selectedProjectItem;
        public ProjectListingViewModel MainProjectListingViewModel => _mainProjectListingViewModel;
        public ProjectListingViewModel SecondaryProjectListingViewModel => _secondaryProjectListingViewModel;
        public ProjectItemDetailsViewModel SelectedProjectItemDetailsViewModel => _selectedProjectItemDetailsViewModel;
        public bool EditingTurnedOn
        {
            get { return _editingTurnedOn; }
            set
            {
                _editingTurnedOn = value;
                OnPropertyChanged(nameof(EditingTurnedOn));
            }
        }
        public string? SelectedUserName => _selectedUser != null ? _selectedUser.Username : "Select user";

        public ICommand HomeCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UserButtonCommand { get; set; }

        public MainViewModel()
        {
            _usersList = new List<User>();

            _mainProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectItem);
            _secondaryProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectSubItem);
            _selectedProjectItemDetailsViewModel = new ProjectItemDetailsViewModel(this);

            HomeCommand = new HomeCommand(this);
            EditCommand = new EditCommand(this);
            BackCommand = new BackCommand(this);
            DeleteCommand = new DeleteCommand(this);

            new ProjectItemDbContext().EnsureCreated();
            _usersList.AddRange(DataAccess.LoadAllUsers());

            UserButtonCommand = new OpenUserMenuCommand(this);
        }
        public void RemoveProjectItem(ProjectItem ItemToRemove)
        {
            ItemToRemove.ParentProject.SubItems.Remove(ItemToRemove);
            DataAccess.RemoveItemAndSubitems(ItemToRemove);

            _selectedUserProjectItems = ProjectItemManager.RootProjectToList(SelectedUser.RootProject);
            OnPropertyChanged(nameof(UserProjectItems));

            RefreshListings();
            PresentedProjectSubItem = PresentedProjectItem.SubItems[0];
        }
        public void RefreshListings()
        {
            MainProjectListingViewModel.RefreshListing();
            SecondaryProjectListingViewModel.RefreshListing();

            OnPropertyChanged(nameof(MainProjectListingViewModel));
            OnPropertyChanged(nameof(SecondaryProjectListingViewModel));
        }
        public void AddUser(string username)
        {
            var userDto = new UserDto(username);
            DataAccess.AddUserToDb(userDto);
            _usersList.Add(new User(username, userDto.Id, userDto.RootProjectId));
        }
        public void ChangeUser(User user)
        {
            if (user == _selectedUser) return;
            _selectedUser = user;
            _selectedUserProjectItems = DataAccess.GetAllUserProjects(user.ID).ToList();
            user.RootProject = _selectedUserProjectItems.Where(i => i.ParentProject == null).FirstOrDefault();

            OnPropertyChanged(nameof(SelectedUser));
            OnPropertyChanged(nameof(SelectedUserName));
            OnPropertyChanged(nameof(UserProjectItems));

            _presentedProjectItem = user.RootProject;
            if (_presentedProjectItem.SubItems != null && _presentedProjectItem.SubItems.Count > 0)
                _presentedProjectSubItem = _presentedProjectItem.SubItems[0];
            else _presentedProjectSubItem = null;
            _selectedProjectItem = null;

            OnPropertyChanged(nameof(PresentedProjectItem));
            OnPropertyChanged(nameof(PresentedProjectSubItem));
            OnPropertyChanged(nameof(SelectedProjectItem));

            _mainProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectItem);
            _secondaryProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectSubItem);
            _selectedProjectItemDetailsViewModel = new ProjectItemDetailsViewModel(this);

            OnPropertyChanged(nameof(MainProjectListingViewModel));
            OnPropertyChanged(nameof(SecondaryProjectListingViewModel));
            OnPropertyChanged(nameof(SelectedProjectItemDetailsViewModel));
        }
        public void ChangePresentedItem(ProjectItem itemToPresent)
        {
            _presentedProjectItem = itemToPresent;
            _presentedProjectSubItem = null;
            _selectedProjectItem = null;

            OnPropertyChanged(nameof(PresentedProjectItem));
            OnPropertyChanged(nameof(PresentedProjectSubItem));
            OnPropertyChanged(nameof(SelectedProjectItem));

            _mainProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectItem);
            _secondaryProjectListingViewModel = new ProjectListingViewModel(this, _presentedProjectSubItem);
            _selectedProjectItemDetailsViewModel = new ProjectItemDetailsViewModel(this);
            _editingTurnedOn = false;
            SelectedProjectItemDetailsViewModel.EditEnabled = false;

            OnPropertyChanged(nameof(MainProjectListingViewModel));
            OnPropertyChanged(nameof(SecondaryProjectListingViewModel));
            OnPropertyChanged(nameof(SelectedProjectItemDetailsViewModel));
            OnPropertyChanged(nameof(EditingTurnedOn));
        }
        public void AddSubItemToItem(string SubItemTitle, ProjectItem parentProjectItem)
        {
            var projectItemDto = new ProjectItemDto(SubItemTitle,"","Planned", parentProjectItem.ID);
            DataAccess.AddItemToDB(projectItemDto);

            parentProjectItem.AddSubItem(projectItemDto.Title, projectItemDto.ID);

            _selectedUserProjectItems = ProjectItemManager.RootProjectToList(SelectedUser.RootProject);
            OnPropertyChanged(nameof(UserProjectItems));

            RefreshListings();
        }
        public void ChangeSelectedItem(ProjectItem selectedItem)
        {
            _selectedProjectItem = selectedItem;
            _selectedProjectItemDetailsViewModel.ChangeSelectedProjectItem(selectedItem);
            _editingTurnedOn = false;
            SelectedProjectItemDetailsViewModel.EditEnabled = false;
            OnPropertyChanged(nameof(EditingTurnedOn));

            foreach (ProjectListingItemViewModel listingItem in MainProjectListingViewModel.ProjectItemListingItems)
            {
                if (listingItem.ProjectListingItem == selectedItem) listingItem.IsSelected = true;
                else listingItem.IsSelected = false;
            }
            foreach (ProjectListingItemViewModel listingItem in SecondaryProjectListingViewModel.ProjectItemListingItems)
            {
                if (listingItem.ProjectListingItem == selectedItem) listingItem.IsSelected = true;
                else listingItem.IsSelected = false;
            }

            if (selectedItem.ParentProject == PresentedProjectItem)
            {
                _presentedProjectSubItem = selectedItem;
                _secondaryProjectListingViewModel = new ProjectListingViewModel(this, selectedItem);

                OnPropertyChanged(nameof(PresentedProjectSubItem));
                OnPropertyChanged(nameof(SelectedProjectItem));
                OnPropertyChanged(nameof(SecondaryProjectListingViewModel));
                OnPropertyChanged(nameof(SelectedProjectItemDetailsViewModel));
            }

        }

    }
}
