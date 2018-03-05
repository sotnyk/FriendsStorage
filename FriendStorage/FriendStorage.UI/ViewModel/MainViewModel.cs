using FriendStorage.DataAccess;
using FriendStorage.UI.Command;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Events;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Func<IFriendEditViewModel> _friendEditViewModelFactory;
        private readonly IEventAggregator _eventAggregator;

        public IFriendEditViewModel SelectedFriendEditViewModel { get; set; }
        public INavigationViewModel NavigationViewModel { get; private set; }
        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; }
            = new ObservableCollection<IFriendEditViewModel>();

        public ICommand CloseFriendTabCommand { get; private set; }
        public ICommand AddFriendCommand { get; private set; }

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendEditViewModel> friendEditViewModelFactory,
            IEventAggregator eventAggregator)
        {
            NavigationViewModel = navigationViewModel;
            _friendEditViewModelFactory = friendEditViewModelFactory;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<OpenFriendEditViewEvent>().Subscribe(OnOpenFriendEditView);
            CloseFriendTabCommand = new DelegateCommand(OnCloseFriendTabExecute);
            AddFriendCommand = new DelegateCommand(OnAddFriendExecute);
        }

        private void OnCloseFriendTabExecute(object obj)
        {
            var friendEditVm = (IFriendEditViewModel)obj;
            FriendEditViewModels.Remove(friendEditVm);
        }

        private void OnOpenFriendEditView(int friendId)
        {
            var friendEditVm = FriendEditViewModels.FirstOrDefault(f => f.Friend.Id == friendId) ??
                CreateAndLoadFriendEditViewModel(friendId);
            SelectedFriendEditViewModel = friendEditVm;
        }

        private void OnAddFriendExecute(object obj)
        {
            SelectedFriendEditViewModel = CreateAndLoadFriendEditViewModel(null);
        }

        private IFriendEditViewModel CreateAndLoadFriendEditViewModel(int? friendId)
        {
            var friendEditVm = _friendEditViewModelFactory();
            FriendEditViewModels.Add(friendEditVm);
            friendEditVm.Load(friendId);
            return friendEditVm;
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
