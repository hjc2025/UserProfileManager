namespace UserProfileManager.Models
{
    // This is our core data model. It's a simple POCO (Plain Old C# Object).
    // It has no logic and only serves to hold the state of a user's profile.
    public class UserProfile
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}