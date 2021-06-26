using AutoMapper;
using LibrarySystem.Application.UserContacts;
using LibrarySystem.Core.Entities;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Application.Users.Queries
{
    public class UserGetModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<UserContactGetModel> UserContacts { get; set; }

        class UserGetModelProfile : Profile
        {
            public UserGetModelProfile()
            {
                CreateMap<User, UserGetModel>()
                    .ForMember(dest => dest.DateOfBirth,
                        opts => opts.MapFrom(src => src.DateOfBirth.Date));
            }
        }
    }
}
