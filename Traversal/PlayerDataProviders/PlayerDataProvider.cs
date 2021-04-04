using System;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Proxies;
using UnityEngine;

namespace Traversal.PlayerDataProviders
{
    public class PlayerDataProvider : IPlayerDataProvider<PlayerSpawnProxy>
    {
        public const string TableName = "PlayerData_Animation";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<PositionData>(TableName);
            }
        }


        /// <summary>
        /// Mimics Unturned's internal spawn code
        /// Janky as hell, but it's the only way to achieve this.
        /// </summary>
        public bool Load(PlayerSpawnProxy instance, MySQLEntityClient database)
        {
            PositionData data = database.QuerySingle<PositionData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2", instance.playerID, instance.SlotID, Traversal.ServerID);

            Vector3 point = Vector3.zero;
            byte angle = 0;
            var initialStance = EPlayerStance.STAND;
            bool flag = false;
            if (data != null && !data.IsDead && Level.info.type == ELevelType.SURVIVAL)
            {
                point = new Vector3(data.X, data.Y, data.Z) + new Vector3(0f, 0.01f, 0f);
                angle = data.Rot;
                if (!point.IsFinite())
                {
                    flag = true;
                }
                else if (point.y > Level.HEIGHT)
                {
                    point.y = Level.HEIGHT - 10f;
                }
                else if (!PlayerStance.getStanceForPosition(point, ref initialStance))
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            try
            {
                if (Provider.onLoginSpawning != null)
                {
                    float num = (float)(angle * 2);
                    Provider.onLoginSpawning(instance.playerID, ref point, ref num, ref initialStance, ref flag);
                    angle = (byte)(num / 2f);
                }
            }
            catch (Exception e)
            {
                UnturnedLog.warn("Plugin raised an exception from onLoginSpawning:");
                UnturnedLog.exception(e);
            }
            if (flag)
            {
                PlayerSpawnpoint spawn = LevelPlayers.getSpawn(false);
                point = spawn.point + new Vector3(0f, 0.5f, 0f);
                angle = (byte)(spawn.angle / 2f);
            }

            instance.Result = new SpawnProxyResult(point, angle, initialStance);
            return true;
        }

        public bool Save(PlayerSpawnProxy instance, MySQLEntityClient database)
        {

            PositionData data = new PositionData()
            {
                IsDead = instance.player.life.isDead,
                PlayerID = instance.PlayerID,
                ServerID = Traversal.ServerID,
                Rot = instance.player.look.rot,
                Slot = instance.SlotID,
                X = instance.player.transform.position.x,
                Y = instance.player.transform.position.y,
                Z = instance.player.transform.position.z
            };

            database.InsertUpdate(data, TableName);
            return true;
        }
    }
}