﻿using System.Diagnostics;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Core;
using Traversal.Models.Attributes;
using Traversal.Models.Proxies;
using UnityEngine;

namespace Traversal.Models
{
    public static class Patches
    {
        public static Stopwatch Stopwatch;
        public static IPlayerDataProvider<T> GetDataProvider<T>()
        {
            return PatchManager.GetDataProvider<T>();
        }

        public static MySQLEntityClient Client => PatchManager.Client;

        [Save(typeof(PlayerAnimator))]
        public static bool Animator_Save(PlayerAnimator __instance)
        {
            var prov = GetDataProvider<PlayerAnimator>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderSave(prov, __instance);
        }

        [Load(typeof(PlayerAnimator))]
        public static bool Animator_Load(PlayerAnimator __instance)
        {
            var prov = GetDataProvider<PlayerAnimator>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderLoad(prov, __instance);
        }

        [Save(typeof(PlayerClothing))]
        public static bool Clothing_Save(PlayerClothing __instance)
        {
            var prov = GetDataProvider<PlayerClothing>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderSave(prov, __instance);
        }

        [Load(typeof(PlayerClothing))]
        public static bool Clothing_Load(PlayerClothing __instance)
        {
            var prov = GetDataProvider<PlayerClothing>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderLoad(prov, __instance);
        }

        [Save(typeof(PlayerInventory))]
        public static bool Inventory_Save(PlayerInventory __instance)
        {
            var prov = GetDataProvider<PlayerInventory>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderSave(prov, __instance);
        }

        [Load(typeof(PlayerInventory))]
        public static bool Inventory_Load(PlayerInventory __instance)
        {
            var prov = GetDataProvider<PlayerInventory>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderLoad(prov, __instance);
        }

        [Save(typeof(PlayerLife))]
        public static bool Life_Save(PlayerLife __instance)
        {
            var prov = GetDataProvider<PlayerLife>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderSave(prov, __instance);
        }

        [Load(typeof(PlayerLife))]
        public static bool Life_Load(PlayerLife __instance)
        {
            var prov = GetDataProvider<PlayerLife>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderLoad(prov, __instance);
        }

        [Save, Target(typeof(Player), "savePositionAndRotation")]
        public static bool Player_Save(Player __instance)
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            var prov = GetDataProvider<PlayerProxy>();

            if (prov == null)
            {
                return true;
            }

            PlayerProxy proxy = new PlayerProxy()
            {
                PlayerID = __instance.channel.GetPlayerID(),
                PlayerSlot = __instance.channel.GetPlayerSlotID(),
                SteamID = __instance.channel.owner.playerID,
                Player = __instance
            };

            return PatchManager.RunPlayerDataProviderSave(prov, proxy);
        }

        [Load, Target(typeof(Provider), "loadPlayerSpawn")]
        public static bool Player_Load(SteamPlayerID playerID, out Vector3 point, out byte angle, out EPlayerStance initialStance)
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();

            point = Vector3.zero;
            angle = 0;
            initialStance = EPlayerStance.STAND;

            var prov = GetDataProvider<PlayerProxy>();

            if (prov == null)
            {
                return true;
            }

            PlayerProxy proxy = new PlayerProxy()
            {
                PlayerID = playerID.steamID.m_SteamID,
                PlayerSlot = playerID.characterID,
                SteamID = playerID,
                Player = null
            };

            if (!PatchManager.RunPlayerDataProviderLoad(prov, proxy))
            {
                point = proxy.Result.Point;
                angle = proxy.Result.Angle;
                initialStance = proxy.Result.Stance;
                return false;
            }
            return true;
        }

        [Save(typeof(PlayerQuests))]
        public static bool Quests_Save(PlayerQuests __instance)
        {
            var prov = GetDataProvider<PlayerQuests>();

            if (prov == null)
            {
                return true;
            }
            bool r = PatchManager.RunPlayerDataProviderSave(prov, __instance);
            Stopwatch.Stop();

            UnturnedLog.info("Player Save took {ms}ms, ({t} ticks)", Stopwatch.ElapsedMilliseconds, Stopwatch.ElapsedTicks);


            return r;
        }

        [Load(typeof(PlayerQuests))]
        public static bool Quests_Load(PlayerQuests __instance)
        {
            var prov = GetDataProvider<PlayerQuests>();

            if (prov == null)
            {
                return true;
            }
            var r = PatchManager.RunPlayerDataProviderLoad(prov, __instance);
            Stopwatch.Stop();

            UnturnedLog.info("Player Load took {ms}ms, ({t} ticks)", Stopwatch.ElapsedMilliseconds, Stopwatch.ElapsedTicks);
            return r;
        }

        [Save(typeof(PlayerSkills))]
        public static bool Skills_Save(PlayerSkills __instance)
        {
            var prov = GetDataProvider<PlayerSkills>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderSave(prov, __instance);
        }

        [Load(typeof(PlayerSkills))]
        public static bool Skills_Load(PlayerSkills __instance)
        {
            var prov = GetDataProvider<PlayerSkills>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderLoad(prov, __instance);
        }
    }
}