using Autofac;
using FriendStorage.DataAccess;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.View;
using FriendStorage.UI.ViewModel;

namespace FriendStorage.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationViewModel>().AsImplementedInterfaces();
            builder.RegisterType<NavigationDataProvider>().AsImplementedInterfaces();
            builder.RegisterType<FileDataService>().AsImplementedInterfaces();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            return builder.Build();
        }
    }
}
