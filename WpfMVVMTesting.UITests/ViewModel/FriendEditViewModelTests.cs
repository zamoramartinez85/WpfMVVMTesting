using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.UI.DataProvider.FriendDataProvider;
using WpfMVVMTesting.UI.ViewModel;
using WpfMVVMTesting.UITests.Extensions;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class FriendEditViewModelTests
    {
       
        private Mock<IFriendDataProvider> _dataProviderMock;
        private FriendEditViewModel _viewModel;

        public FriendEditViewModelTests()
        {
            _dataProviderMock = new Mock<IFriendDataProvider>();

            _viewModel = new FriendEditViewModel(_dataProviderMock.Object);
        }

        [Theory]
        [InlineData(5)]
        public void ShouldLoadFriend(int friendId)
        {
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);

            Assert.NotNull(_viewModel.Friend);
            Assert.Equal(friendId, _viewModel.Friend.Id);

            _dataProviderMock.Verify(dpm => dpm.GetFriendById(friendId), Times.Once);
        }

        [Theory]
        [InlineData(5)]
        public void ShouldRaisePropertyChangedEventForFriend(int friendId)
        {
            bool fired = _viewModel.IsPropertyChangedFired(() =>
                _viewModel.Load(friendId), nameof(_viewModel.Friend));

            Assert.True(fired);
        }

        [Theory]
        [InlineData(5)]
        public void ShouldDisableSaveCommandWhenFriendIsLoaded(int friendId)
        {
            _viewModel.Load(friendId);

            Assert.False(_viewModel.SaveCommand.CanExecute(null));
        }

        [Theory]
        [InlineData(7, "Nuevo nombre")]
        public void ShouldEnableSaveCommandWhenFriendIsChanged(int friendId, string updatedName)
        {
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);

            _viewModel.Friend.FirstName = updatedName;

            Assert.True(_viewModel.SaveCommand.CanExecute(null));
        }

        [Fact]
        public void ShouldDisableSaveCommandWithoutLoad()
        {
            Assert.False(_viewModel.SaveCommand.CanExecute(null));
        }

        //[Theory]
        //[InlineData(7, "Nuevo nombre")]
        //public void ShouldRaiseCanExecuteChangedForSaveCommandWhenFriendIsChanged(int friendId, string updatedName)
        //{
        //    _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
        //    _viewModel.Load(friendId);
        //    bool fired = false;
        //    _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
        //    _viewModel.Friend.FirstName = updatedName;

        //    Assert.True(fired);
        //}

        //[Theory]
        //[InlineData(7)]
        //public void ShouldRaiseCanExecuteChangedForSaveCommandWhenAfterLoad(int friendId)
        //{
        //    _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
        //    _viewModel.Load(friendId);
        //    bool fired = false;
        //    _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;

        //    Assert.True(fired);
        //}
    }
}
