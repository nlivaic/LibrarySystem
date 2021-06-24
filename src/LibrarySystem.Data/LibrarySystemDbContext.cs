using LibrarySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data
{
    public class LibrarySystemDbContext : DbContext
    {
        public LibrarySystemDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<RentEvent> RentEvents { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleCopy> TitleCopies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
    }
}
