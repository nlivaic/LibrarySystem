using AutoMapper;
using LibrarySystem.Api.Helpers;
using LibrarySystem.Api.Models.Users;
using LibrarySystem.Application.Models;
using LibrarySystem.Application.Services.Sorting;
using LibrarySystem.Application.Services.Sorting.Models;
using LibrarySystem.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace LibrarySystem.Api.ResourceParameters
{
    public class UserResourceParameters : ISortable
    {
        public int PageSize { get; set; } = 3;
        [BindNever]
        public int MaximumPageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public string SearchQuery { get; set; }
        [BindProperty(BinderType = typeof(ArrayModelBinder))]
        public IEnumerable<SortCriteria> SortBy { get; set; }

        class UserResourceParametersProfile : Profile
        {
            public UserResourceParametersProfile()
            {
                CreateMap<UserResourceParameters, UserQueryParameters>()
                    .ForSortableMembers<UserGetResponse, UserGetModel, UserResourceParameters, UserQueryParameters>();
            }
        }
    }
}
