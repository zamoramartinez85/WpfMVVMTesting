using System;
using System.Collections.ObjectModel;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.ViewModelInterface;

namespace WpfMVVMTesting.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        public ObservableCollection<LookUpItem> Friends { get; private set; }

        private readonly INavigationDataProvider _dataProvider;

        public NavigationViewModel(INavigationDataProvider dataProvider)
        {
            Friends = new ObservableCollection<LookUpItem>();
            _dataProvider = dataProvider;
        }

        public void Load()
        {
            Friends.Clear();
            foreach(LookUpItem friend in _dataProvider.GetAllFriends())
            {
                Friends.Add(friend);
            }
        }
    }
}
