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

        public void LoadDefaults()
        {
        }
    }
}