using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShimmyMySherbet.MySQL.EF.Models;

namespace Traversal.Models
{
    public abstract class GlobalDataModel
    {
        [SQLPrimaryKey]
        public ulong PlayerID;

        public byte Slot;
    }
}
