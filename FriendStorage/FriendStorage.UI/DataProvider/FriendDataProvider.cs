using FriendStorage.DataAccess;
using FriendStorage.Model;
using System;

namespace FriendStorage.UI.DataProvider
{
    public class FriendDataProvider : IFriendDataProvider
    {
        private readonly Func<IDataService> _dataServiceFactory;

        public FriendDataProvider(Func<IDataService> dataServiceFactory)
        {
            _dataServiceFactory = dataServiceFactory;
        }

        public void DeleteFriend(int id)
        {
            using (var dataService = _dataServiceFactory())
                dataService.DeleteFriend(id);
        }

        public Friend GetFriendById(int id)
        {
            using (var dataService = _dataServiceFactory())
                return dataService.GetFriendById(id);
        }

        public void SaveFriend(Friend friend)
        {
            using (var dataService = _dataServiceFactory())
                dataService.SaveFriend(friend);
        }
    }
}
