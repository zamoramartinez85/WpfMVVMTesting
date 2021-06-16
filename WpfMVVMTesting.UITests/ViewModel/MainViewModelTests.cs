using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModel;
using WpfMVVMTesting.UI.ViewModelInterface;
using WpfMVVMTesting.UITests.Extensions;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class MainViewModelTests
    {
        private readonly INavigationViewModel _navigationViewModel;
        private readonly OpenFriendEditViewEvent _openFriendEditViewEvent;
        private readonly Mock<INavigationViewModel> _navigationViewModelMock;
        private readonly MainViewModel _mainViewModel;
        private readonly Mock<IEventAggregator> _eventAggregatorMock;
        private readonly List<Mock<IFriendEditViewModel>> _friendEditViewModelMocks;

        public MainViewModelTests()
        {
            _friendEditViewModelMocks = new List<Mock<IFriendEditViewModel>>();
            _navigationViewModelMock = new Mock<INavigationViewModel>();
            _navigationViewModel = _navigationViewModelMock.Object;

            _openFriendEditViewEvent = new OpenFriendEditViewEvent();

            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(eam => eam.GetEvent<OpenFriendEditViewEvent>()).Returns(_openFriendEditViewEvent);

            _mainViewModel = new MainViewModel(_navigationViewModel, CreateFriendEditViewModel, _eventAggregatorMock.Object);
        }

        private IFriendEditViewModel CreateFriendEditViewModel()
        {
            Mock<IFriendEditViewModel> friendEditViewModelMock = new Mock<IFriendEditViewModel>();
            friendEditViewModelMock.Setup(fevmm => fevmm.Load(It.IsAny<int>()))
                .Callback<int>(friendId =>
                {
                    friendEditViewModelMock.Setup(fevmm => fevmm.Friend).Returns(new UI.Wrapper.FriendWrapper(new Models.Friend() { Id = friendId }));
                });
            _friendEditViewModelMocks.Add(friendEditViewModelMock);
            return friendEditViewModelMock.Object;
        }

        [Fact]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            //Act
            _mainViewModel.Load();

            //Assert
            _navigationViewModelMock.Verify(vm => vm.Load(), Times.Once);
        }

        [Theory]
        [InlineData(6)]
        public void ShouldAddFriendEditViewModelAndLoadAndSelectIt(int friendId)
        {
            _openFriendEditViewEvent.Publish(friendId);
            IFriendEditViewModel friendEditViewModel = _mainViewModel.FriendEditViewModels.First();

            Assert.Equal(1, _mainViewModel.FriendEditViewModels.Count);
            Assert.Equal(friendEditViewModel, _mainViewModel.SelectedFriendEditViewModel);
            _friendEditViewModelMocks.First().Verify(fevmm => fevmm.Load(friendId), Times.Once);
        }

        [Fact]
        public void ShouldAddFriendEditViewModelOnlyOnce()
        {
            _openFriendEditViewEvent.Publish(5);
            _openFriendEditViewEvent.Publish(5);
            _openFriendEditViewEvent.Publish(6);
            _openFriendEditViewEvent.Publish(7);
            _openFriendEditViewEvent.Publish(7);

            Assert.Equal(3, _mainViewModel.FriendEditViewModels.Count);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForSelectedFriendEditViewModel()
        {
            Mock<IFriendEditViewModel> friedEditViewModelMock = new Mock<IFriendEditViewModel>();
            bool fired = _mainViewModel.IsPropertyChangedFired(() => { 
            
                _mainViewModel.SelectedFriendEditViewModel = friedEditViewModelMock.Object;
            }, nameof(_mainViewModel.SelectedFriendEditViewModel));

            Assert.True(fired);
        }

        [Theory]
        [InlineData(6)]
        public void ShouldRemoveFriendEditViewModelOnCloseFriendTabCommand(int friendId)
        {
            _openFriendEditViewEvent.Publish(friendId);

            IFriendEditViewModel friendEditVm = _mainViewModel.SelectedFriendEditViewModel;

            _mainViewModel.CloseFriendTabCommand.Execute(friendEditVm);

            Assert.Empty(_mainViewModel.FriendEditViewModels);
        }
    }
}
