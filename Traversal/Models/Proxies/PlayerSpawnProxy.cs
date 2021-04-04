using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;

namespace Traversal.Models.Proxies
{
    public class PlayerSpawnProxy
    {
        public const int MODE_SAVE = 0;
        public const int MODE_LOAD = 1;

        public int MODE = 0;

        public ulong PlayerID
        {
            get
            {
                if (MODE == MODE_SAVE)
                {
                    return player.channel.GetPlayerID();
                }
                else
                {
                    return playerID.steamID.m_SteamID;
                }
            }
        }

        public byte SlotID
        {
            get
            {
                if (MODE == MODE_SAVE)
                {
                    return player.channel.GetPlayerSlotID();
                }
                else
                {
                    return playerID.characterID;
                }
            }
        }

        public SteamPlayerID playerID;

        public Player player;

        public SpawnProxyResult Result = SpawnProxyResult.Nil;

    }
}
