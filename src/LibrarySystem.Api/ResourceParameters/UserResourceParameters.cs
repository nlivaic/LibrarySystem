using AutoMapper;
using LibrarySystem.Application.Models;
using LibrarySystem.Application.Services.Sorting.Models;
using System.Collections.Generic;

namespace LibrarySystem.Api.ResourceParameters
{
    public class UserResourceParameters : ISortable
    {
        public int PageSize { get; set; } = 3;
        public int MaximumPageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public string SearchQuery { get; set; }
        public IEnumerable<SortCriteria> SortBy { get; set; }

        class UserResourceParametersProfile : Profile
        {
            public UserResourceParametersProfile()
            {
                CreateMap<UserResourceParameters, UserQueryParameters>();
            }
        }
    }
}
