using LibrarySystem.Common.Base;
using LibrarySystem.Common.Exceptions;
using LibrarySystem.Common.Guards;
using System;

namespace LibrarySystem.Core.Entities
{
    public class RentEvent : BaseEntity<Guid>
    {
        public TitleCopy TitleCopy { get; private set; }
        public Guid TitleCopyId { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public Librarian Librarian { get; private set; }
        public Guid LibrarianId { get; private set; }
        public DateTime DateRented { get; private set; }
        public DateTime DateDue { get; private set; }
        public DateTime? DateReturned { get; private set; }
        public bool IsReturned { get; private set; }

        private RentEvent()
        { }

        public RentEvent(Guid titleCopyId, Guid userId, Guid librarianId)
        {
            Guards.NonDefault(titleCopyId, nameof(titleCopyId));
            Guards.NonDefault(userId, nameof(userId));
            Guards.NonDefault(librarianId, nameof(librarianId));
            Id = Guid.NewGuid();
            TitleCopyId = titleCopyId;
            UserId = userId;
            LibrarianId = librarianId;
            DateRented = DateTime.UtcNow;
            DateDue = DateTime.UtcNow.AddMonths(1);
        }

        public void TitleReturned()
        {
            DateReturned = DateTime.UtcNow;

            //if (DateReturned > DateDue)
            //{
            //    Raise an event .
            //}
        }
    }
}
