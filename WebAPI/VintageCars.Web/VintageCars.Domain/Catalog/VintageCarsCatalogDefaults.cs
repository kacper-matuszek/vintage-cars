using Nop.Core.Caching;

namespace VintageCars.Domain.Catalog
{
    public static partial class VintageCarsCatalogDefaults
    {
        #region Category attributes

        public static CacheKey CategoryAttributeMappingsAllCacheKey => new CacheKey("Vintage.categoryattributemapping.all-{0}", CategoryAttributePrefixCacheKey);

        public static string CategoryAttributePrefixCacheKey => "Vintage.categoryattributemapping.";

        public static CacheKey CategoryAttributeValuesCacheKey => new CacheKey("Vintage.categoryattributevalue.all-{0}", CategoryAttributeValuesAllPrefixCacheKey);

        public static string CategoryAttributeValuesAllPrefixCacheKey => "Vintage.categoryattributevalue.all";

        #endregion

    }
}
