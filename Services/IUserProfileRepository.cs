using UserProfileManager.Models;

namespace UserProfileManager.Services
{
    // The contract for any class that can provide user profile data.
    // This decouples our Presenter from the actual data source (e.g., database, API, file).
    public interface IUserProfileRepository
    {
        UserProfile GetUserProfile();
    }
}