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
    public class PlayerDataProvider : IPlayerDataProvider<Player>
    {
        public bool Load(Player instance, MySQLEntityClient database)
        {
            return false;
        }

        public bool Save(Player instance, MySQLEntityClient database)
        {
            return false;
        }
    }
}
