using Prism.Commands;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.DataProvider.FriendDataProvider;
using WpfMVVMTesting.UI.Events;
using WpfMVVMTesting.UI.ViewModelInterface;
using WpfMVVMTesting.UI.Wrapper;

namespace WpfMVVMTesting.UI.ViewModel
{

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        #region Propiedades privadas
        private IFriendDataProvider _friendDataProvider;
        private IEventAggregator _eventAggregator;
        private FriendWrapper _friend;
        #endregion

        #region Propiedades públicas

        public ICommand SaveCommand { get; private set; }

        public FriendWrapper Friend
        {
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
            get
            {
                return _friend;
            }
        }
        #endregion

        #region Constructor
        public FriendEditViewModel(IFriendDataProvider friendDataProvider, IEventAggregator eventAggregator)
        {
            _friendDataProvider = friendDataProvider;
            _eventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand<object>(OnSaveExecute, OnSaveCanExecute);
        }
        #endregion

        #region Métodos públicos
        public void Load(int friendId)
        {
            Friend friend = _friendDataProvider.GetFriendById(friendId);

            Friend = new FriendWrapper(friend);

            Friend.PropertyChanged += Friend_PropertyChanged;

            ((DelegateCommand<object>)SaveCommand).RaiseCanExecuteChanged();
        }

        private void Friend_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ((DelegateCommand<object>)SaveCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Métodos privados
        private void OnSaveExecute(object obj)
        {
            _friendDataProvider.SaveFriend(Friend.Model);
            Friend.AcceptChanges();
            _eventAggregator.GetEvent<FriendSavedEvent>().Publish(Friend.Model);
        }

        private bool OnSaveCanExecute(object arg)
        {
            return Friend != null && Friend.IsChanged;
        }
        #endregion

    }
}
