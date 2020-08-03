using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Caching;

namespace Nop.Service.Store
{
    public static partial class NopStoreDefaults
    {
        #region Stores

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static CacheKey StoresAllCacheKey => new CacheKey("Nop.stores.all");

        #endregion
    }
}
