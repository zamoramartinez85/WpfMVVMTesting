using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfMVVMTesting.UITests.Extensions
{
    public static class NotifyPropertyChangedExtensions
    {
        public static bool IsPropertyChangedFired(this INotifyPropertyChanged notifyPropertyChanged, Action action, string propertyName)
        {
            bool fired = false;

            notifyPropertyChanged.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    fired = true;
                }
            };

            action();

            return fired;
        }
    }
}
