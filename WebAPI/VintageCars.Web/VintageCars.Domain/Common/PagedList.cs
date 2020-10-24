using System.Collections.Generic;
using System.Linq;

namespace VintageCars.Domain.Common
{
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            Source = source.ToList();
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public List<T> Source { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => TotalCount / PageSize;
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
