using System;

namespace LibrarySystem.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        { }
    }
}
