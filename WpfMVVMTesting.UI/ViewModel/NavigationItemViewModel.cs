using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfMVVMTesting.UI.Events;

namespace WpfMVVMTesting.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        public int Id { get; private set;  }
        private string _displayMember;

        public string DisplayMember
        {
            get { return _displayMember; }
            set 
            { 
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFriendEditViewCommand { get; private set; }

        private readonly IEventAggregator _eventAggregator;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFriendEditViewCommand = new DelegateCommand(OnFriendEditViewExecute);
            _eventAggregator = eventAggregator;
        }

        private void OnFriendEditViewExecute()
        {
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Publish(Id);
        }
    }
}
