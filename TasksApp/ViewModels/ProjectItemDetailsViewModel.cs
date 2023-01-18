using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressApp
{ 
    public class ProjectItemDetailsViewModel : ViewModelBase
    {
        private ProjectItem? _selectedProjectItem;
        private MainViewModel _mainViewModel;
        private bool _editEnabled;

        public MainViewModel MainViewModel => _mainViewModel;
        public ProjectItem? SelectedProjectItem => _selectedProjectItem;
        public bool IsProjectItemSelected => _selectedProjectItem != null;
        public bool EditEnabled 
        { 
            get { return _editEnabled; }
            set
            {
                _editEnabled = value;
                OnPropertyChanged(nameof(EditEnabled));
                OnPropertyChanged(nameof(StatusButtonsEnabled));
            }
        }
        public string Title
        {
            get 
            {
                return IsProjectItemSelected ? _selectedProjectItem.Title : ""; 
            }
            set 
            {
               if(IsProjectItemSelected) _selectedProjectItem.Title = value; 
               OnPropertyChanged(nameof(Title));
            }       
            
        }
        public string Description
        {
            get
            {
                return IsProjectItemSelected ? _selectedProjectItem.Description : ""; 
            }
            set
            {
                if (IsProjectItemSelected) _selectedProjectItem.Description = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public bool StatusButtonsEnabled
        { 
            get
            {
                if (_selectedProjectItem != null)
                    return _editEnabled && !_selectedProjectItem.HasSubItems;
                else return false;
            }
        }
        public bool IsInProgress
        {
            get
            {
                if (_selectedProjectItem != null)
                    return _selectedProjectItem.ItemStatus == ProjectItem.Status.InProgress;
                else return false;
            }
        }
        public bool IsCompleted
        {
            get
            {
                if (_selectedProjectItem != null)
                    return _selectedProjectItem.ItemStatus == ProjectItem.Status.Completed;
                else return false;
            }
        }
        public ICommand SetInProgress { get; set; }
        public ICommand SetCompleted { get; set; }

        public ProjectItemDetailsViewModel(MainViewModel mainViewModel)
        {
            _selectedProjectItem = mainViewModel.SelectedProjectItem;
            _mainViewModel = mainViewModel;
            SetInProgress = new ChangeItemStatusCommand(ProjectItem.Status.InProgress, this);
            SetCompleted = new ChangeItemStatusCommand(ProjectItem.Status.Completed, this);
        }

        public void ChangeSelectedProjectItem(ProjectItem? projectItem)
        {
            _selectedProjectItem = projectItem;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            UpdateStatusButtons();

        }
        public void UpdateStatusButtons()
        {
            OnPropertyChanged(nameof(IsInProgress));
            OnPropertyChanged(nameof(IsCompleted));
        }
    }
}
