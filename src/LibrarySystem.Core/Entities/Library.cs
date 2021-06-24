using LibrarySystem.Common.Base;
using LibrarySystem.Common.Exceptions;
using LibrarySystem.Common.Guards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Entities
{
    public class Library : BaseEntity<Guid>
    {
        public string Name { get; private set; }
        public IEnumerable<Librarian> Librarians => _librarians;
        public IEnumerable<TitleCopy> TitleCopies => _titleCopies;

        private readonly List<Librarian> _librarians = new();
        private readonly List<TitleCopy> _titleCopies = new();

        private Library()
        { }

        public Library(string name)
        {
            Guards.NonEmpty(name, nameof(name));
            Id = Guid.NewGuid();
            Name = name;
        }

        public void Employ(Librarian librarian)
        {
            if (_librarians.Any(l => l.FullName == librarian.FullName))
            {
                throw new BusinessException($"Librarian '{librarian.FullName}` is already employed.");
            }
            _librarians.Add(librarian);
        }

        // Fire Librarian :)
    }
}
