using LibrarySystem.Common.Base;
using System;

namespace LibrarySystem.Core.Entities
{
    public class Librarian : BaseEntity<Guid>
    {
        public string FullName { get; private set; }

        private Librarian()
        { }
    }
}
