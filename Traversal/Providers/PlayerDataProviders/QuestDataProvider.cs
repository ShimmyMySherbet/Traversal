﻿using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Steamworks;
using Traversal.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;
using UnityEngine;

namespace Traversal.PlayerDataProviders
{
    public class QuestDataProvider : IPlayerDataProvider<PlayerQuests>
    {
        public const string TableName = "TraversalData_Quests";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<PlayerQuestData>(TableName);
            }
        }

        public bool Load(PlayerQuests instance, MySQLEntityClient database)
        {
            PlayerQuestData data = database.QuerySingle<PlayerQuestData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2 AND Map=@3", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID, Provider.map);

            if (data == null)
            {
                return false;
            }
            data.Load();
            SyncManager.AskSync(ref data);

            bool placed = data.MarkerX != 0 || data.MarkerY != 0 || data.MarkerZ != 0;
            Vector3 pos = new Vector3(data.MarkerX, data.MarkerY, data.MarkerZ);
            CSteamID id = new CSteamID(data.GroupID);

            instance.SetValue("isMarkerPlaced", placed);
            instance.SetValue("markerPosition", pos);
            instance.SetValue("radioFrequency", data.RadioFrequency);
            instance.SetValue("groupID", id);
            instance.SetValue("groupRank", (EPlayerGroupRank)data.GroupRank);
            instance.SetValue("inMainGroup", data.IsMainGroup);
            Dictionary<ushort, PlayerQuestFlag> flags = new Dictionary<ushort, PlayerQuestFlag>();
            List<PlayerQuestFlag> flagsList = new List<PlayerQuestFlag>();
            foreach (var dFlag in data.Flags)
            {
                var flag = new PlayerQuestFlag(dFlag.Flag, dFlag.Value);
                flags.Add(dFlag.Flag, flag);
                flagsList.Add(flag);
            }
            instance.SetValue("flagsMap", flags);
            instance.SetValue("flagsList", flagsList);
            Dictionary<ushort, PlayerQuest> questMap = new Dictionary<ushort, PlayerQuest>();
            List<PlayerQuest> questList = new List<PlayerQuest>();

            foreach (var dQuest in data.ActiveQuests)
            {
                PlayerQuest quest = new PlayerQuest(dQuest);
                questMap.Add(dQuest, quest);
                questList.Add(quest);
            }
            instance.SetValue("questsMap", questMap);
            instance.SetValue("questsList", questList);
            instance.SetValue("TrackedQuestID", data.ActiveQuest);

            instance.SetValue("wasLoadCalled", true);

            return true;
        }

        public bool Save(PlayerQuests instance, MySQLEntityClient database)
        {
            PlayerQuestData data = new PlayerQuestData()
            {
                GroupID = instance.groupID.m_SteamID,
                PlayerID = instance.channel.GetPlayerID(),
                Slot = instance.channel.GetPlayerSlotID(),
                ServerID = Traversal.ServerID,
                ActiveQuest = instance.TrackedQuestID,
                GroupRank = (byte)instance.groupRank,
                IsMainGroup = instance.GetValue<bool>("inMainGroup"),
                MarkerX = instance.markerPosition.x,
                MarkerY = instance.markerPosition.y,
                MarkerZ = instance.markerPosition.z,
                RadioFrequency = instance.radioFrequency,
                Map = Provider.map
            };
            var quests = instance.GetValue<List<PlayerQuest>>("questsList");
            var flags = instance.GetValue<List<PlayerQuestFlag>>("flagsList");

            if (quests == null) quests = new List<PlayerQuest>();
            if (flags == null) flags = new List<PlayerQuestFlag>();

            data.Flags = flags.Where(x => x != null).Select(x => new QuestFlag() { Flag = x.id, Value = x.value }).ToList();
            data.ActiveQuests = quests.Where(x => x != null).Select(x => x.id).ToList();

            data.Save();
            database.InsertUpdate(data, TableName);
            SyncManager.AskSave(data);
            return true;
        }
    }
}