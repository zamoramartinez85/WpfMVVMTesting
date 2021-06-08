using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.Models;

namespace WpfMVVMTesting.DataAccess
{
    public interface IDataService : IDisposable
    {
        Friend GetFriendById(int friendId);

        void SaveFriend(Friend friend);

        void DeleteFriend(int friendId);

        IEnumerable<LookUpItem> GetAllFriends();
    }
}
