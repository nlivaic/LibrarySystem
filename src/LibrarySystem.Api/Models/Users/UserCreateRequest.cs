using AutoMapper;
using LibrarySystem.Application.Users.Commands;
using System;

namespace LibrarySystem.Api.Models.Users
{
    public class UserCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        class UserCreateRequestProfile : Profile
        {
            public UserCreateRequestProfile()
            {
                CreateMap<UserCreateRequest, CreateUserCommand>();
            }
        }
    }
}
