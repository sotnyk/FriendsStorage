using System;
using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Events;
using FriendStorage.UI.ViewModel;
using Moq;
using Prism.Events;
using Xunit;
using FriendStorage.UITests.Extensions;
using FriendStorage.UI.Wrappers;

namespace FriendStorage.UITests.ViewModel
{
    public class MainViewModelTests
    {
        private readonly Mock<INavigationViewModel> _navigationViewModelMock;
        private readonly OpenFriendEditViewEvent _openFriendEditViewEvent;
        private readonly Mock<IEventAggregator> _eventAggregatorMock;
        private readonly MainViewModel _viewModel;
        private readonly List<Mock<IFriendEditViewModel>> _friendEditViewModelMocks
            = new List<Mock<IFriendEditViewModel>>();

        public MainViewModelTests()
        {
            _navigationViewModelMock = new Mock<INavigationViewModel>();
            _openFriendEditViewEvent = new OpenFriendEditViewEvent();

            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock
                .Setup(ea => ea.GetEvent<OpenFriendEditViewEvent>())
                .Returns(_openFriendEditViewEvent);

            _viewModel = new MainViewModel(_navigationViewModelMock.Object,
                CreateFriendViewModel, _eventAggregatorMock.Object);

        }

        private IFriendEditViewModel CreateFriendViewModel()
        {
            var friendEditViewModelMock = new Mock<IFriendEditViewModel>();
            friendEditViewModelMock.Setup(vm => vm.Load(It.IsAny<int>()))
                .Callback<int?>(friendId =>
                {
                    friendEditViewModelMock.Setup(vm => vm.Friend)
                        .Returns(new FriendWrapper(new Friend { Id = friendId.Value }));
                });
            _friendEditViewModelMocks.Add(friendEditViewModelMock);
            return friendEditViewModelMock.Object;
        }

        [Fact]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            _viewModel.Load();
            _navigationViewModelMock.Verify(vm => vm.Load(), Times.Once);
        }

        [Fact]
        public void ShouldAddFriendEditViewModelAndLoadAndSelectIt()
        {
            const int friendId = 7;
            _openFriendEditViewEvent.Publish(friendId);

            Assert.Single(_viewModel.FriendEditViewModels);

            var friendEditVm = _viewModel.FriendEditViewModels.First();
            Assert.Equal(friendEditVm, _viewModel.SelectedFriendEditViewModel);

            _friendEditViewModelMocks.First().Verify(vm => vm.Load(friendId), Times.Once);
        }

        [Fact]
        public void ShouldAddFriendEditViewModelAndLoadItWithNullAndSelectIt()
        {
            _viewModel.AddFriendCommand.Execute(null);

            Assert.Single(_viewModel.FriendEditViewModels);

            var friendEditVm = _viewModel.FriendEditViewModels.First();
            Assert.Equal(friendEditVm, _viewModel.SelectedFriendEditViewModel);

            _friendEditViewModelMocks.First().Verify(vm => vm.Load(null), Times.Once);
        }

        [Fact]
        public void ShouldAddFriendEditViewModelOnlyOnce()
        {
            _openFriendEditViewEvent.Publish(5);
            _openFriendEditViewEvent.Publish(5);
            _openFriendEditViewEvent.Publish(6);
            _openFriendEditViewEvent.Publish(7);
            _openFriendEditViewEvent.Publish(7);

            Assert.Equal(3, _viewModel.FriendEditViewModels.Count);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForSelectedFriendEditViewModel()
        {
            var friendEditVMMock = new Mock<IFriendEditViewModel>();
            var fired = _viewModel.IsPropertyChangedFired(() =>
                {
                    _viewModel.SelectedFriendEditViewModel = friendEditVMMock.Object;
                }, 
                nameof(_viewModel.SelectedFriendEditViewModel));            

            Assert.True(fired);
        }

        [Fact]
        public void ShouldRemoveFriendEditViewModelOnCloseFriendTabCommand()
        {
            _openFriendEditViewEvent.Publish(7);
            var friendEditVm = _viewModel.SelectedFriendEditViewModel;
            _viewModel.CloseFriendTabCommand.Execute(friendEditVm);
            Assert.Empty(_viewModel.FriendEditViewModels);
        }
    }
}
