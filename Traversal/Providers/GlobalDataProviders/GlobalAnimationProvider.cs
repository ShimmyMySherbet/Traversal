using System.Text.RegularExpressions;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.GlobalDataProviders
{
    public class GlobalAnimationProvider : IGlobalDataProvider<PlayerAnimationData>
    {
        public const string TableName = "PlayerData_Animation_Global";

        public string Name => "Animation";

        public void CheckSchema(MySQLEntityClient database)
        {
            if (!database.TableExists(TableName))
            {
                database.CreateTable<GlobalAnimationData>(TableName);
            }
        }

        public bool Load(ref PlayerAnimationData data, MySQLEntityClient database, Scope scope)
        {
            GlobalAnimationData global = database.QuerySingle<GlobalAnimationData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null) return false;

            if (scope["Animation.Handcuf"])
            {
                data.HandcuffID = global.HandcuffID;
                data.HandcuffStrength = global.HandcuffStrength;
                data.ArresterID = global.ArresterID;
            }

            if (scope["Animation.Gesture"])
            {
                data.Gesture = global.Gesture;
            }
            return true;
        }

        public bool Save(PlayerAnimationData data, MySQLEntityClient database, Scope scope)
        {
            GlobalAnimationData global = database.QuerySingle<GlobalAnimationData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                global = new GlobalAnimationData()
                {
                    Slot = data.Slot,
                    PlayerID = data.PlayerID
                };
            }

            if (scope["Animation.HandCuff"])
            {
                data.HandcuffID = global.HandcuffID;
                data.HandcuffStrength = global.HandcuffStrength;
                data.ArresterID = global.ArresterID;
            }
            if (scope["Animation.Gesture"])
            {
                data.Gesture = global.Gesture;
            }

            database.InsertUpdate(data, TableName);
            return true;
        }
    }
}