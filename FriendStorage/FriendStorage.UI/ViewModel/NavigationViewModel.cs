using FriendStorage.DataAccess;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using System;
using System.Collections.ObjectModel;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private INavigationDataProvider _dataProvider;

        public NavigationViewModel(INavigationDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Load()
        {
            Friends.Clear();
            foreach(var friend in _dataProvider.GetAllFriends())
            {
                Friends.Add(friend);
            }
        }

        public ObservableCollection<LookupItem> Friends { get; } = new ObservableCollection<LookupItem>();
    }
}
