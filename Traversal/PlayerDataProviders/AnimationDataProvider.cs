using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;

namespace Traversal.PlayerDataProviders
{
    public class AnimationDataProvider : IPlayerDataProvider<PlayerAnimator>
    {
        public const string TableName = "PlayerData_Animation";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<AnimationData>(TableName);
            }
        }

        public bool Load(PlayerAnimator instance, MySQLEntityClient database)
        {
            AnimationData data = database.QuerySingle<AnimationData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID);

            if (data == null)
            {
                return true;
            }

            instance.SetValue("_gesture", (EPlayerGesture)data.Gesture);
            instance.captorID = new Steamworks.CSteamID(data.ArresterID);
            instance.captorItem = data.HandcuffID;
            instance.captorStrength = data.HandcuffStrength;

            instance.SetValue("wasLoadCalled", true);
            return true;
        }

        public bool Save(PlayerAnimator instance, MySQLEntityClient database)
        {
            AnimationData data = new AnimationData()
            {
                ArresterID = instance.captorID.m_SteamID,
                PlayerID = instance.channel.GetPlayerID(),
                ServerID = Traversal.ServerID,
                Gesture = (byte)instance.gesture,
                HandcuffID = instance.captorItem,
                HandcuffStrength = instance.captorStrength,
                Slot = instance.channel.GetPlayerSlotID()
            };
            database.InsertUpdate(data, TableName);
            return true;
        }
    }
}