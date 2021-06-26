using AutoMapper;
using LibrarySystem.Core.Entities;
using System;

namespace LibrarySystem.Application.UserContacts
{
    public class UserContactGetModel
    {
        public Guid Id { get; set; }
        public string ContactNumber { get; set; }

        class UserContactGetModelProfile : Profile
        {
            public UserContactGetModelProfile()
            {
                CreateMap<UserContact, UserContactGetModel>();
            }
        }
    }
}