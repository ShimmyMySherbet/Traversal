using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Exceptions
{
    public sealed class TraversalBadCastException : Exception
    {
        private string msg;

        public override string Message => msg;


        public TraversalBadCastException(string message) => msg = message;
    }
}
