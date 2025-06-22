namespace UserProfileManager.Views
{
    // This interface is the "contract" for our main form. The Presenter will
    // only interact with the Form through this interface, making the Presenter
    // completely independent of the WinForms technology.
    public interface IProfileView
    {
        // Properties to get/set data from UI controls
        string UserName { get; set; }
        string Email { get; set; }
        string SelectedSaveFormat { get; }

        // Method to populate UI elements
        void SetSaveFormats(IEnumerable<string> formats);

        // Method to show a message to the user
        void ShowMessage(string message, string title, bool isError = false);

        // Events that the View will raise in response to user actions
        event EventHandler LoadDataClicked;
        event EventHandler SaveClicked;
    }
}