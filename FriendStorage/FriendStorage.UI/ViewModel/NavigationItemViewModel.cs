using FriendStorage.UI.Command;
using FriendStorage.UI.Events;
using Prism.Events;
using System.Windows.Input;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationItemViewModel
    {
        public int Id { get; }
        public string DisplayMember { get; }

        private IEventAggregator _eventAggregator;

        public NavigationItemViewModel(int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;
            OpenFriendEditViewCommand = new DelegateCommand(OnFriendEditViewExecute);
        }

        public ICommand OpenFriendEditViewCommand { get; } 

        private void OnFriendEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Publish(Id);
        }
    }
}
