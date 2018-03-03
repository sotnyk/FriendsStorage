using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FriendStorage.UITests.ViewModel
{
    public class MainViewModelTests
    {
        [Fact]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            var navigationViewModel = new NavigationViewModelMock();
            var viewModel = new MainViewModel(navigationViewModel);
            viewModel.Load();
            Assert.True(navigationViewModel.LoadHasBeenCalled);
        }
    }

    class NavigationViewModelMock : INavigationViewModel
    {
        public bool LoadHasBeenCalled { get; set; }
        public void Load()
        {
            LoadHasBeenCalled = true;
        }
    }
}
