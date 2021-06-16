using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.Wrapper;

namespace WpfMVVMTesting.UI.ViewModelInterface
{
    public interface IFriendEditViewModel
    {
        void Load(int friendId);

        FriendWrapper Friend { get; }
    }
}
