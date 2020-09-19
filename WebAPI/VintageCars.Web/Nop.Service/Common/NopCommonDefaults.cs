using Nop.Core.Caching;

namespace Nop.Service.Common
{
    public static  partial class NopCommonDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : key group
        /// </remarks>
        public static CacheKey GenericAttributeCacheKey => new CacheKey("Nop.genericattribute.{0}-{1}");
    }
}
