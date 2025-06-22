using UserProfileManager.Models;

namespace UserProfileManager.Strategies
{
    // This is the heart of the Strategy Pattern. It defines a common interface
    // for all possible "saving algorithms" (JSON, XML, etc.).
    public interface ISavingStrategy
    {
        // A property to identify the strategy in the UI (e.g., in a dropdown).
        string StrategyName { get; }

        void Save(UserProfile profile, string filePath);
    }
}