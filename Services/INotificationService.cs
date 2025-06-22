namespace UserProfileManager.Services
{
    // The contract for a service that shows non-critical notifications to the user.
    // This is a great example of a cross-cutting concern abstracted away.
    public interface INotificationService
    {
        void ShowSuccess(string message);
        void ShowInfo(string message);
    }
}