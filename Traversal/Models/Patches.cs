using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Core;
using Traversal.Models.Attributes;

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
            return !prov.Save(__instance, Client);
        }

        [Load(typeof(PlayerAnimator))]
        public static bool Animator_Load(PlayerAnimator __instance)
        {
            var prov = GetDataProvider<PlayerAnimator>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Load(__instance, Client);
        }

        [Save(typeof(PlayerClothing))]
        public static bool Clothing_Save(PlayerClothing __instance)
        {
            var prov = GetDataProvider<PlayerClothing>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Save(__instance, Client);
        }

        [Load(typeof(PlayerClothing))]
        public static bool Clothing_Load(PlayerClothing __instance)
        {
            var prov = GetDataProvider<PlayerClothing>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Load(__instance, Client);
        }

        [Save(typeof(PlayerInventory))]
        public static bool Inventory_Save(PlayerInventory __instance)
        {
            var prov = GetDataProvider<PlayerInventory>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Save(__instance, Client);
        }

        [Load(typeof(PlayerInventory))]
        public static bool Inventory_Load(PlayerInventory __instance)
        {
            var prov = GetDataProvider<PlayerInventory>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Load(__instance, Client);
        }

        [Save(typeof(PlayerLife))]
        public static bool Life_Save(PlayerLife __instance)
        {
            var prov = GetDataProvider<PlayerLife>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Save(__instance, Client);
        }

        [Load(typeof(PlayerLife))]
        public static bool Life_Load(PlayerLife __instance)
        {
            var prov = GetDataProvider<PlayerLife>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Load(__instance, Client);
        }

        [Save(typeof(Player))]
        public static bool Player_Save(Player __instance)
        {
            var prov = GetDataProvider<Player>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Save(__instance, Client);
        }

        [Load(typeof(Player))]
        public static bool Player_Load(Player __instance)
        {
            var prov = GetDataProvider<Player>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Load(__instance, Client);
        }

        [Save(typeof(PlayerQuests))]
        public static bool Quests_Save(PlayerQuests __instance)
        {
            var prov = GetDataProvider<PlayerQuests>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Save(__instance, Client);
        }

        [Load(typeof(PlayerQuests))]
        public static bool Quests_Load(PlayerQuests __instance)
        {
            var prov = GetDataProvider<PlayerQuests>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Load(__instance, Client);
        }

        [Save(typeof(PlayerSkills))]
        public static bool Skills_Save(PlayerSkills __instance)
        {
            var prov = GetDataProvider<PlayerSkills>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Save(__instance, Client);
        }

        [Load(typeof(PlayerSkills))]
        public static bool Skills_Load(PlayerSkills __instance)
        {
            var prov = GetDataProvider<PlayerSkills>();

            if (prov == null)
            {
                return true;
            }
            return !prov.Load(__instance, Client);
        }
    }
}