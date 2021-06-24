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

        public UserContact(string contactNumber)
        {
            Guards.NonEmpty(contactNumber, nameof(contactNumber));
            Id = Guid.NewGuid();
            ContactNumber = contactNumber;
        }

        public void Update(string contactNumber)
        {
            Guards.NonEmpty(contactNumber, nameof(contactNumber));
            ContactNumber = contactNumber;
        }
    }
}
