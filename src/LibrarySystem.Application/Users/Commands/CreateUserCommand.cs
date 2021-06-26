using AutoMapper;
using LibrarySystem.Application.Users.Queries;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserGetModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserGetModel>
        {
            private readonly IRepository<User> _userRepository;
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;
            private readonly ILogger<CreateUserCommandHandler> _logger;

            public CreateUserCommandHandler(
                IRepository<User> userRepository,
                IUnitOfWork uow,
                IMapper mapper,
                ILogger<CreateUserCommandHandler> logger)
            {
                _userRepository = userRepository;
                _uow = uow;
                _mapper = mapper;
                _logger = logger;
            }
            public async Task<UserGetModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new User(request.FirstName, request.LastName, request.DateOfBirth);
                await _userRepository.AddAsync(user);
                await _uow.SaveAsync();
                _logger.LogInformation("New user created.");
                return _mapper.Map<UserGetModel>(user);
            }
        }
    }
}
