using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Events;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FriendStorage.UI.ViewModel
{
    public interface INavigationViewModel
    {
        void Load();
    }

    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private INavigationDataProvider _dataProvider;

        public IEventAggregator _eventAggregator;

        public NavigationViewModel(INavigationDataProvider dataProvider,
            IEventAggregator eventAggregator)
        {
            _dataProvider = dataProvider;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<FriendSavedEvent>().Subscribe(OnFriendSaved);
        }

        private void OnFriendSaved(Friend friend)
        {
            var displayMember = $"{friend.FirstName} {friend.LastName}";
            var navigationItem = Friends.SingleOrDefault(n => n.Id == friend.Id);
            if (navigationItem != null)
            {
                navigationItem.DisplayMember = displayMember;
            }
            else
            {
                navigationItem = new NavigationItemViewModel(friend.Id, displayMember, _eventAggregator);
                Friends.Add(navigationItem);
            }
        }

        public void Load()
        {
            Friends.Clear();
            foreach(var friend in _dataProvider.GetAllFriends())
            {
                Friends.Add(new NavigationItemViewModel(friend.Id, friend.DisplayMember, _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Friends { get; } = new ObservableCollection<NavigationItemViewModel>();
    }
}
