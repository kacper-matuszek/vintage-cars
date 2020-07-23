using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;

namespace Nop.Service.Caching.Extensions
{
    public static class IQueryableExtensions
    {
        private static IStaticCacheManager CacheManager => EngineContext.Current.Resolve<IStaticCacheManager>();

        /// <summary>
        /// Gets a cached list
        /// </summary>
        /// <typeparam name="T">The type of the elements of source</typeparam>
        /// <param name="query">Elements of source to put on cache</param>
        /// <param name="cacheKey">Cache key</param>
        /// <returns>Cached list</returns>
        public static IList<T> ToCachedList<T>(this IQueryable<T> query, CacheKey cacheKey)
        {
            return cacheKey == null ? query.ToList() : CacheManager.Get(cacheKey, query.ToList);
        }
    }
}
