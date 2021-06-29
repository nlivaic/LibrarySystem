using AutoMapper;
using FluentValidation;
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

        public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
        {
            public UserCreateRequestValidator()
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .WithMessage("First name cannot be empty.");
                RuleFor(x => x.LastName)
                    .NotEmpty()
                    .WithMessage("Last name cannot be empty.");
                RuleFor(x => x.DateOfBirth)
                    .LessThan(DateTime.UtcNow)
                    .WithMessage("User must have a date of birth earlier than today.");
            }
        }
    }
}
