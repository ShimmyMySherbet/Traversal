﻿using System.Collections.Generic;
using Newtonsoft.Json;
using ShimmyMySherbet.MySQL.EF.Models;

namespace Traversal.Models.Databasing
{
    public class QuestData : PlayerDataModel
    {
        public float MarkerX;
        public float MarkerY;
        public float MarkerZ;
        public float RadioFrequency;
        public ulong GroupID;
        public byte GroupRank;
        public bool IsMainGroup;
        public ushort ActiveQuest;

        [SQLIgnore]
        public List<QuestFlag> Flags
        {
            get => InnerData.Flags;
            set => InnerData.Flags = value;
        }

        [SQLIgnore]
        public List<ushort> ActiveQuests
        {
            get => InnerData.ActiveQuests;
            set => InnerData.ActiveQuests = value;
        }

        [SQLIgnore] public InnerQuestData InnerData;

        [SQLPropertyName("Data")] public string FlagValue;

        public void Save() => FlagValue = JsonConvert.SerializeObject(InnerData);

        public void Load() => InnerData = JsonConvert.DeserializeObject<InnerQuestData>(FlagValue);
    }
}