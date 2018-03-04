using FriendStorage.Model;
using FriendStorage.UI.Command;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Wrappers;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        void Load(int friendId);
        FriendWrapper Friend { get; }
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private IFriendDataProvider _dataProvider;

        public FriendEditViewModel(IFriendDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute(object arg)
        {
            return Friend?.IsChanged ?? false;
        }

        private void OnSaveExecute(object obj)
        {
            //return Friend.IsChanged;
        }

        public ICommand SaveCommand { get; private set; }

        public FriendWrapper Friend { get; private set; }

        public void Load(int friendId)
        {
            var friend = _dataProvider.GetFriendById(friendId);
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += Friend_PropertyChanged;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void Friend_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
