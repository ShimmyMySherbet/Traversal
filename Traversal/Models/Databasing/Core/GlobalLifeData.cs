using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Databasing
{
    public class GlobalLifeData : GlobalDataModel
    {
        public byte Health;
        public byte Food;
        public byte Water;
        public byte Virus;
        public byte Stamina;
        public byte Oxygen;
        public bool Bleeding;
        public bool Broken;
        public byte Temperature;
    }
}
