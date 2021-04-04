using HarmonyLib;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Core;
using Traversal.Models;

namespace Traversal
{
    public class Traversal : RocketPlugin<TraversalConfig>
    {
        public static int ServerID => 0;
        public Harmony HarmonyInstance;
        public MySQLEntityClient Client;

        public override void LoadPlugin()
        {
            base.LoadPlugin();
            Logger.Log("Loading Traversal...");
            HarmonyInstance = new Harmony(PatchManager.Harmony_ID);
            var c = Configuration.Instance;
            Client = new MySQLEntityClient(c.DatabaseAddress, c.DatabaseUsername, c.DatabasePassword, c.DatabaseName, c.DatabasePort, true);

            if (!Client.Connect())
            {
                Logger.LogError("Unable to connect to database! Double check your database settings in the config.");

                UnloadPlugin(Rocket.API.PluginState.Failure);
                return;
            }

            PatchManager.Patch(Client, HarmonyInstance);
        }

        public override void UnloadPlugin(PluginState state = PluginState.Unloaded)
        {
            base.UnloadPlugin(state);
            if (HarmonyInstance != null)
            {
                PatchManager.Unpatch(HarmonyInstance);
            }
            Client?.Disconnect();
        }
    }
}