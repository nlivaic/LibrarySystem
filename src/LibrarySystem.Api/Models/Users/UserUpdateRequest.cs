using AutoMapper;
using LibrarySystem.Application.Users.Commands;
using System;

namespace LibrarySystem.Api.Models.Users
{
    public class UserUpdateRequest
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        class UserUpdateRequestProfile : Profile
        {
            public UserUpdateRequestProfile()
            {
                CreateMap<UserUpdateRequest, UpdateUserCommand>();
            }
        }
    }
}
