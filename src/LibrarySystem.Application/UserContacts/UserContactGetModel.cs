using AutoMapper;
using LibrarySystem.Core.Entities;

namespace LibrarySystem.Application.UserContacts
{
    public class UserContactGetModel
    {
        public string ContactNumber { get; private set; }

        class UserContactGetModelProfile : Profile
        {
            public UserContactGetModelProfile()
            {
                CreateMap<UserContact, UserContactGetModel>();
            }
        }
    }
}