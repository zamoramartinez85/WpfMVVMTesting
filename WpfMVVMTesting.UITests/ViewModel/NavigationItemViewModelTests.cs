using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModel;
using WpfMVVMTesting.UITests.Extensions;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class NavigationItemViewModelTests
    {
        private NavigationItemViewModel _navigationItemViewModel;
        private Mock<OpenFriendEditViewEvent> _eventMock;
        private Mock<IEventAggregator> _eventAggregatorMock;

        private const int _friendId = 6;

        public NavigationItemViewModelTests()
        {
            _eventMock = new Mock<OpenFriendEditViewEvent>();
         
            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<OpenFriendEditViewEvent>()).Returns(_eventMock.Object);

            _navigationItemViewModel = new NavigationItemViewModel(_friendId, "David", _eventAggregatorMock.Object);
        }

        [Fact]
        public void ShouldPublishOpenFriendEditViewEvent()
        {
            //Act
            _navigationItemViewModel.OpenFriendEditViewCommand.Execute(null);

            //Assert
            _eventMock.Verify(e => e.Publish(_friendId), Times.Once);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForDisplayMember()
        {
            //Act
            bool fired = _navigationItemViewModel.IsPropertyChangedFired(() =>
            {
                _navigationItemViewModel.DisplayMember = "Lucas";
            }, nameof(_navigationItemViewModel.DisplayMember));

            //Assert
            Assert.True(fired);
        }
    }
}
