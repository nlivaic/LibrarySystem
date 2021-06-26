using AutoMapper;
using LibrarySystem.Common.Guards;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Application.UserContacts.Queries
{
    public class GetUserContactQuery : IRequest<UserContactGetModel>
    {
        public Guid UserContactId { get; set; }
        public Guid UserId { get; set; }

        class GetUserContactQueryHandler : IRequestHandler<GetUserContactQuery, UserContactGetModel>
        {
            private readonly IRepository<UserContact> _repository;
            private readonly IMapper _mapper;

            public GetUserContactQueryHandler(
                IRepository<UserContact> repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<UserContactGetModel> Handle(GetUserContactQuery request, CancellationToken cancellationToken)
            {
                var result = await _repository.GetSingleAsync(uc =>
                    uc.Id == request.UserContactId &&
                    uc.UserId == request.UserId);
                Guards.NonNull(result, request.UserContactId);
                return _mapper.Map<UserContactGetModel>(result);
            }
        }
    }
}
