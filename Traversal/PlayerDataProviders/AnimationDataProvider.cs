using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.PlayerDataProviders
{
    public class AnimationDataProvider : IPlayerDataProvider<PlayerAnimator>
    {
        public const string TableName = "PlayerData_Animation";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<PlayerAnimationData>(TableName);
            }
        }

        public bool Load(PlayerAnimator instance, MySQLEntityClient database)
        {
            PlayerAnimationData data = database.QuerySingle<PlayerAnimationData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2 AND Map=@3", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID, Provider.map);

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
            PlayerAnimationData data = new PlayerAnimationData()
            {
                ArresterID = instance.captorID.m_SteamID,
                PlayerID = instance.channel.GetPlayerID(),
                ServerID = Traversal.ServerID,
                Gesture = (byte)instance.gesture,
                HandcuffID = instance.captorItem,
                HandcuffStrength = instance.captorStrength,
                Slot = instance.channel.GetPlayerSlotID(),
                Map = Provider.map
            };
            database.InsertUpdate(data, TableName);
            return true;
        }
    }
}