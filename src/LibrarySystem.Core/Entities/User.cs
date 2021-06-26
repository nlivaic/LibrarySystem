using LibrarySystem.Common.Base;
using LibrarySystem.Common.Exceptions;
using LibrarySystem.Common.Guards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool IsEnabled { get; private set; }
        public IEnumerable<RentEvent> RentEvents => _rentEvents;
        public IEnumerable<UserContact> UserContacts => _userContacts;

        private readonly List<RentEvent> _rentEvents = new();
        private readonly List<UserContact> _userContacts = new();

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
            ValidateUserContact(userContact.ContactNumber);
            _userContacts.Add(userContact);
        }

        public void UpdateContactAsync(Guid userContactId, string contactNumber)
        {
            var userContact = _userContacts.SingleOrDefault(uc => uc.Id == userContactId)
                ?? throw new EntityNotFoundException(nameof(UserContact), userContactId);
            ValidateUserContact(contactNumber);
            userContact.Update(contactNumber);
        }

        private void Validate()
        {
            Guards.NonEmpty(FirstName, nameof(FirstName));
            Guards.NonEmpty(LastName, nameof(LastName));
            Guards.NonDefault(DateOfBirth, nameof(DateOfBirth));
        }

        private void ValidateUserContact(string contactNumber)
        {
            if (_userContacts.Any(uc => uc.ContactNumber == contactNumber))
            {
                throw new BusinessException(
                    $"Contact number '{contactNumber}' already assigned to user.");
            }
        }
    }
}
