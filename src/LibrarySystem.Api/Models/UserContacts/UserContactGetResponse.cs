using AutoMapper;
using LibrarySystem.Application.UserContacts;

namespace LibrarySystem.Api.Models.UserContacts
{
    public class UserContactGetResponse
    {
        public string ContactNumber { get; private set; }

        public class UserContactGetResponseProfile : Profile
        {
            public UserContactGetResponseProfile()
            {
                CreateMap<UserContactGetModel, UserContactGetResponse>();
            }
        }
    }
}