using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;

namespace Traversal.PlayerDataProviders
{
    public class SkillDataProvider : IPlayerDataProvider<PlayerSkills>
    {
        public const string TableName = "PlayerData_Skills";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<SkillData>(TableName);
            }
        }

        public bool Load(PlayerSkills instance, MySQLEntityClient database)
        {
            SkillData data = database.QuerySingle<SkillData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID);

            if (data == null)
            {
                return false;
            }
            data.Load();

            instance.SetValue("_experience", data.Experience);
            instance.SetValue("_reputation", data.Reputation);
            instance.SetValue("_boost", (EPlayerBoost)data.Boost);

            foreach (var skill in data.Data.Skills)
            {
                instance.skills[skill.Category][skill.ID].level = skill.Value;
            }

            instance.InvokeTarget("applyDefaultSkills");

            instance.SetValue("wasLoadCalled", true);

            return true;
        }

        public bool Save(PlayerSkills instance, MySQLEntityClient database)
        {
            SkillData data = new SkillData()
            {
                PlayerID = instance.channel.GetPlayerID(),
                Slot = instance.channel.GetPlayerSlotID(),
                ServerID = Traversal.ServerID,
                Boost = (byte)instance.boost,
                Experience = instance.experience,
                Reputation = instance.reputation
            };
            data.Data = new InnerSkillData();

            for (byte cat = 0; cat < instance.skills.Length; cat++)
                for (byte id = 0; id < instance.skills[cat].Length; id++)
                {
                    data.Data.Skills.Add(new PlayerDataSkill() { Category = cat, ID = id, Value = instance.skills[cat][id].level });
                }

            data.Save();

            database.InsertUpdate(data, TableName);
            return true;
        }
    }
}