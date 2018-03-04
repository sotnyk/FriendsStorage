using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.ViewModel;
using Moq;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FriendStorage.UITests.ViewModel
{
    public class NavigationViewModelTests
    {
        INavigationDataProvider ConstructNavigationDataProviderMock()
        {
            var navigationDataProviderMock = new Mock<INavigationDataProvider>();
            navigationDataProviderMock.Setup(dp => dp.GetAllFriends())
                .Returns(new List<LookupItem>
                    {
                        new LookupItem { Id = 1, DisplayMember = "Julia" },
                        new LookupItem { Id = 2, DisplayMember = "Thomas" },
                    });
            return navigationDataProviderMock.Object;
        }

        Mock<IEventAggregator> ConstructIAMock()
        {
            return new Mock<IEventAggregator>();
        }

        [Fact]
        public void ShouldLoadFriends()
        {
            var viewModel = new NavigationViewModel(ConstructNavigationDataProviderMock(), ConstructIAMock().Object);
            viewModel.Load();
            Assert.Equal(2, viewModel.Friends.Count);

            var friend = viewModel.Friends.SingleOrDefault(f => f.Id == 1);
            Assert.NotNull(friend);
            Assert.Equal("Julia", friend.DisplayMember);
        }

        [Fact]
        public void ShouldLoadFriendsOnlyOnce()
        {
            var viewModel = new NavigationViewModel(ConstructNavigationDataProviderMock(), ConstructIAMock().Object);
            viewModel.Load();
            viewModel.Load();
            Assert.Equal(2, viewModel.Friends.Count);

        }
    }
}
