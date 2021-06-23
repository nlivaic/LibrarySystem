using LibrarySystem.Common.Base;
using System;

namespace LibrarySystem.Core.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        private User()
        { }

    }
}
