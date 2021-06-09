using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfMVVMTesting.UI.Events;

namespace WpfMVVMTesting.UI.ViewModel
{
    public class NavigationItemViewModel
    {
        public int Id { get; private set;  }
        public string DisplayMember { get;  set; }            
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
