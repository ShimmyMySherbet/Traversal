using System.Management.Instrumentation;
using System.Reflection;
using Renci.SshNet;
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

        //[Save, Target(typeof(Player), "savePositionAndRotation")]
        //public static bool Player_Save(Player __instance)
        //{
        //    var prov = GetDataProvider<PlayerSpawnProxy>();

        //    if (prov == null)
        //    {
        //        return true;
        //    }

        //    PlayerSpawnProxy proxy = new PlayerSpawnProxy();
        //    proxy.MODE = PlayerSpawnProxy.MODE_SAVE;
        //    proxy.player = __instance;

        //    return PatchManager.RunPlayerDataProviderSave(prov, proxy);
        //}

        //[Load, Target(typeof(Provider), "loadPlayerSpawn")]
        //public static bool Player_Load(SteamPlayerID playerID, out Vector3 point, out byte angle, out EPlayerStance initialStance)
        //{
        //    point = Vector3.zero;
        //    angle = 0;
        //    initialStance = EPlayerStance.STAND;

        //    var prov = GetDataProvider<PlayerSpawnProxy>();

        //    if (prov == null)
        //    {
        //        return true;
        //    }
        //    PlayerSpawnProxy proxy = new PlayerSpawnProxy();
        //    proxy.MODE = PlayerSpawnProxy.MODE_LOAD;
        //    proxy.playerID = playerID;

        //    bool retval = PatchManager.RunPlayerDataProviderLoad(prov, proxy);
        //    if (retval)
        //    {
        //        return true;
        //    }

        //    point = proxy.Result.Point;
        //    angle = proxy.Result.Angle;
        //    initialStance = proxy.Result.Stance;
        //    return false;
        //}

        [Save(typeof(PlayerQuests))]
        public static bool Quests_Save(PlayerQuests __instance)
        {
            var prov = GetDataProvider<PlayerQuests>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderSave(prov, __instance);
        }

        [Load(typeof(PlayerQuests))]
        public static bool Quests_Load(PlayerQuests __instance)
        {
            var prov = GetDataProvider<PlayerQuests>();

            if (prov == null)
            {
                return true;
            }
            return PatchManager.RunPlayerDataProviderLoad(prov, __instance);
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