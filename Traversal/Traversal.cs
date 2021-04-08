using System.Linq;
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

        public static Scope Scope;

        public static bool DoSync => Traversal.Instance.Configuration.Instance.EnableSyncing;

        public static Traversal Instance;




        public override void LoadPlugin()
        {
            base.LoadPlugin();
            Instance = this;
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

            MountConfig();

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

        public void MountConfig()
        {
            Scope = new Scope();
            foreach(var scope in Configuration.Instance.Syncs)
            {
                Scope.AddVariable(scope);
            }
        }



    }
}