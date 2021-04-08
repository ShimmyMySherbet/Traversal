using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;

namespace Traversal.Models
{
    public class TraversalConfig : IRocketPluginConfiguration
    {
        public string DatabaseAddress = "127.0.0.1";
        public string DatabaseUsername = "Unturned";
        public string DatabasePassword = "SuperSecretPassword";
        public string DatabaseName = "UnturnedServer";
        public ushort DatabasePort = 3306;
        public int TraversalServerID = 0;

        public bool EnableSyncing = false;
        [XmlArrayItem(elementName: "Sync")]
        public List<string> Syncs = new List<string>();

        public void LoadDefaults()
        {
            Syncs = new List<string>() { "Life.*", "Quests.*", "Clothing.*", "Inventory.*", "Skills.*" };
        }
    }
}