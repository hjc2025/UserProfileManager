using System.IO;
using System.Xml.Serialization;
using UserProfileManager.Models;

namespace UserProfileManager.Strategies.Implementations
{
    public class XmlSavingStrategy : ISavingStrategy
    {
        public string StrategyName => "XML";

        public void Save(UserProfile profile, string filePath)
        {
            var serializer = new XmlSerializer(typeof(UserProfile));

            // Append the correct extension to the base file path
            using (var writer = new StreamWriter(filePath + ".xml"))
            {
                serializer.Serialize(writer, profile);
            }
        }
    }
}