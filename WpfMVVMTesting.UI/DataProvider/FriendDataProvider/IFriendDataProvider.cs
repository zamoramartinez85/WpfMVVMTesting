using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.Models;

namespace WpfMVVMTesting.UI.DataProvider.FriendDataProvider
{
    public interface IFriendDataProvider
    {
        bool DeleteFriend(int friendId);

        Friend GetFriendById(int friendId);

        Friend SaveFriend(Friend friend);
    }
}
