using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Attributes
{
    public sealed class Target : Attribute
    {
        public MethodInfo TargetOverride;
        public Target(Type targetType, string targetName)
        {
            TargetOverride = targetType.GetMethod(targetName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase);
        }
    }
}
