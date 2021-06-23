using LibrarySystem.Common.Base;
using System;

namespace LibrarySystem.Core.Entities
{
    public class Title : BaseEntity<Guid>
    {
        public string Name { get; private set; }
        public string Author { get; private set; }

        private Title()
        { }
    }
}
