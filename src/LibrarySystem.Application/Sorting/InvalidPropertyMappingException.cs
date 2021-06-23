using System;

namespace LibrarySystem.Application.Services.Sorting
{
    public class InvalidPropertyMappingException : Exception
    {
        public InvalidPropertyMappingException(string message) : base(message)
        {
        }
    }
}
