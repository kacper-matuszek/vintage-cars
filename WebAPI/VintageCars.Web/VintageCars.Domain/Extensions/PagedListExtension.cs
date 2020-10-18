using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Infrastructure.Mapper;

namespace VintageCars.Domain.Extensions
{
    public static class PagedListExtension
    {
        public static IPagedList<C> ConvertPagedList<T, C>(this IPagedList<T> pagedList)
        {
            var collectionPagedList = pagedList.Select(x => x);
            var mappedCollection = AutoMapperConfiguration.Mapper.Map<IEnumerable<C>>(collectionPagedList);
            return new PagedList<C>(mappedCollection, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount);
        }
    }
}
