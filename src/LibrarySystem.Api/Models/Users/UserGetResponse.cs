using AutoMapper;
using LibrarySystem.Api.Models.UserContacts;
using LibrarySystem.Application.Users.Queries;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Api.Models.Users
{
    public class UserGetResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public IEnumerable<UserContactGetResponse> UserContacts { get; set; }

        public class UserGetResponseProfile : Profile
        {
            public UserGetResponseProfile()
            {
                CreateMap<UserGetModel, UserGetResponse>()
                    .ForMember(dest => dest.Age,
                        opts => opts.MapFrom(src => DateTime.UtcNow.Date.Year - src.DateOfBirth.Year));
            }
        }
    }
}
