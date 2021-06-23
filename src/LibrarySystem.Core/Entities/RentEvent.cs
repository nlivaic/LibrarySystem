using LibrarySystem.Common.Base;
using System;

namespace LibrarySystem.Core.Entities
{
    public class RentEvent : BaseEntity<Guid>
    {
        public Title Title { get; private set; }
        public Guid TitleCopyId { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public Librarian Librarian { get; private set; }
        public Guid LibrarianId { get; private set; }
        public DateTime DateRented { get; private set; }
        public DateTime DateDue { get; private set; }
        public DateTime? DateReturned { get; private set; }

        private RentEvent()
        { }
    }
}
