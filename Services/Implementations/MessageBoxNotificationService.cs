using System.Windows.Forms;

namespace UserProfileManager.Services.Implementations
{
    public class MessageBoxNotificationService : INotificationService
    {
        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}