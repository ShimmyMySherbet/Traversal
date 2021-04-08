using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShimmyMySherbet.MySQL.EF;

namespace Traversal.Models.Databasing
{
    public class GlobalAnimationData : GlobalDataModel
    {
        public byte Gesture;
        public ulong ArresterID;
        public ushort HandcuffID;
        public ushort HandcuffStrength;
    }
}
