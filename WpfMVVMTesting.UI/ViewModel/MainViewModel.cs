
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModelInterface;

namespace WpfMVVMTesting.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationViewModel NavigationViewModel { get; private set; }
        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }

        public ICommand CloseFriendTabCommand { get; private set; }

        public ICommand AddFriendCommand { get; private set; }


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
            eventAggregator.GetEvent<FriendDeletedEvent>().Subscribe(OnFriendDeleted);
            CloseFriendTabCommand = new DelegateCommand<object>(OnCloseFriendTabExecute);
            AddFriendCommand = new DelegateCommand<object>(OnAddFriendExecute);
        }

        private void OnFriendDeleted(int friendId)
        {
            IFriendEditViewModel friendEditViewModel = FriendEditViewModels.Single(fevm => fevm.Friend.Id == friendId);
            FriendEditViewModels.Remove(friendEditViewModel);
        }

        private void OnCloseFriendTabExecute(object obj)
        {
            IFriendEditViewModel friendEditVm = (IFriendEditViewModel)obj;
            FriendEditViewModels.Remove(friendEditVm);
        }

        private void OnAddFriendExecute(object obj)
        {            
            SelectedFriendEditViewModel = CreateAndLoadFriendEditViewModel(null);
        }

        private IFriendEditViewModel CreateAndLoadFriendEditViewModel(int? friendId)
        {
            IFriendEditViewModel friendEditViewModel = _friendEditVmCreator();
            FriendEditViewModels.Add(friendEditViewModel);
            friendEditViewModel.Load(friendId);
            return friendEditViewModel;
        }

        private void OnOpenFriendEditView(int friendId)
        {
            IFriendEditViewModel friendEditViewModel = this.FriendEditViewModels.SingleOrDefault(fevm => fevm.Friend.Id == friendId);

            if (friendEditViewModel == null)
            {
                friendEditViewModel = CreateAndLoadFriendEditViewModel(friendId);
            }

            SelectedFriendEditViewModel = friendEditViewModel;
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
