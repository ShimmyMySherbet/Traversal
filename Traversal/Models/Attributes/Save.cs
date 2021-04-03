using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Attributes
{
    public sealed class Save : Attribute
    {
        public Type Type;
        public Save(Type target) => Type = target;
    }
}
