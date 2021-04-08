using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.GlobalDataProviders
{
    public class GlobalQuestProvider : IGlobalDataProvider<PlayerQuestData>
    {
        public const string TableName = "TraversalData_Quests_Global";

        public string Name => "Quests";

        public void CheckSchema(MySQLEntityClient database)
        {
            if (!database.TableExists(TableName))
            {
                database.CreateTable<GlobalQuestData>(TableName);
            }
        }

        public bool Load(ref PlayerQuestData data, MySQLEntityClient database, Scope scope)
        {
            GlobalQuestData global = database.QuerySingle<GlobalQuestData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                return false;
            }
            global.Load();

            if (scope["Quests.Group"])
            {
                data.GroupID = global.GroupID;
                data.GroupRank = global.GroupRank;
                data.IsMainGroup = global.IsMainGroup;
            }

            if (scope["Quests.Flags"])
            {
                data.Flags = global.Flags;
            }

            if (scope["Quests.RadioFrequency"])
            {
                data.RadioFrequency = global.RadioFrequency;
            }

            if (scope["Quests.Marker"])
            {
                data.MarkerX = global.MarkerX;
                data.MarkerY = global.MarkerY;
                data.MarkerZ = global.MarkerZ;
            }

            if (scope["Quests.Quests"])
            {
                data.ActiveQuests = global.ActiveQuests;
                data.ActiveQuest = global.ActiveQuest;
            }

            return true;
        }

        public bool Save(PlayerQuestData data, MySQLEntityClient database, Scope scope)
        {
            GlobalQuestData global = database.QuerySingle<GlobalQuestData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                global = new GlobalQuestData()
                {
                    PlayerID = data.PlayerID,
                    Slot = data.Slot
                };
            }
            global.Load();

            if (scope["Quests.Group"])
            {
                global.GroupID = data.GroupID;
                global.GroupRank = data.GroupRank;
                global.IsMainGroup = data.IsMainGroup;
            }

            if (scope["Quests.Flags"])
            {
                global.Flags = data.Flags;
            }

            if (scope["Quests.RadioFrequency"])
            {
                global.RadioFrequency = data.RadioFrequency;
            }

            if (scope["Quests.Marker"])
            {
                global.MarkerX = data.MarkerX;
                global.MarkerY = data.MarkerY;
                global.MarkerZ = data.MarkerZ;
            }

            if (scope["Quests.Quests"])
            {
                global.ActiveQuests = data.ActiveQuests;
                global.ActiveQuest = data.ActiveQuest;
            }

            global.Save();
            database.InsertUpdate(global, TableName);
            return true;
        }
    }
}