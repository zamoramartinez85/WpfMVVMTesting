using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.Models;

namespace WpfMVVMTesting.UI.DataProvider
{
    public interface INavigationDataProvider
    {
        IEnumerable<LookUpItem> GetAllFriends();
    }
}
