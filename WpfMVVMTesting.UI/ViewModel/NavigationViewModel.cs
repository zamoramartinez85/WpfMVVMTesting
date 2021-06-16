using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModelInterface;

namespace WpfMVVMTesting.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        public ObservableCollection<NavigationItemViewModel> Friends { get; private set; }

        private readonly INavigationDataProvider _dataProvider;

        private readonly IEventAggregator _eventAggregator;

        public NavigationViewModel(INavigationDataProvider dataProvider, IEventAggregator eventAggregator)
        {
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _dataProvider = dataProvider;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<FriendSavedEvent>().Subscribe(OnFriendSaved);
        }

        private void OnFriendSaved(Friend friend)
        {
            var navigationItem = Friends.Single(n => n.Id == friend.Id);
            navigationItem.DisplayMember = $"{friend.FirstName} {friend.LastName}";
        }

        public void Load()
        {
            Friends.Clear();
            foreach(LookUpItem friend in _dataProvider.GetAllFriends())
            {
                Friends.Add(new NavigationItemViewModel(friend.Id, friend.DisplayMember, _eventAggregator));
            }
        }
    }
}
