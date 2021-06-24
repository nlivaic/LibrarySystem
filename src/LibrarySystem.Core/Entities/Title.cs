using LibrarySystem.Common.Base;
using LibrarySystem.Common.Guards;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Core.Entities
{
    public class Title : BaseEntity<Guid>
    {
        public string Name { get; private set; }
        public string Author { get; private set; }
        public IEnumerable<TitleCopy> TitleCopies => _titleCopies;

        private List<TitleCopy> _titleCopies = new();

        private Title()
        { }

        public Title(string name, string author)
        {
            Guards.NonEmpty(name, nameof(name));
            Guards.NonEmpty(author, nameof(author));
            Id = Guid.NewGuid();
            Name = name;
            Author = author;
        }

        public TitleCopy AddTitleCopy()
        {
            var titleCopy = new TitleCopy(Id);
            _titleCopies.Add(titleCopy);
            return titleCopy;
        }
    }
}
