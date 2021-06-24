using LibrarySystem.Common.Base;
using LibrarySystem.Common.Exceptions;
using LibrarySystem.Common.Guards;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Core.Entities
{
    public class Librarian : BaseEntity<Guid>
    {
        public string FullName { get; private set; }
        public Library Library { get; set; }
        public Guid LibraryId { get; set; }
        public IEnumerable<RentEvent> RentEvents => _rentEvents;

        private readonly List<RentEvent> _rentEvents = new();

        private Librarian()
        { }

        public Librarian(string fullName, Guid libraryId)
        {
            Guards.NonEmpty(fullName, nameof(fullName));
            Guards.NonDefault(libraryId, nameof(libraryId));
            Id = Guid.NewGuid();
            FullName = fullName;
            LibraryId = libraryId;
        }
    }
}
