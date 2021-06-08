using System;

namespace WpfMVVMTesting.UI.ViewModel
{
  public class MainViewModel : ViewModelBase
  {

        public MainViewModel()
        {
            //this.NavigationViewModel = new NavigationViewModel();
        }

        public NavigationViewModel NavigationViewModel { get; private set; }

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
