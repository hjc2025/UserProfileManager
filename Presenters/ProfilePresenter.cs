using Microsoft.Extensions.Configuration;
using UserProfileManager.Models;
using UserProfileManager.Services;
using UserProfileManager.Strategies;
using UserProfileManager.Views;

namespace UserProfileManager.Presenters
{
    // The Presenter acts as the bridge between the Model and the View.
    // It contains all the application logic and state management for the view.
    public class ProfilePresenter
    {
        // Private fields for all the dependencies (abstractions, not concrete classes).
        private readonly IProfileView _view;
        private readonly IUserProfileRepository _repository;
        private readonly ISavingStrategyFactory _strategyFactory;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        private UserProfile? _currentUserProfile;

        // Constructor Injection: The DI container will automatically supply the required
        // instances when it creates the Presenter.
        public ProfilePresenter(
            IProfileView view,
            IUserProfileRepository repository,
            ISavingStrategyFactory strategyFactory,
            INotificationService notificationService,
            IConfiguration configuration)
        {
            // Store dependencies
            _view = view;
            _repository = repository;
            _strategyFactory = strategyFactory;
            _notificationService = notificationService;
            _configuration = configuration;

            // Subscribe to the view's events. This is the "P" listening to the "V".
            _view.LoadDataClicked += OnLoadDataClicked;
            _view.SaveClicked += OnSaveClicked;

            // Initialize the view state
            InitializeView();
        }

        // Method to set up the initial state of the view
        private void InitializeView()
        {
            var strategyNames = _strategyFactory.GetAvailableStrategyNames();
            _view.SetSaveFormats(strategyNames);
        }

        // Event handler for when the user wants to load data
        private void OnLoadDataClicked(object? sender, EventArgs e)
        {
            _currentUserProfile = _repository.GetUserProfile();

            // Update the view with data from the model
            _view.UserName = _currentUserProfile.Name;
            _view.Email = _currentUserProfile.Email;
        }

        // Event handler for when the user clicks the save button
        private void OnSaveClicked(object? sender, EventArgs e)
        {
            if (_currentUserProfile is null)
            {
                _view.ShowMessage("Please load user data first before saving.", "Warning", isError: true);
                return;
            }

            // 1. Get updated data from the view
            _currentUserProfile.Name = _view.UserName;
            _currentUserProfile.Email = _view.Email;

            // 2. Perform validation
            if (string.IsNullOrWhiteSpace(_currentUserProfile.Name))
            {
                _view.ShowMessage("User name cannot be empty.", "Validation Error", isError: true);
                return;
            }

            try
            {
                // 3. Use the factory to get the selected saving strategy
                var selectedStrategyName = _view.SelectedSaveFormat;
                ISavingStrategy strategy = _strategyFactory.GetStrategy(selectedStrategyName);

                // 4. Get save path from configuration
                string savePath = _configuration["Settings:DefaultSavePath"] ?? "default_profile";

                // 5. Execute the strategy
                strategy.Save(_currentUserProfile, savePath);

                // 6. Notify the user of success via the notification service
                _notificationService.ShowSuccess($"Profile saved successfully using {strategy.StrategyName} format.");
            }
            catch (Exception ex)
            {
                // In a real app, you would log this exception
                _view.ShowMessage($"An error occurred during save: {ex.Message}", "Save Error", isError: true);
            }
        }
    }
}