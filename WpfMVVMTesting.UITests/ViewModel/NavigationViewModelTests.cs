using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModel;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class NavigationViewModelTests
    {
        private readonly INavigationDataProvider _navigationDataProvider;

        private readonly Mock<INavigationDataProvider> _navigationDataProviderMock;

        private readonly Mock<IEventAggregator> _eventAggregatorMock;

        private NavigationViewModel _navigationViewModel;
        private FriendSavedEvent _friendSavedEvent;
        private FriendDeletedEvent _friendDeletedEvent;

        public NavigationViewModelTests()
        {
            _friendSavedEvent = new FriendSavedEvent();
            _friendDeletedEvent = new FriendDeletedEvent();

            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(eam => eam.GetEvent<FriendSavedEvent>()).Returns(_friendSavedEvent);
            _eventAggregatorMock.Setup(eam => eam.GetEvent<FriendDeletedEvent>()).Returns(_friendDeletedEvent);

            _navigationDataProviderMock = new Mock<INavigationDataProvider>();
            _navigationDataProvider = _navigationDataProviderMock.Object;
            _navigationDataProviderMock.Setup(nddpm => nddpm.GetAllFriends()).Returns(new List<LookUpItem>()
            {
                new LookUpItem() { Id = 1, DisplayMember = "Julia" },
                new LookUpItem() { Id = 2, DisplayMember = "Victoria" }
            });

            _navigationViewModel = new NavigationViewModel(_navigationDataProvider, _eventAggregatorMock.Object);
        }

        [Fact]
        public void ShouldLoadFriends()
        {         
            //Act
            _navigationViewModel.Load();

            //Assert
            Assert.Equal(2, _navigationViewModel.Friends.Count);
        }

        [Fact]
        public void ShouldLoadFriendsOnlyOnce()
        {
            //Act
            _navigationViewModel.Load();
            _navigationViewModel.Load();

            //Assert
            Assert.Equal(2, _navigationViewModel.Friends.Count);
        }

        [Fact]
        public void ShouldUpdateNavigationItemWhenFriendIsSaved()
        {
            //Assert            
            _navigationViewModel.Load();

            NavigationItemViewModel navigationItem = _navigationViewModel.Friends.First();

            int friendId = navigationItem.Id;
            
            //Act
            _friendSavedEvent.Publish(
                new Friend()
                {
                    Id = friendId,
                    FirstName = "David",
                    LastName = "Zamora"
                }
            );

            //Assert
            Assert.Equal("David Zamora", navigationItem.DisplayMember);
        }

        [Fact]
        public void ShouldAddNavigationItemWhenAddedFriendIsSaved()
        {
            //Arrange
            _navigationViewModel.Load();

            int newFriendId = 85;

            //Act
            _friendSavedEvent.Publish(new Friend()
            {
                Id = newFriendId,
                FirstName = "Antonio",
                LastName = "Lacambra"
            });

            NavigationItemViewModel addedItem = _navigationViewModel.Friends.SingleOrDefault(x => x.Id == newFriendId);

            //Assert
            Assert.Equal(3, _navigationViewModel.Friends.Count);
            Assert.NotNull(addedItem);
            Assert.Equal("Antonio Lacambra", addedItem.DisplayMember);
        }

        [Fact]
        public void ShouldRemoveNavigationItemWhenFriendIsDeleted()
        {
            //Arrange
            _navigationViewModel.Load();
            int deletedFriendId = _navigationViewModel.Friends.First().Id;

            //Act
            _friendDeletedEvent.Publish(deletedFriendId);

            //Assert
            Assert.Single(_navigationViewModel.Friends);
            Assert.NotEqual(deletedFriendId, _navigationViewModel.Friends.Single().Id);
        }

    }
}
