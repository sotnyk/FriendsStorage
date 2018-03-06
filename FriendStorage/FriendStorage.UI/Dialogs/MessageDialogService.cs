using System.Windows;

namespace FriendStorage.UI.Dialogs
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowYesNoDialog(string title, string message)
        {
            if (MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                return MessageDialogResult.Yes;
            else
                return MessageDialogResult.No;
        }
    }
}
