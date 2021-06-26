using AutoMapper;
using LibrarySystem.Application.UserContacts.Commands;
using System;

namespace LibrarySystem.Api.Models.UserContacts
{
    public class UserContactCreateRequest
    {
        public Guid UserId { get; set; }
        public string ContactNumber { get; set; }

        class UserContactCreateRequestProfile : Profile
        {
            public UserContactCreateRequestProfile()
            {
                CreateMap<UserContactCreateRequest, CreateUserContactCommand>();
            }
        }
    }
}
