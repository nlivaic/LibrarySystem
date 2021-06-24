using LibrarySystem.Application.Services.Sorting.Models;
using System.Collections.Generic;

namespace LibrarySystem.Application.Models
{
    public class UserQueryParameters : ISortable
    {
        public int PageSize { get; set; } = 3;
        public int MaximumPageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public string SearchQuery { get; set; }
        public IEnumerable<SortCriteria> SortBy { get; set; }
    }
}
