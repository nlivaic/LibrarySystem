using AutoMapper;
using FluentValidation;
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

        public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
        {
            public UserUpdateRequestValidator()
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
