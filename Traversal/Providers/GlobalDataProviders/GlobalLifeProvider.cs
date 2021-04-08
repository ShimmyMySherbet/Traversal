using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.GlobalDataProviders
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
            GlobalLifeData global = database.QuerySingle<GlobalLifeData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                global = new GlobalLifeData()
                {
                    PlayerID = data.PlayerID,
                    Slot = data.Slot
                };
            }

            if (scope["Life.Health"])
                global.Health = data.Health;

            if (scope["Life.Food"])
                global.Food = data.Food;

            if (scope["Life.Water"])
                global.Water = data.Water;

            if (scope["Life.Virus"])
                global.Virus = data.Virus;

            if (scope["Life.Stamina"])
                global.Stamina = data.Stamina;

            if (scope["Life.Oxygen"])
                global.Oxygen = data.Oxygen;

            if (scope["Life.Bleeding"])
                global.Bleeding = data.Bleeding;

            if (scope["Life.Broken"])
                global.Broken = data.Broken;

            if (scope["Life.Temperature"])
                global.Temperature = data.Temperature;

            return true;
        }
    }
}