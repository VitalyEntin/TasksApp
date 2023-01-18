using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ProgressApp
{
    public class ProjectListingItemViewModel : ViewModelBase
    {
        private readonly ProjectItem _projectListingItem;
        private readonly ProjectListingViewModel _projectListingViewModel;
        private bool _isLast;
        private bool _isSelected;

        public ProjectItem ProjectListingItem => _projectListingItem;
        public ProjectListingViewModel ProjectListingViewModel => _projectListingViewModel;
        public string Title 
        { 
            get 
            {
                if (!_isLast) return _projectListingItem.Title;
                else return "Add a new item";
            } 
        }
        public bool IsLast => _isLast;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        public string Color
        {
            get
            {
                if (!_isLast)
                {
                    if (_projectListingItem.ItemStatus == ProjectItem.Status.Planned) return "Grey";
                    if (_projectListingItem.ItemStatus == ProjectItem.Status.InProgress) return "Yellow";
                    if (_projectListingItem.ItemStatus == ProjectItem.Status.Completed) return "Green";
                    else return "Red";
                }
                else return "None";
            }
        }

        public bool HasSubItems
        {
            get
            {
                return _projectListingItem.HasSubItems && !IsLast;
            }
        }
        public ICommand ItemClickedCommand { get; }
        public ICommand ItemDoubleClickedCommand { get; }


        public ProjectListingItemViewModel(ProjectItem projectItem, bool isLast, ProjectListingViewModel projectListingViewModel)
        {
            _projectListingItem = projectItem;
            _isLast = isLast;
            _projectListingViewModel = projectListingViewModel;
            ItemClickedCommand = new SelectOrAddItemCommand(this);
            ItemDoubleClickedCommand = new PresentItemCommand(this);
        }

        public void SelectedProjectItemChanged() => OnPropertyChanged(nameof(IsSelected));
    }
}
