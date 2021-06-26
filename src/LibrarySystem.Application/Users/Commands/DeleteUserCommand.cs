using LibrarySystem.Common.Guards;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }

        class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
        {
            private readonly IRepository<User> _userRepository;
            private readonly IUnitOfWork _uow;

            public DeleteUserCommandHandler(
                IRepository<User> userRepository,
                IUnitOfWork uow)
            {
                _userRepository = userRepository;
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                Guards.NonNull(user, request.UserId);
                user.Delete();
                await _uow.SaveAsync();
                return Unit.Value;
            }
        }
    }
}
