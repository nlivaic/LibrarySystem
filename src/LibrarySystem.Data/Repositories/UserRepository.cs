using AutoMapper;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Models;
using LibrarySystem.Application.Users.Queries;
using LibrarySystem.Common.Paging;
using LibrarySystem.Core.Entities;
using LibrarySystem.Data.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(
            LibrarySystemDbContext context,
            IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<UserGetModel>> GetPagedUsers(UserQueryParameters userQueryParameters)
        {
            var query = _context
                .Users as IQueryable<User>;
            if (!string.IsNullOrWhiteSpace(userQueryParameters.SearchQuery))
            {
                var searchQueryLowercase = userQueryParameters.SearchQuery.ToLower();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(searchQueryLowercase) ||
                    u.LastName.ToLower().Contains(searchQueryLowercase));
            }
            return await query
                .OrderBy(u => u.Id)
                .Include(u => u.UserContacts)
                .ApplySorting(userQueryParameters.SortBy)
                .AsNoTracking()
                .ApplyPagingAsync<User, UserGetModel>(
                    _mapper,
                    userQueryParameters.PageNumber,
                    userQueryParameters.PageSize);
        }
    }
}
