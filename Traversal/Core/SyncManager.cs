using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rocket.Core.Logging;
using Traversal.Models;

namespace Traversal.Core
{
    public static class SyncManager
    {
        public static bool IsEnabled = true;
        public static Dictionary<Type, object> Instances = new Dictionary<Type, object>();


        public static void AskSync<T>(ref T instance)
        {
            if (!Traversal.DoSync) return;

            var provider = GetGlobalProvider<T>();

            if (provider == null) return;

            if (!Traversal.Scope[$"{provider.Name}.*"])
            {
                return;
            }
            Logger.Log($"Running Global Sync Load of {typeof(T).Name} through {provider.GetType().Name}...");

            try
            {
                provider.Load(ref instance, PatchManager.Client, Traversal.Scope);
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
        }


        public static void AskSave<T>(T instance)
        {

            if (!Traversal.DoSync) return;

            var provider = GetGlobalProvider<T>();

            if (provider == null) return;

            if (!Traversal.Scope[$"{provider.Name}.*"])
            {
                return;
            }
            Logger.Log($"Running Global Sync Save of {typeof(T).Name} through {provider.GetType().Name}...");

            try
            {
                provider.Save(instance, PatchManager.Client, Traversal.Scope);

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
        }


        public static IGlobalDataProvider<T> GetGlobalProvider<T>()
        {
            if (!IsEnabled) return null;
            Logger.Log($"Looking for global provider type for {typeof(T).Name}");
            Type t = typeof(IGlobalDataProvider<T>);

            if (Instances.ContainsKey(t))
            {
                object ob = Instances[t];
                if (ob is IGlobalDataProvider<T> prov)
                {
                    return prov;
                }
            }
            Logger.Log($"Looking for new instance of global provider for {typeof(T).Name}");

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => t.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface))
            {
                Console.WriteLine($"Instantiate {type.Name}");
                object instance = Activator.CreateInstance(type);
                if (instance is IGlobalDataProvider<T> prov)
                {
                    Instances[t] = prov;
                    Logger.Log($"Checking schema for provider: {prov.GetType().Name}");
                    prov.CheckSchema(PatchManager.Client);
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