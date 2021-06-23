using LibrarySystem.Common.Interfaces;
using System.Threading.Tasks;

namespace LibrarySystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibrarySystemDbContext _dbContext;

        public UnitOfWork(LibrarySystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
