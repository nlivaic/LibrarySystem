using LibrarySystem.Common.Base;
using LibrarySystem.Common.Guards;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Core.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public IEnumerable<RentEvent> RentEvents => _rentEvents;

        private readonly List<RentEvent> _rentEvents = new();

        private User()
        { }

        public User(string firstName, string lastName, DateTime dateOfBirth)
        {
            Guards.NonEmpty(firstName, nameof(firstName));
            Guards.NonEmpty(lastName, nameof(lastName));
            Guards.NonDefault(dateOfBirth, nameof(dateOfBirth));
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }
}
