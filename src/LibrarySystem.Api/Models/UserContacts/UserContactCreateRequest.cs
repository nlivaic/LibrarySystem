using AutoMapper;
using FluentValidation;
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

        public class UserContactCreateRequestValidator : AbstractValidator<UserContactCreateRequest>
        {
            public UserContactCreateRequestValidator()
            {
                RuleFor(x => x.ContactNumber)
                    .NotEmpty()
                    .WithMessage("Contact Number cannot be empty.");
            }
        }
    }
}
