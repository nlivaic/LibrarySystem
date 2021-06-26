using AutoMapper;
using LibrarySystem.Common.Guards;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.UserContacts.Commands
{
    public class DeleteUserContactCommand : IRequest<Unit>
    {
        public Guid UserContactId { get; set; }
        public Guid UserId { get; set; }

        public class DeleteUserContactCommandHandler : IRequestHandler<DeleteUserContactCommand, Unit>
        {
            private readonly IRepository<UserContact> _repository;
            private readonly IUnitOfWork _uow;

            public DeleteUserContactCommandHandler(
                IRepository<UserContact> repository,
                IUnitOfWork uow)
            {
                _repository = repository;
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteUserContactCommand request, CancellationToken cancellationToken)
            {
                var userContact = await _repository.GetSingleAsync(uc =>
                    uc.Id == request.UserContactId &&
                    uc.UserId == request.UserId);
                Guards.NonNull(userContact, request.UserContactId); 
                _repository.Delete(userContact);
                await _uow.SaveAsync();
                return Unit.Value;
            }
        }
    }
}
