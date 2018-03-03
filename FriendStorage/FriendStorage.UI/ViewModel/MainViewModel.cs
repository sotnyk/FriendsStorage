using FriendStorage.DataAccess;
using FriendStorage.UI.DataProvider;
using System;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // TODO: fix this
        public NavigationViewModel NavigationViewModel { get; }
            = new NavigationViewModel(
                new NavigationDataProvider(
                    () => new FileDataService()));

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
