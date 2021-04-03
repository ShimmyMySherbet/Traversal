using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;

namespace Traversal.PlayerDataProviders
{
    public class InventoryDataProvider : IPlayerDataProvider<PlayerInventory>
    {
        public bool Load(PlayerInventory instance, MySQLEntityClient database)
        {
            return false;
        }

        public bool Save(PlayerInventory instance, MySQLEntityClient database)
        {
            return false;
        }
    }
}
