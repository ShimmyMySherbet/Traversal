using ShimmyMySherbet.MySQL.EF.Models;

namespace Traversal.Models
{
    public abstract class PlayerDataModel
    {
        [SQLPrimaryKey]
        public ulong PlayerID;

        public byte Slot;

        public int ServerID;

        public string Map;
    }
}