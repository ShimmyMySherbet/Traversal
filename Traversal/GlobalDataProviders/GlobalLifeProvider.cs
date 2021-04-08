using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.GlobalDataProviders
{
    public class GlobalLifeProvider : IGlobalDataProvider<PlayerLifeData>
    {
        public const string TableName = "PlayerData_Life_Global";
        public string Name => "Life";

        public void CheckSchema(MySQLEntityClient database)
        {
            if (!database.TableExists(TableName))
            {
                database.CreateTable<GlobalLifeData>(TableName);
            }
        }

        public bool Load(ref PlayerLifeData data, MySQLEntityClient database, Scope scope)
        {
            GlobalLifeData global = database.QuerySingle<GlobalLifeData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                return false;
            }

            if (scope["Life.Health"])
                data.Health = global.Health;

            if (scope["Life.Food"])
                data.Food = global.Food;

            if (scope["Life.Water"])
                data.Water = global.Water;

            if (scope["Life.Virus"])
                data.Virus = global.Virus;

            if (scope["Life.Stamina"])
                data.Stamina = global.Stamina;

            if (scope["Life.Oxygen"])
                data.Oxygen = global.Oxygen;

            if (scope["Life.Bleeding"])
                data.Bleeding = global.Bleeding;

            if (scope["Life.Broken"])
                data.Broken = global.Broken;

            if (scope["Life.Temperature"])
                data.Temperature = global.Temperature;

            return true;
        }

        public bool Save(PlayerLifeData data, MySQLEntityClient database, Scope scope)
        {
        }
    }
}
