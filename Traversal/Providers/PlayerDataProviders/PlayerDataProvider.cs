﻿using System;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;
using Traversal.Models.Proxies;
using UnityEngine;

namespace Traversal.Providers.PlayerDataProviders
{
    public class PlayerDataProvider : IPlayerDataProvider<PlayerProxy>
    {
        public const string TableName = "TraversalData_Location";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<PlayerPositionData>(TableName);
            }
        }

        /// <summary>
        /// Mimics Unturned's internal spawn code
        /// Janky as hell, but it's the only way to achieve this.
        /// </summary>
        public bool Load(PlayerProxy instance, MySQLEntityClient database)
        {
            PlayerPositionData data = database.QuerySingle<PlayerPositionData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2 AND Map=@3", instance.PlayerID, instance.PlayerSlot, Traversal.ServerID, Provider.map);
            SyncManager.AskSync(ref data);

            Vector3 point = Vector3.zero;
            byte angle = 0;
            var initialStance = EPlayerStance.STAND;
            bool flag = false;
            if (data != null && !data.IsDead && Level.info.type == ELevelType.SURVIVAL)
            {
                initialStance = (EPlayerStance)data.Stance;
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
                    Provider.onLoginSpawning(instance.SteamID, ref point, ref num, ref initialStance, ref flag);
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

        public bool Save(PlayerProxy instance, MySQLEntityClient database)
        {
            PlayerPositionData data = new PlayerPositionData()
            {
                IsDead = instance.Player.life.isDead,
                PlayerID = instance.PlayerID,
                ServerID = Traversal.ServerID,
                Rot = instance.Player.look.rot,
                Slot = instance.PlayerSlot,
                X = instance.Player.transform.position.x,
                Y = instance.Player.transform.position.y,
                Z = instance.Player.transform.position.z,
                Stance = (byte)instance.Player.stance.stance,
                Map = Provider.map
            };
            database.InsertUpdate(data, TableName);
            SyncManager.AskSave(data);
            return true;
        }
    }
}