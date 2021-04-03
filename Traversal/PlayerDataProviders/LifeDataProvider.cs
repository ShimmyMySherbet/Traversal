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
    public class LifeDataProvider : IPlayerDataProvider<PlayerLife>
    {
        public bool Load(PlayerLife instance, MySQLEntityClient database)
        {
            return false;
        }

        public bool Save(PlayerLife instance, MySQLEntityClient database)
        {
            return false;
        }
    }
}
