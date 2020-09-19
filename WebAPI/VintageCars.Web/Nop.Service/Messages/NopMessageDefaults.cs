using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Caching;

namespace Nop.Service.Messages
{
    public static partial class NopMessageDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : template name
        /// {1} : store ID
        /// </remarks>
        public static CacheKey MessageTemplatesByNameCacheKey => new CacheKey("Nop.messagetemplate.name-{0}-{1}", MessageTemplatesByNamePrefixCacheKey);

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : template name
        /// </remarks>
        public static string MessageTemplatesByNamePrefixCacheKey => "Nop.messagetemplate.name-{0}";
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : store ID
        /// </remarks>
        public static CacheKey EmailAccountsAllCacheKey => new CacheKey("Nop.emailaccounts.all");
    }
}
