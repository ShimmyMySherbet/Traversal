﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;

namespace Traversal.PlayerDataProviders
{
    public class SkillDataProvider : IPlayerDataProvider<PlayerSkills>
    {
        public void CheckSchema(MySQLEntityClient client)
        {
        }

        public bool Load(PlayerSkills instance, MySQLEntityClient database)
        {
            return false;
        }

        public bool Save(PlayerSkills instance, MySQLEntityClient database)
        {
            return false;
        }
    }
}
