using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;

namespace Traversal.PlayerDataProviders
{
    public class LifeDataProvider : IPlayerDataProvider<PlayerLife>
    {
        public const string TableName = "PlayerData_Life";

        public bool Load(PlayerLife instance, MySQLEntityClient database)
        {
            LifeData data = database.QuerySingle<LifeData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID);

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
            LifeData life = new LifeData()
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
                Water = instance.water
            };
            database.InsertUpdate(life, TableName);
            return true;
        }
    }
}