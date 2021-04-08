using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Databasing
{
    public class InnerQuestData
    {
        public List<QuestFlag> Flags = new List<QuestFlag>();
        public List<ushort> ActiveQuests = new List<ushort>();
    }
}
