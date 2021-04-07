using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversal.Models.Exceptions
{
    public sealed class TraversalBadCastException : Exception
    {
        public new string Message { get; set; }

        public TraversalBadCastException(string message) => Message = message;
    }
}
