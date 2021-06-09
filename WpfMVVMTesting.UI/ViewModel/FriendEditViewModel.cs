using System;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider.FriendDataProvider;
using WpfMVVMTesting.UI.ViewModelInterface;

namespace WpfMVVMTesting.UI.ViewModel
{

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private IFriendDataProvider _friendDataProvider;

        public FriendEditViewModel(IFriendDataProvider friendDataProvider)
        {
            this._friendDataProvider = friendDataProvider;
        }

        private Friend _friend;

        public Friend Friend
        {
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
            get
            {
                return _friend;
            }
        }

        

        public void Load(int friendId)
        {
            this.Friend = _friendDataProvider.GetFriendById(friendId);
        }
    }
}
