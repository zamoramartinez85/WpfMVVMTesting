﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using WpfMVVMTesting.Models;
using WpfMVVMTesting.UI.ViewModel;

namespace WpfMVVMTesting.UI.Wrapper
{
    public class FriendWrapper : ViewModelBase
    {
        private Friend _friend;
        private bool _isChanged;

        public Friend Model
        {
            get { return _friend; }
        }
        public bool IsChanged
        {
            get { return _isChanged; }
            private set 
            { 
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        public int Id { 
            get { return _friend.Id; } 
        }

        public string FirstName 
        { 
            get { return _friend.FirstName; }
            set
            {
                _friend.FirstName = value;
                OnPropertyChanged();
            } 
        }

        public string LastName
        {
            get { return _friend.LastName; }
            set
            {
                _friend.LastName = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Birthday
        {
            get { return _friend.Birthday; }
            set
            {
                _friend.Birthday = value;
                OnPropertyChanged();
            }
        }

        public bool IsDeveloper
        {
            get { return _friend.IsDeveloper; }
            set
            {
                _friend.IsDeveloper = value;
                OnPropertyChanged();
            }
        }

        public FriendWrapper(Friend friend)
        {
            _friend = friend;
        }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
        }


    }
}
