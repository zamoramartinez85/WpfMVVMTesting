using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModel;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class NavigationItemViewModelTests
    {
        [Theory]
        [InlineData(7, "David")]
        [InlineData(7, "Francisco")]
        public void ShouldPublishOpenFriendEditViewEvent(int friendId, string displayMember)
        {            

            Mock<OpenFriendEditViewEvent> eventMock = new Mock<OpenFriendEditViewEvent>();
            Mock<IEventAggregator> eventAggregatorMock = new Mock<IEventAggregator>();

            eventAggregatorMock.Setup(ea => ea.GetEvent<OpenFriendEditViewEvent>()).Returns(eventMock.Object);

            NavigationItemViewModel viewModel = new NavigationItemViewModel(friendId, displayMember, eventAggregatorMock.Object);

            viewModel.OpenFriendEditViewCommand.Execute(null);

            eventMock.Verify(e => e.Publish(friendId), Times.Once);
        }
    }
}
