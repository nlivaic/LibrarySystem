using LibrarySystem.Common.Base;
using System;

namespace LibrarySystem.Core.Entities
{
    public class TitleCopy : BaseEntity<Guid>
    {
        public Title Title { get; private set; }
        public Guid TitleId { get; private set; }

        private TitleCopy()
        { }
    }
}
