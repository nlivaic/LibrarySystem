using LibrarySystem.Common.Base;
using LibrarySystem.Common.Exceptions;
using LibrarySystem.Common.Guards;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Core.Entities
{
    public class TitleCopy : BaseEntity<Guid>
    {
        public Title Title { get; private set; }
        public Guid TitleId { get; private set; }
        public Library Library { get; private set; }
        public Guid LibraryId { get; private set; }
        public IEnumerable<RentEvent> RentEvents => _rentEvents;
        public IEnumerable<TitleCopy> TitleCopies => _titleCopies;

        private readonly List<RentEvent> _rentEvents = new();
        private readonly List<TitleCopy> _titleCopies = new();

        private TitleCopy()
        { }

        public TitleCopy(Guid titleId)
        {
            Guards.NonDefault(titleId, nameof(titleId));
            Id = Guid.NewGuid();
            TitleId = titleId;
        }
    }
}
