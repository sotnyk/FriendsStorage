using System;
using System.ComponentModel;

namespace FriendStorage.UITests.Extensions
{
    public static class NotifiedPropertyChangedExtensions
    {
        public static bool IsPropertyChangedFired(this INotifyPropertyChanged notifyPropertyChanged,
            Action action, string propertyName)
        {
            var fired = false;
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
