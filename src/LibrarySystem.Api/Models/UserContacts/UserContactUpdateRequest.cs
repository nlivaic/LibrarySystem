using AutoMapper;
using FluentValidation;
using LibrarySystem.Application.UserContacts.Commands;

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

        public class UserContactUpdateRequestValidator : AbstractValidator<UserContactUpdateRequest>
        {
            public UserContactUpdateRequestValidator()
            {
                RuleFor(x => x.ContactNumber)
                    .NotEmpty()
                    .WithMessage("Contact Number cannot be empty.");
            }
        }
    }
}
