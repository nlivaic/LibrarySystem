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

namespace LibrarySystem.Application.UserContacts.Commands
{
    public class UpdateUserContactCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid UserContactId { get; set; }
        public string ContactNumber { get; set; }

        class UpdateUserContactCommandHandler : IRequestHandler<UpdateUserContactCommand, Unit>
        {
            private readonly IUserRepository _userRepository;
            private readonly IRepository<UserContact> _repository;
            private readonly IUnitOfWork _uow;

            public UpdateUserContactCommandHandler(
                IUserRepository userRepository,
                IRepository<UserContact> repository,
                IUnitOfWork uow)
            {
                _userRepository = userRepository;
                _repository = repository;
                _uow = uow;
            }

            public async Task<Unit> Handle(UpdateUserContactCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetWithUserContacts(request.UserId);
                user.UpdateContactAsync(request.UserContactId, request.ContactNumber);


                //var userContact = await _repository.GetSingleAsync(uc =>
                //    uc.Id == request.UserContactId &&
                //    uc.UserId == request.UserId);
                //Guards.NonNull(userContact, request.UserContactId);
                //userContact.Update(request.ContactNumber);
                await _uow.SaveAsync();
                return Unit.Value;
            }
        }
    }
}
