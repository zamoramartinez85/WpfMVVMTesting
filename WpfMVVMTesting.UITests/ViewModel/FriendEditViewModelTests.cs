using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.UI.DataProvider.FriendDataProvider;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModel;
using WpfMVVMTesting.UITests.Extensions;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class FriendEditViewModelTests
    {

        private Mock<IFriendDataProvider> _dataProviderMock;
        private Mock<FriendSavedEvent> _friendSavedEventMock;
        private Mock<FriendDeletedEvent> _friendDeletedEventMock;
        private Mock<IEventAggregator> _eventAggregatorMock;
        private FriendEditViewModel _viewModel;

        public FriendEditViewModelTests()
        {
            _friendSavedEventMock = new Mock<FriendSavedEvent>();
            _friendDeletedEventMock = new Mock<FriendDeletedEvent>();

            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(eam => eam.GetEvent<FriendSavedEvent>()).Returns(_friendSavedEventMock.Object);
            _eventAggregatorMock.Setup(eam => eam.GetEvent<FriendDeletedEvent>()).Returns(_friendDeletedEventMock.Object);

            _dataProviderMock = new Mock<IFriendDataProvider>();

            _viewModel = new FriendEditViewModel(_dataProviderMock.Object, _eventAggregatorMock.Object);
        }

        [Theory]
        [InlineData(5)]
        public void ShouldLoadFriend(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);

            //Assert
            Assert.NotNull(_viewModel.Friend);
            Assert.Equal(friendId, _viewModel.Friend.Id);

            _dataProviderMock.Verify(dpm => dpm.GetFriendById(friendId), Times.Once);
        }

        [Theory]
        [InlineData(5)]
        public void ShouldRaisePropertyChangedEventForFriend(int friendId)
        {
            //Arrange
            bool fired = _viewModel.IsPropertyChangedFired(() =>
                _viewModel.Load(friendId), nameof(_viewModel.Friend));

            //Assert
            Assert.True(fired);
        }

        [Theory]
        [InlineData(5)]
        public void ShouldDisableSaveCommandWhenFriendIsLoaded(int friendId)
        {
            //Arrange
            _viewModel.Load(friendId);

            //Assert
            Assert.False(_viewModel.SaveCommand.CanExecute(null));
        }

        [Theory]
        [InlineData(7, "Nuevo nombre")]
        public void ShouldEnableSaveCommandWhenFriendIsChanged(int friendId, string updatedName)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);

            _viewModel.Friend.FirstName = updatedName;

            //Assert
            Assert.True(_viewModel.SaveCommand.CanExecute(null));
        }

        [Fact]
        public void ShouldDisableSaveCommandWithoutLoad()
        {
            //Assert
            Assert.False(_viewModel.SaveCommand.CanExecute(null));
        }

        [Theory]
        [InlineData(7, "Nuevo nombre")]
        public void ShouldRaiseCanExecuteChangedForSaveCommandWhenFriendIsChanged(int friendId, string updatedName)
        {
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);
            bool fired = false;
            _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Friend.FirstName = updatedName;

            Assert.True(fired);
        }

        [Theory]
        [InlineData(7)]
        public void ShouldRaiseCanExecuteChangedForSaveCommandWhenAfterLoad(int friendId)
        {
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            bool fired = false;
            _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Load(friendId);

            Assert.True(fired);
        }

        [Theory]
        [InlineData(4)]
        public void ShouldPublishFriendsSavedEventWhenSaveCommandsIsExecuted(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);
            _viewModel.Friend.FirstName = "Manuel";

            //Act
            _viewModel.SaveCommand.Execute(null);

            //Assert
            _friendSavedEventMock.Verify(e => e.Publish(_viewModel.Friend.Model), Times.Once);

        }

        [Theory]
        [InlineData(7)]
        public void ShouldCallSaveMethodOfDataProviderWhenSaveCommandIsExecuted(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);
            _viewModel.Friend.FirstName = "Manuel";

            //Act
            _viewModel.SaveCommand.Execute(null);
            
            //Assert
            _dataProviderMock.Verify(dpm => dpm.SaveFriend(_viewModel.Friend.Model), Times.Once);
        }

        [Theory]
        [InlineData(7)]
        public void ShouldAcceptChangesWhenSaveCommandIsExecuted(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);
            _viewModel.Friend.FirstName = "Manuel";

            //Act
            _viewModel.SaveCommand.Execute(null);

            //Assert
            Assert.False(_viewModel.Friend.IsChanged);
        }

        [Fact]
        public void ShouldCreateNewFriendWhenNullIsPassedToLoadMethod()
        {
            //Act
            _viewModel.Load(null);

            //Assert
            Assert.NotNull(_viewModel.Friend);
            Assert.Equal(0, _viewModel.Friend.Id);
            Assert.Null(_viewModel.Friend.FirstName);
            Assert.Null(_viewModel.Friend.LastName);
            Assert.Null(_viewModel.Friend.Birthday);
            Assert.False(_viewModel.Friend.IsDeveloper);

            _dataProviderMock.Verify(dpm => dpm.GetFriendById(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData(7)]
        public void ShouldEnableDeleteCommandForExistingFriend(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);

            //Assert
            Assert.True(_viewModel.DeleteCommand.CanExecute(null));
        }

        [Fact]        
        public void ShouldDisableDeleteCommandForExistingFriend()
        {
            //Arrange
            _viewModel.Load(null);

            //Assert
            Assert.False(_viewModel.DeleteCommand.CanExecute(null));
        }

        [Fact]
        public void ShouldDisableDeleteCommandWithoutLoad()
        {           
            //Assert
            Assert.False(_viewModel.DeleteCommand.CanExecute(null));
        }

        [Theory]
        [InlineData(7, "Nuevo nombre")]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandWhenAcceptingChanges(int friendId, string updatedName)
        {
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            _viewModel.Load(friendId);
            bool fired = false;
            _viewModel.Friend.FirstName = updatedName;
            _viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Friend.AcceptChanges();

            Assert.True(fired);
        }

        [Theory]
        [InlineData(7)]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandWhenAfterLoad(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            bool fired = false;

            //Act
            _viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Load(friendId);

            //Assert
            Assert.True(fired);
        }

        [Theory]
        [InlineData(7)]
        public void ShouldCallDeleteFriendWhenDeleteCommandsIsExecuted(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });
            
            //Act
            _viewModel.Load(friendId);
            _viewModel.DeleteCommand.Execute(null);

            //Assert
            _dataProviderMock.Verify(dpm => dpm.DeleteFriend(friendId), Times.Once);
        }

        [Theory]
        [InlineData(7)]
        public void ShouldPublishFriendDeletedEventWhenDeleteCommandsIsExecuted(int friendId)
        {
            //Arrange
            _dataProviderMock.Setup(dpm => dpm.GetFriendById(friendId)).Returns(new Models.Friend() { Id = friendId, FirstName = "Pepe" });

            //Act
            _viewModel.Load(friendId);
            _viewModel.DeleteCommand.Execute(null);

            //Assert
            _friendDeletedEventMock.Verify(e => e.Publish(friendId), Times.Once);
        }
    }
}
