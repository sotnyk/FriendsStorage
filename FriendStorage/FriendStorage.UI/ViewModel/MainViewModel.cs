using System;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationViewModel { get; } = new NavigationViewModel();

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
