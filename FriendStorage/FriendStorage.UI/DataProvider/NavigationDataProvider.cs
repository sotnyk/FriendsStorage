using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendStorage.DataAccess;
using FriendStorage.Model;

namespace FriendStorage.UI.DataProvider
{
    class NavigationDataProvider : INavigationDataProvider
    {
        private Func<IDataService> _dataServiceFactory;

        public NavigationDataProvider(Func<IDataService> dataServiceFactory)
        {
            _dataServiceFactory = dataServiceFactory;
        }

        public IEnumerable<LookupItem> GetAllFriends()
        {
            using (var dataService = _dataServiceFactory())
                return dataService.GetAllFriends();
        }
    }
}
