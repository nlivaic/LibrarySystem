using System.Threading.Tasks;

namespace LibrarySystem.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
