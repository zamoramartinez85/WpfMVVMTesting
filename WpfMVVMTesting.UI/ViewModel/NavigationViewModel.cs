using System;
using System.Collections.ObjectModel;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider;

namespace WpfMVVMTesting.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        public ObservableCollection<Friend> Friends { get; private set; }

        private readonly INavigationDataProvider _dataProvider;

        public NavigationViewModel(INavigationDataProvider dataProvider)
        {
            Friends = new ObservableCollection<Friend>();
            _dataProvider = dataProvider;
        }

        public void Load()
        {            
            foreach(Friend friend in _dataProvider.GetAllFriends())
            {
                Friends.Add(friend);
            }
        }
    }
}
