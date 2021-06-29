using AutoMapper;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Users.Commands
{
    public class CreateUserFromIdentityCardCommand : IRequest<UserGetModel>
    {
        public byte[] IdentityCardImage { get; set; }

        class CreateUserFromIdentityCardCommandHandler : IRequestHandler<CreateUserFromIdentityCardCommand, UserGetModel>
        {
            private readonly IIdentityCardScannerService _identityCardScannerService;
            private readonly IRepository<User> _userRepository;
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;
            private readonly ILogger<CreateUserFromIdentityCardCommand> _logger;

            public CreateUserFromIdentityCardCommandHandler(
                IIdentityCardScannerService identityCardScannerService,
                IRepository<User> userRepository,
                IUnitOfWork uow,
                IMapper mapper,
                ILogger<CreateUserFromIdentityCardCommand> logger)
            {
                _identityCardScannerService = identityCardScannerService;
                _userRepository = userRepository;
                _uow = uow;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<UserGetModel> Handle(CreateUserFromIdentityCardCommand request, CancellationToken cancellationToken)
            {
                var user = await _identityCardScannerService.ScanAsync(request.IdentityCardImage);
                await _userRepository.AddAsync(user);
                await _uow.SaveAsync();
                _logger.LogInformation("New user created.");
                return _mapper.Map<UserGetModel>(user);
            }
        }
    }
}
