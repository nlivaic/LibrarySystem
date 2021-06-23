using LibrarySystem.Common.Base;
using System;

namespace LibrarySystem.Core.Entities
{
    public class UserContact : BaseEntity<Guid>
    {
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public string ContactNumber { get; private set; }

        private UserContact()
        { }
    }
}
