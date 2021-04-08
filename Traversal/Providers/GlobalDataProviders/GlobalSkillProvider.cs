using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.GlobalDataProviders
{
    public class GlobalSkillProvider : IGlobalDataProvider<PlayerSkillData>
    {
        public const string TableName = "TraversalData_Skills_Global";

        public string Name => "Skills";

        public void CheckSchema(MySQLEntityClient database)
        {
            if (!database.TableExists(TableName))
            {
                database.CreateTable<GlobalSkillData>(TableName);
            }
        }

        public bool Load(ref PlayerSkillData data, MySQLEntityClient database, Scope scope)
        {
            GlobalSkillData global = database.QuerySingle<GlobalSkillData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                return false;
            }
            global.Load();


            if (scope["Skills.Skills"])
            {
                data.Data = global.Data;
            }

            if (scope["Skills.Experience"])
            {
                data.Experience = global.Experience;
            }

            if (scope["Skills.Reputation"])
            {
                data.Reputation = global.Reputation;
            }

            return true;
        }

        public bool Save(PlayerSkillData data, MySQLEntityClient database, Scope scope)
        {
            GlobalSkillData global = database.QuerySingle<GlobalSkillData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                global = new GlobalSkillData()
                {
                    PlayerID = data.PlayerID,
                    Slot = data.Slot
                };
            }
            global.Load();


            if (scope["Skills.Skills"])
            {
                global.Data = data.Data;
            }

            if (scope["Skills.Experience"])
            {
                global.Experience = data.Experience;
            }

            if (scope["Skills.Reputation"])
            {
                global.Reputation = data.Reputation;
            }

            global.Save();

            database.InsertUpdate(global, TableName);

            return true;
        }
    }
}
