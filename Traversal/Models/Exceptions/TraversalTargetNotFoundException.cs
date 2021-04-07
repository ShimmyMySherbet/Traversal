using System;

namespace Traversal.Models.Exceptions
{
    public sealed class TraversalTargetNotFoundException : Exception
    {
        private string msg;

        public override string Message => msg;

        public TraversalTargetNotFoundException(string message) => msg = message;
    }
}