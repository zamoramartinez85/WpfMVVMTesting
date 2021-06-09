using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.Models;

namespace WpfMVVMTesting.UI.DataProvider.FriendDataProvider
{
    public class FriendDataProvider : IFriendDataProvider
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public FriendDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public bool DeleteFriend(int friendId)
        {
            throw new NotImplementedException();
        }

        public Friend GetFriendById(int friendId)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetFriendById(friendId);
            }
        }

        public Friend SaveFriend(Friend friend)
        {
            throw new NotImplementedException();
        }
    }
}
