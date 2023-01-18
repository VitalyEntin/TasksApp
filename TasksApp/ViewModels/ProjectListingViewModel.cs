using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressApp
{
    public class ProjectListingViewModel : ViewModelBase
    {

        private readonly MainViewModel _mainViewModel;
        private readonly ProjectItem? _presentedProjectItem;
        private ObservableCollection<ProjectListingItemViewModel> _projectItemListingItems;
        private ProjectListingItemViewModel? _selectedProjectItemListingItem;

        public IEnumerable<ProjectListingItemViewModel> ProjectItemListingItems
        {
            get { return _projectItemListingItems; }
            set
            {
                OnPropertyChanged(nameof(ProjectItemListingItems));
            }
        }
        public ProjectListingItemViewModel? SelectedProjectItemListingItem
        {
            get { return _selectedProjectItemListingItem; }
            set
            {
                _selectedProjectItemListingItem = value;
                SelectedProjectItemListingItemChanged?.Invoke();
                OnPropertyChanged(nameof(SelectedProjectItemListingItem));
            }
        }
        public MainViewModel MainViewModel => _mainViewModel;
        public event Action SelectedProjectItemListingItemChanged;

        public ProjectListingViewModel(MainViewModel mainViewModel, ProjectItem? presentedProjectItem)
        {
            _mainViewModel = mainViewModel;
            _presentedProjectItem = presentedProjectItem;

            _projectItemListingItems = new ObservableCollection<ProjectListingItemViewModel>();
            RefreshListing();
        }   
        
        public void RefreshListing()
        {
            _projectItemListingItems.Clear();
            if (_presentedProjectItem == null) return;
            if(_presentedProjectItem.SubItems!=null && _presentedProjectItem.SubItems.Count>0)
            {
                foreach(var item in _presentedProjectItem.SubItems)
                    _projectItemListingItems.Add(new ProjectListingItemViewModel(item, false, this));
            }

            _projectItemListingItems.Add(new ProjectListingItemViewModel(_presentedProjectItem, true, this));
            OnPropertyChanged(nameof(ProjectItemListingItems));

            foreach (ProjectListingItemViewModel listingItem in ProjectItemListingItems)
            {
                if (MainViewModel.SelectedProjectItem!=null && 
                    listingItem.IsLast == false &&
                    listingItem.ProjectListingItem == MainViewModel.SelectedProjectItem) 
                        listingItem.IsSelected = true;
                else listingItem.IsSelected = false;
            }
        }

    }
}
