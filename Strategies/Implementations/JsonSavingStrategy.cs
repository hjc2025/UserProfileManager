using System.IO;
using System.Text.Json;
using UserProfileManager.Models;

namespace UserProfileManager.Strategies.Implementations
{
    public class JsonSavingStrategy : ISavingStrategy
    {
        public string StrategyName => "JSON";

        public void Save(UserProfile profile, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(profile, options);

            // Append the correct extension to the base file path
            File.WriteAllText(filePath + ".json", jsonString);
        }
    }
}