using Autofac;
using FriendStorage.DataAccess;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Dialogs;
using FriendStorage.UI.View;
using FriendStorage.UI.ViewModel;
using Prism.Events;

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
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<FriendDataProvider>().AsImplementedInterfaces();
            builder.RegisterType<FriendEditViewModel>().AsImplementedInterfaces();
            builder.RegisterType<MessageDialogService>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
