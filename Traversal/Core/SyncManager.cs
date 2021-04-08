﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rocket.Core.Logging;
using Traversal.Models;

namespace Traversal.Core
{
    public static class SyncManager
    {
        public static bool IsEnabled;
        public static Dictionary<Type, object> Instances = new Dictionary<Type, object>();


        public static void AskSync<T>(ref T instance)
        {
            if (!Traversal.DoSync) return;

            var provider = GetGlobalProvider<T>();

            if (provider == null) return;

            if (!Traversal.Scope[$"{provider.Name}*"])
            {
                return;
            }

            provider.Load(ref instance, PatchManager.Client, Traversal.Scope);
        }


        public static void AskSave<T>(T instance)
        {

            if (!Traversal.DoSync) return;

            var provider = GetGlobalProvider<T>();

            if (provider == null) return;

            if (!Traversal.Scope[$"{provider.Name}*"])
            {
                return;
            }
            provider.Save(instance, PatchManager.Client, Traversal.Scope);
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