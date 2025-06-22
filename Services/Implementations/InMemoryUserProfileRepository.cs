using UserProfileManager.Models;

namespace UserProfileManager.Services.Implementations
{
    // This is a concrete implementation of our repository interface.
    // For this example, it provides hard-coded data to simulate fetching
    // from a database or a web service.
    public class InMemoryUserProfileRepository : IUserProfileRepository
    {
        public UserProfile GetUserProfile()
        {
            // In a real app, this would query a database.
            // Here, we just return a new dummy object.
            return new UserProfile
            {
                Name = "Ada Lovelace",
                Email = "ada.lovelace@example.com"
            };
        }
    }
}