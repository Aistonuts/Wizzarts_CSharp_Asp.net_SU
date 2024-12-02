namespace Wizzarts.Common
{
    using System;

    public class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException() { }

        public UnauthorizedActionException(string message)
            : base(message) { }
    }
}
