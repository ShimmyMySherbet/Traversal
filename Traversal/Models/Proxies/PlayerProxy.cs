using SDG.Unturned;

namespace Traversal.Models.Proxies
{
    public class PlayerProxy : IModelProxy
    {
        public ulong PlayerID { get; set; }

        public byte PlayerSlot { get; set; }

        public SpawnProxyResult Result = SpawnProxyResult.Nil;

        public Player Player;

        public SteamPlayerID SteamID;
    }
}