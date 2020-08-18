using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;

namespace Nop.Service.Extensions
{
    public static class QueryableExtension
    {
        private static IStaticCacheManager CacheManager => EngineContext.Current.Resolve<IStaticCacheManager>();

        /// <summary>
        /// Gets a cached first element of a sequence, or a default value
        /// </summary>
        /// <typeparam name="T">The type of the elements of source</typeparam>
        /// <param name="query">Elements of source to put on cache</param>
        /// <param name="cacheKey">Cache key</param>
        /// <returns>Cached first element or default value</returns>
        public static T ToCachedFirstOrDefault<T>(this IQueryable<T> query, CacheKey cacheKey)
        {
            return cacheKey == null
                ? query.FirstOrDefault()
                : CacheManager.Get(cacheKey, query.FirstOrDefault);
        }
    }
}
