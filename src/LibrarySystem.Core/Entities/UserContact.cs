using LibrarySystem.Common.Base;
using LibrarySystem.Common.Guards;
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

        public UserContact(Guid userId, string contactNumber)
        {
            Guards.NonDefault(userId, nameof(userId));
            Guards.NonEmpty(contactNumber, nameof(contactNumber));
            Id = Guid.NewGuid();
            UserId = userId;
            ContactNumber = contactNumber;
        }
    }
}
