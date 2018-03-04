using FriendStorage.Model;
using FriendStorage.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UI.Wrappers
{
    public class FriendWrapper: ViewModelBase
    {
        private Friend _friend;

        public FriendWrapper(Friend friend)
        {
            _friend = friend;
        }

        public Friend Model { get { return _friend; } }
        public bool IsChanged { get; private set; }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        public int Id => _friend.Id;

        public string FirstName {
            get { return _friend.FirstName; }
            set { _friend.FirstName = value; }
        }

        public string LastName
        {
            get { return _friend.LastName; }
            set { _friend.LastName = value; }
        }

        public DateTime? Birthday
        {
            get { return _friend.Birthday; }
            set { _friend.Birthday = value; }
        }

        public bool IsDeveloper
        {
            get { return _friend.IsDeveloper; }
            set { _friend.IsDeveloper = value; }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != nameof(IsChanged))
                IsChanged = true;
        }
    }
}
