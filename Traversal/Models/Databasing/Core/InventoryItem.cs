using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Databasing
{
    public class InventoryItem
    {
        public ushort ItemID;
        public byte[] State = new byte[0];
        public byte X;
        public byte Y;
        public byte Rot;
        public byte Amount;
        public byte Quality;
    }
}
