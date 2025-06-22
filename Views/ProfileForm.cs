using UserProfileManager.Views; 

namespace UserProfileManager
{
    // The form is back to being fully independent. It implements the interface
    // but knows nothing about the Presenter.
    public partial class ProfileForm : Form, IProfileView
    {
        // The constructor is now parameterless again.
        public ProfileForm()
        {
            InitializeComponent();

            // Wire up the control events to the interface events.
            this.btnLoad.Click += (s, e) => LoadDataClicked?.Invoke(this, EventArgs.Empty);
            this.btnSave.Click += (s, e) => SaveClicked?.Invoke(this, EventArgs.Empty);
        }

        // --- IProfileView Implementation (This part does NOT change) ---

        public string UserName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        public string Email
        {
            get => txtEmail.Text;
            set => txtEmail.Text = value;
        }

        public string SelectedSaveFormat => cmbSaveFormat.SelectedItem?.ToString() ?? string.Empty;

        public void SetSaveFormats(IEnumerable<string> formats)
        {
            cmbSaveFormat.Items.Clear();
            cmbSaveFormat.Items.AddRange(formats.ToArray());
            if (cmbSaveFormat.Items.Count > 0)
            {
                cmbSaveFormat.SelectedIndex = 0;
            }
        }

        public void ShowMessage(string message, string title, bool isError = false)
        {
            var icon = isError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        public event EventHandler? LoadDataClicked;
        public event EventHandler? SaveClicked;
    }
}