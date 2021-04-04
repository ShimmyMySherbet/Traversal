using SDG.Unturned;
using UnityEngine;

namespace Traversal.Models.Proxies
{
    public struct SpawnProxyResult
    {
        public static readonly SpawnProxyResult Nil = new SpawnProxyResult(Vector3.zero, 0, EPlayerStance.CLIMB);

        public Vector3 Point;
        public byte Angle;
        public EPlayerStance Stance;

        public SpawnProxyResult(Vector3 p, byte a, EPlayerStance s)
        {
            Point = p;
            Angle = a;
            Stance = s;
        }

  
    }
}