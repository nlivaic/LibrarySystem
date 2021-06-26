using AutoMapper;
using LibrarySystem.Application.UserContacts.Commands;
using System;

namespace LibrarySystem.Api.Models.UserContacts
{
    public class UserContactUpdateRequest
    {
        public string ContactNumber { get; set; }

        class UserContactUpdateRequestProfile : Profile
        {
            public UserContactUpdateRequestProfile()
            {
                CreateMap<UserContactUpdateRequest, UpdateUserContactCommand>();
            }
        }
    }
}
