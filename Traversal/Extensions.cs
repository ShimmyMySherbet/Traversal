using System;
using System.Reflection;
using SDG.Unturned;
using Traversal.Models.Exceptions;

namespace Traversal
{
    public static class Extensions
    {
        public const BindingFlags Flags_All = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

        public static T GetValue<T>(this object obj, string propertyName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            FieldInfo fo = obj.GetType().GetField(propertyName, Flags_All);
            if (fo != null)
            {
                var val = fo.GetValue(obj);
                if (val is T t)
                {
                    return t;
                }
                throw new InvalidCastException();
            }
            PropertyInfo pro = obj.GetType().GetProperty(propertyName, Flags_All);

            if (pro != null)
            {
                var val = pro.GetValue(obj);
                if (val is T t)
                {
                    return t;
                }
                throw new InvalidCastException();
            }
            throw new TraversalTargetNotFoundException($"Get Property/Field '{propertyName}' not found.");
        }

        public static void SetValue<T>(this object obj, string propertyName, T value)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            FieldInfo fo = obj.GetType().GetField(propertyName, Flags_All);
            if (fo != null)
            {
                fo.SetValue(obj, value);
                return;
            }
            PropertyInfo pro = obj.GetType().GetProperty(propertyName, Flags_All);

            if (pro != null)
            {
                pro.SetValue(obj, value);
            }
            throw new TraversalTargetNotFoundException($"Set Property/Field '{propertyName}' not found.");
        }

        public static void InvokeTarget(this object obj, string methodName, params object[] param)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            MethodInfo m = obj.GetType().GetMethod(methodName, Flags_All);
            if (m != null)
            {
                m.Invoke(obj, param);
            }
            throw new TraversalTargetNotFoundException($"Method '{methodName}' not found.");
        }

        public static T InvokeTarget<T>(this object obj, string methodName, params object[] param)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            MethodInfo m = obj.GetType().GetMethod(methodName, Flags_All);

            if (m != null)
            {
                if (!typeof(T).IsAssignableFrom(m.ReturnType))
                {
                    throw new TraversalBadCastException($"Cannot cast from type {m.ReturnType.Name} to {typeof(T).Name} at method {methodName}");
                }

                var o = m.Invoke(obj, param);
                if (o == null) return default(T);
                if (o is T t)
                {
                    return t;
                }
                throw new TraversalBadCastException($"Cannot cast from type {o.GetType().Name} to {typeof(T).Name} at method {methodName}");
            }
            throw new TraversalTargetNotFoundException($"Method '{methodName}' not found.");
        }

        public static ulong GetPlayerID(this SteamChannel channel)
        {
            return channel.owner.playerID.steamID.m_SteamID;
        }

        public static byte GetPlayerSlotID(this SteamChannel channel)
        {
            return channel.owner.playerID.characterID;
        }
    }
}