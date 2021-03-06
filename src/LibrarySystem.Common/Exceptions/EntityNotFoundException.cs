using System;

namespace LibrarySystem.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, Guid id)
            : base($"Entity {entityName} with id {id} not found.")
        { }
    }
}
