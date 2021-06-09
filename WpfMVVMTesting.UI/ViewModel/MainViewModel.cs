using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModelInterface;

namespace WpfMVVMTesting.UI.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
        public INavigationViewModel NavigationViewModel { get; private set; }
        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }

        private Func<IFriendEditViewModel> _friendEditVmCreator;
        private IFriendEditViewModel _selectedFriendEditViewModel;

        public IFriendEditViewModel SelectedFriendEditViewModel
        {
            get { return _selectedFriendEditViewModel; }
            set { _selectedFriendEditViewModel = value; OnPropertyChanged(); }
        }                

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendEditViewModel> friendEditVmCreator, IEventAggregator eventAggregator)
        {
            this.NavigationViewModel = navigationViewModel;
            this.FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
            _friendEditVmCreator = friendEditVmCreator;
            eventAggregator.GetEvent<OpenFriendEditViewEvent>().Subscribe(OnOpenFriendEditView);
        }

        private void OnOpenFriendEditView(int friendId)
        {
            IFriendEditViewModel friendEditViewModel = this.FriendEditViewModels.SingleOrDefault(fevm => fevm.Friend.Id == friendId);

            if (friendEditViewModel == null)
            {
                friendEditViewModel = _friendEditVmCreator();
                FriendEditViewModels.Add(friendEditViewModel);
                friendEditViewModel.Load(friendId);
            }

            SelectedFriendEditViewModel = friendEditViewModel;
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
