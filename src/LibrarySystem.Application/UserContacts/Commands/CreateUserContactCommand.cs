using AutoMapper;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Common.Guards;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.UserContacts.Commands
{
    public class CreateUserContactCommand : IRequest<UserContactGetModel>
    {
        public Guid UserId { get; set; }
        public string ContactNumber { get; set; }

        class CreateUserContactCommandHandler : IRequestHandler<CreateUserContactCommand, UserContactGetModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IRepository<UserContact> _userContactRepository;
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public CreateUserContactCommandHandler(
                IUserRepository userRepository,
                IRepository<UserContact> userContactRepository,
                IUnitOfWork uow,
                IMapper mapper)
            {
                _userRepository = userRepository;
                _userContactRepository = userContactRepository;
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<UserContactGetModel> Handle(CreateUserContactCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetWithUserContacts(request.UserId);
                Guards.NonNull(user, request.UserId);
                var userContact = new UserContact(request.ContactNumber);
                user.AddContact(userContact);
                await _userContactRepository.AddAsync(userContact);
                await _uow.SaveAsync();
                return _mapper.Map<UserContactGetModel>(userContact);
            }
        }
    }
}
