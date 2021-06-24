using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Models;
using LibrarySystem.Common.Paging;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<PagedList<UserGetModel>>
    {
        public UserQueryParameters UserQueryParameters { get; set; }

        class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedList<UserGetModel>>
        {
            private readonly IUserRepository _userRepository;

            public GetUsersQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<PagedList<UserGetModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken) =>
                await _userRepository.GetPagedUsers(request.UserQueryParameters);
        }
    }
}
