using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Attributes;
using Patches = Traversal.Models.Patches;

namespace Traversal.Core
{
    public class PatchManager
    {
        public const string Harmony_ID = "Traversal";
        public static Dictionary<Type, object> Providers = new Dictionary<Type, object>();

        public static MySQLEntityClient Client;

        public static bool IsEnabled = false;

        public static void Patch(MySQLEntityClient client, Harmony HarmonyInstance)
        {
            Client = client;
            if (HarmonyInstance != null) HarmonyInstance.UnpatchAll(Harmony_ID);
            HarmonyInstance = new Harmony(Harmony_ID);

            foreach (MethodInfo method in typeof(Patches).GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                if (Attribute.IsDefined(method, typeof(Save)))
                {
                    Save s = (Save)Attribute.GetCustomAttribute(method, typeof(Save));

                    MethodInfo target = s.Type.GetMethod("save", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);

                    HarmonyInstance.Patch(target, new HarmonyMethod(method));
                }

                if (Attribute.IsDefined(method, typeof(Load)))
                {
                    Load l = (Load)Attribute.GetCustomAttribute(method, typeof(Load));

                    MethodInfo target = l.Type.GetMethod("load", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);

                    HarmonyInstance.Patch(target, new HarmonyMethod(method));
                }
            }
            IsEnabled = true;
        }

        public static void Unpatch(Harmony HarmonyInstance)
        {
            HarmonyInstance.UnpatchAll(Harmony_ID);
            Client?.Disconnect();
            IsEnabled = false;
        }

        public static IPlayerDataProvider<T> GetDataProvider<T>()
        {
            if (!IsEnabled) return null;

            Type t = typeof(IPlayerDataProvider<T>);

            if (Providers.ContainsKey(t))
            {
                object ob = Providers[t];
                if (ob is IPlayerDataProvider<T> prov)
                {
                    return prov;
                }
            }

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => t.IsAssignableFrom(t)))
            {
                object instance = Activator.CreateInstance(type);
                if (instance is IPlayerDataProvider<T> prov)
                {
                    Providers[t] = prov;
                    return prov;
                }
            }
            return null;
        }
    }
}