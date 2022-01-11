using System;

namespace Core.Exceptions
{
    public class ControllerException : Exception
    {
        public ControllerException()
        { }

        public ControllerException(string message) : base(message)
        { }
    }
}
