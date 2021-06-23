using LibrarySystem.Application.Services.Sorting.Models;
using LibrarySystem.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LibrarySystem.Data.QueryableExtensions
{
    public static class Sorting
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IEnumerable<SortCriteria> sortCriteria)
            where T : BaseEntity<Guid>
         => sortCriteria.Any()
            ? query.OrderBy(string.Join(',', sortCriteria))
            : query.OrderBy(x => x.Id);
    }
}
