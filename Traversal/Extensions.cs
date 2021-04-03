using System;
using System.Reflection;
using Org.BouncyCastle.Asn1;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;

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
            throw new Exception("Property/Field not found.");
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
            throw new Exception("Property/Field not found.");
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