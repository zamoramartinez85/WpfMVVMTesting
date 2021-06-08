﻿using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.ViewModel;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class NavigationViewModelTests
    {
        private readonly INavigationDataProvider _navigationDataProvider;

        private readonly Mock<INavigationDataProvider> _navigationDataProviderMock;

        public NavigationViewModelTests()
        {
            _navigationDataProviderMock = new Mock<INavigationDataProvider>();
            _navigationDataProvider = _navigationDataProviderMock.Object;
            _navigationDataProviderMock.Setup(nddpm => nddpm.GetAllFriends()).Returns(new List<LookUpItem>()
            {
                new LookUpItem() { Id = 1, DisplayMember = "Julia" },
                new LookUpItem() { Id = 2, DisplayMember = "Victoria" }
            });
        }

        [Fact]
        public void ShouldLoadFriends()
        {
            //Assert           
            NavigationViewModel viewModel = new NavigationViewModel(_navigationDataProvider);

            //Act
            viewModel.Load();

            //Assert
            Assert.Equal(2, viewModel.Friends.Count);
        }

        [Fact]
        public void ShouldLoadFriendsOnlyOnce()
        {
            //Assert
            NavigationViewModel viewModel = new NavigationViewModel(_navigationDataProvider);

            //Act
            viewModel.Load();
            viewModel.Load();

            //Assert
            Assert.Equal(2, viewModel.Friends.Count);
        }
    }
}
