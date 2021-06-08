using System;
using WpfMVVMTesting.UI.ViewModelInterface;

namespace WpfMVVMTesting.UI.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
        public INavigationViewModel NavigationViewModel { get; private set; }

        public MainViewModel(INavigationViewModel navigationViewModel)
        {
            this.NavigationViewModel = navigationViewModel;
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
