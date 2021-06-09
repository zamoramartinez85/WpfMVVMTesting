using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.Models;

namespace WpfMVVMTesting.UI.ViewModelInterface
{
    public interface IFriendEditViewModel
    {
        void Load(int friendId);

        Friend Friend { get; }
    }
}
