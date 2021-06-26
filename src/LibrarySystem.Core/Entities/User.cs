using LibrarySystem.Common.Base;
using LibrarySystem.Common.Exceptions;
using LibrarySystem.Common.Guards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool IsEnabled { get; private set; }
        public IEnumerable<RentEvent> RentEvents => _rentEvents;
        public IEnumerable<UserContact> UserContacts => _userContact;

        private readonly List<RentEvent> _rentEvents = new();
        private readonly List<UserContact> _userContact = new();

        private User()
        { }

        public User(string firstName, string lastName, DateTime dateOfBirth)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            IsEnabled = true;
            Validate();
        }

        public void UpdateUser(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Validate();
        }

        public void Delete()
        {
            IsEnabled = false;
        }

        public void AddContact(UserContact userContact)
        {
            if (_userContact.Any(uc => uc.ContactNumber == userContact.ContactNumber))
            {
                throw new BusinessException(
                    $"Contact number '{userContact.ContactNumber}' already assigned to user.");
            }
            _userContact.Add(userContact);
        }

        private void Validate()
        {
            Guards.NonEmpty(FirstName, nameof(FirstName));
            Guards.NonEmpty(LastName, nameof(LastName));
            Guards.NonDefault(DateOfBirth, nameof(DateOfBirth));
        }
    }
}
