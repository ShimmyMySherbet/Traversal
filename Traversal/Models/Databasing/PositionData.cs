using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Databasing
{
    public class PositionData : PlayerDataModel
    {
        public bool IsDead;
        public float X;
        public float Y;
        public float Z;
        public byte Rot;
        public byte Stance;
    }
}
