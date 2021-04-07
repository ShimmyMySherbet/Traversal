using System;

namespace Traversal.Models.Exceptions
{
    public sealed class TraversalTargetNotFoundException : Exception
    {
        public new string Message { get; set; }

        public TraversalTargetNotFoundException(string message) => Message = message;
    }
}