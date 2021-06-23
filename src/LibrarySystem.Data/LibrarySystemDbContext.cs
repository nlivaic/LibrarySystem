using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data
{
    public class LibrarySystemDbContext : DbContext
    {
        public LibrarySystemDbContext(DbContextOptions options)
            : base(options)
        { }
    }
}
