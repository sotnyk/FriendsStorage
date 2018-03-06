namespace FriendStorage.UI.Dialogs
{
    public enum MessageDialogResult
    {
        No,
        Yes
    }

    public interface IMessageDialogService
    {
        MessageDialogResult ShowYesNoDialog(string title, string message);
    }
}
