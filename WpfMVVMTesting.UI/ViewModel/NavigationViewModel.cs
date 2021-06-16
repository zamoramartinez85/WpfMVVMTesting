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
            _eventAggregator.GetEvent<FriendDeletedEvent>().Subscribe(OnFriendDeleted);
        }

        private void OnFriendDeleted(int friendId)
        {
            NavigationItemViewModel navigationItem = Friends.Single(x => x.Id == friendId);
            Friends.Remove(navigationItem);
        }

        private void OnFriendSaved(Friend friend)
        {
            NavigationItemViewModel navigationItem = Friends.SingleOrDefault(n => n.Id == friend.Id);
            string displayMember = $"{friend.FirstName} {friend.LastName}";

            if (navigationItem != null)            
                navigationItem.DisplayMember = displayMember;            
            else
            {
                navigationItem = new NavigationItemViewModel(friend.Id, displayMember, _eventAggregator);
                Friends.Add(navigationItem);
            }      
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
