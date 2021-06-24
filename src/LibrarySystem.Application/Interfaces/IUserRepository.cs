using LibrarySystem.Application.Models;
using LibrarySystem.Application.Users.Queries;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Common.Paging;
using LibrarySystem.Core.Entities;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PagedList<UserGetModel>> GetPagedUsers(UserQueryParameters userQueryParameters);
    }
}
