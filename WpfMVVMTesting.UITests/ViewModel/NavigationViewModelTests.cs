using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.ViewModel;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class NavigationDataProviderMock : INavigationDataProvider
    {
        public IEnumerable<Friend> GetAllFriends()
        {
            yield return new Friend() { Id = 1, FirstName = "Julia" };
            yield return new Friend() { Id = 2, FirstName = "Victoria" };
        }
    }

    public class NavigationViewModelTests
    {
        public NavigationViewModelTests()
        {
            Console.WriteLine();
        }

        [Fact]
        public void ShouldLoadFriends()
        {
            var viewModel = new NavigationViewModel(new NavigationDataProviderMock());

            viewModel.Load();

            Assert.Equal(2, viewModel.Friends.Count);
        }
    }
}
