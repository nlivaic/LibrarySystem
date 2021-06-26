using AutoMapper;
using LibrarySystem.Application.UserContacts;
using System;

namespace LibrarySystem.Api.Models.UserContacts
{
    public class UserContactGetResponse
    {
        public Guid Id { get; set; }
        public string ContactNumber { get; private set; }

        class UserContactGetResponseProfile : Profile
        {
            public UserContactGetResponseProfile()
            {
                CreateMap<UserContactGetModel, UserContactGetResponse>();
            }
        }
    }
}