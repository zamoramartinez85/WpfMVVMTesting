using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.ViewModel;
using WpfMVVMTesting.UI.ViewModelInterface;
using Xunit;

namespace WpfMVVMTesting.UITests.ViewModel
{
    public class MainViewModelTests
    {
        private readonly INavigationViewModel _navigationViewModel;

        private readonly Mock<INavigationViewModel> _navigationViewModelMock;

        public MainViewModelTests()
        {
            _navigationViewModelMock = new Mock<INavigationViewModel>();
            _navigationViewModel = _navigationViewModelMock.Object;
        }

        [Fact]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            //Arrange
            MainViewModel mainViewModel = new MainViewModel(_navigationViewModel);

            //Act
            mainViewModel.Load();

            //Assert
            _navigationViewModelMock.Verify(vm => vm.Load(), Times.Once);
        }
    }
}
