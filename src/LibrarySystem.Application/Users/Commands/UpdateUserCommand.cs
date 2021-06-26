using LibrarySystem.Common.Guards;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
        {
            private readonly IRepository<User> _userRepository;
            private readonly IUnitOfWork _uow;

            public UpdateUserCommandHandler(
                IRepository<User> userRepository,
                IUnitOfWork uow)
            {
                _userRepository = userRepository;
                _uow = uow;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                Guards.NonNull(user, request.UserId);
                user.UpdateUser(request.FirstName, request.LastName, request.DateOfBirth);
                await _uow.SaveAsync();
                return Unit.Value;
            }
        }
    }
}
