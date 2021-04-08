using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;

namespace Traversal.Commands
{
    public class TraversalReloadCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Console;

        public string Name => "TraversalReload";

        public string Help => "Reloads Traversal's sync settings";

        public string Syntax => Name;

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "Traversal.Reload" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            Traversal.Instance.Configuration.Load();
            Traversal.Instance.MountConfig();

            UnturnedChat.Say(caller, "Syncs reloaded.");
        }
    }
}