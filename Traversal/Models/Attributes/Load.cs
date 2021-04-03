using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Attributes
{
    public sealed class Load : Attribute
    {
        public Type Type;
        public Load(Type target) => Type = target;

    }
}
