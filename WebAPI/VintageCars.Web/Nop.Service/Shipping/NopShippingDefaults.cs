using Nop.Core.Caching;

namespace Nop.Service.Shipping
{
    public static partial class NopShippingDefaults
    {
        #region Warehouses

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static CacheKey WarehousesAllCacheKey => new CacheKey("Nop.warehouse.all");

        #endregion
    }
}
