using LibrarySystem.Common.Base;
using System;

namespace LibrarySystem.Core.Entities
{
    public class Library : BaseEntity<Guid>
    {
        public string Name { get; private set; }

        private Library()
        { }
    }
}
