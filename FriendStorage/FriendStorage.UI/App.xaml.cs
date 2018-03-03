using FriendStorage.DataAccess;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.View;
using FriendStorage.UI.ViewModel;
using System.Windows;

namespace FriendStorage.UI
{
  public partial class App : Application
  {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow(
                new MainViewModel(
                    new NavigationViewModel(
                        new NavigationDataProvider(
                        () => new FileDataService()))));
            mainWindow.Show();
        }
    }
}
