using FriendStorage.UI.ViewModel;
using Moq;
using Xunit;

namespace FriendStorage.UITests.ViewModel
{
    public class MainViewModelTests
    {

        Mock<INavigationViewModel> ConstructMock()
        {
            var nvmMock = new Mock<INavigationViewModel>();
            return nvmMock;
        }

        [Fact]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            var navigationViewModelMock = ConstructMock();
            var viewModel = new MainViewModel(navigationViewModelMock.Object);
            viewModel.Load();
            navigationViewModelMock.Verify(vm => vm.Load(), Times.Once);
        }
    }
}
