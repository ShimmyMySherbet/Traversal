using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Rocket.Core.Logging;
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
                if (Attribute.IsDefined(method, typeof(Disabled))) continue;

                if (Attribute.IsDefined(method, typeof(Save)))
                {
                    Save s = (Save)Attribute.GetCustomAttribute(method, typeof(Save));

                    MethodInfo target;

                    if (Attribute.IsDefined(method, typeof(Target)))
                    {
                        target = ((Target)Attribute.GetCustomAttribute(method, typeof(Target))).TargetOverride;
                    }
                    else
                    {
                        target = s.Type.GetMethod("save", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
                    }

                    HarmonyInstance.Patch(target, new HarmonyMethod(method));
                }

                if (Attribute.IsDefined(method, typeof(Load)))
                {
                    Load l = (Load)Attribute.GetCustomAttribute(method, typeof(Load));

                    MethodInfo target;

                    if (Attribute.IsDefined(method, typeof(Target)))
                    {
                        target = ((Target)Attribute.GetCustomAttribute(method, typeof(Target))).TargetOverride;
                    }
                    else
                    {
                        target = l.Type.GetMethod("load", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
                    }

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

        public static bool RunPlayerDataProviderLoad<T>(IPlayerDataProvider<T> provider, T instance)
        {
            Logger.Log($"Running Load of {typeof(T).Name} through {provider.GetType().Name}...");
            try
            {
                return !provider.Load(instance, Client);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to load player data from provider: {provider.GetType().Name}");
                Logger.LogError($"Message: {ex.Message}");
                Logger.LogError($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException == null)
                {
                    Logger.LogWarning("No inner error.");
                }
                else
                {
                    Logger.LogError($"Inner: {ex.InnerException.Message}");
                }
                Logger.LogWarning($"Falling back on internal load for type {typeof(T).Name}");
            }
            Logger.Log($"Loaded {typeof(T).Name}.");

            return false;
        }

        public static bool RunPlayerDataProviderSave<T>(IPlayerDataProvider<T> provider, T instance)
        {
            Logger.Log($"Running save of {typeof(T).Name} through {provider.GetType().Name}...");

            try
            {
                return !provider.Save(instance, Client);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to save player data to provider: {provider.GetType().Name}");
                Logger.LogError($"Message: {ex.Message}");
                Console.WriteLine();
                Logger.LogError($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException == null)
                {
                    Logger.LogWarning("No inner error.");
                }
                else
                {
                    Logger.LogError($"Inner: {ex.InnerException.Message}");
                }
                Logger.LogWarning($"Falling back on internal save for type {typeof(T).Name}");
            }
            Logger.Log($"Saved {typeof(T).Name}.");

            return false;
        }

        public static IPlayerDataProvider<T> GetDataProvider<T>()
        {
            if (!IsEnabled) return null;
            Logger.Log($"Looking for provider type for {typeof(T).Name}");
            Type t = typeof(IPlayerDataProvider<T>);

            if (Providers.ContainsKey(t))
            {
                object ob = Providers[t];
                if (ob is IPlayerDataProvider<T> prov)
                {
                    return prov;
                }
            }
            Logger.Log($"Looking for new instance of provider for {typeof(T).Name}");

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => t.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface))
            {
                Console.WriteLine($"Instantiate {type.Name}");
                object instance = Activator.CreateInstance(type);
                if (instance is IPlayerDataProvider<T> prov)
                {
                    Providers[t] = prov;
                    Logger.Log($"Checking schema for provider: {prov.GetType().Name}");
                    prov.CheckSchema(Client);
                    return prov;
                }
                else
                {
                    Console.WriteLine($"Something went wrong! @ {instance.GetType().Name}");
                }
            }
            return null;
        }
    }
}