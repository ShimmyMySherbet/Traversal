using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.PlayerDataProviders
{
    public class LifeDataProvider : IPlayerDataProvider<PlayerLife>
    {
        public const string TableName = "TraversalData_Life";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<PlayerLifeData>(TableName);
            }
        }

        public bool Load(PlayerLife instance, MySQLEntityClient database)
        {
            PlayerLifeData data = database.QuerySingle<PlayerLifeData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2 AND Map=@3", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID, Provider.map);

            if (data == null)
            {
                return false;
            }

            instance.SetValue("_health", data.Health);
            instance.SetValue("_food", data.Food);
            instance.SetValue("_water", data.Water);
            instance.SetValue("_virus", data.Virus);
            instance.SetValue("_stamina", data.Stamina);
            instance.SetValue("_oxygen", data.Oxygen);
            instance.SetValue("_isBleeding", data.Bleeding);
            instance.SetValue("_isBroken", data.Broken);
            instance.SetValue("_temperature", (EPlayerTemperature)data.Temperature);

            instance.SetValue("wasLoadCalled", true);

            return true;
        }

        public bool Save(PlayerLife instance, MySQLEntityClient database)
        {
            PlayerLifeData life = new PlayerLifeData()
            {
                PlayerID = instance.channel.GetPlayerID(),
                ServerID = Traversal.ServerID,
                Slot = instance.channel.GetPlayerSlotID(),
                Bleeding = instance.isBleeding,
                Broken = instance.isBroken,
                Food = instance.food,
                Health = instance.health,
                Oxygen = instance.oxygen,
                Stamina = instance.stamina,
                Temperature = (byte)instance.temperature,
                Virus = instance.virus,
                Water = instance.water,
                Map = Provider.map
            };
            database.InsertUpdate(life, TableName);
            return true;
        }
    }
}