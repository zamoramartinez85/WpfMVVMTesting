using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.Models;

namespace WpfMVVMTesting.UI.DataProvider
{
    public class NavigationDataProvider : INavigationDataProvider
    {
        private Func<IDataService> _dataServiceCreator;

        public NavigationDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        } 

        public IEnumerable<LookUpItem> GetAllFriends()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllFriends();
            }
        }
    }
}
