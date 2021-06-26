using AutoMapper;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Common.Exceptions;
using LibrarySystem.Common.Guards;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserGetModel>
    {
        public Guid UserId { get; set; }

        class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserGetModel>
        {
            private readonly IUserRepository _repository;
            private readonly IMapper _mapper;

            public GetUserQueryHandler(
                IUserRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<UserGetModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _repository.GetByIdAsync(request.UserId);
                Guards.NonNull(user, request.UserId);
                return _mapper.Map<UserGetModel>(user);
            }
        }
    }
}
