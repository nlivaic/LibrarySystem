using System.Collections.Generic;

namespace LibrarySystem.Application.Services.Sorting.Models
{
    public interface ISortable
    {
        IEnumerable<SortCriteria> SortBy { get; set; }
    }
}
