using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Caching;

namespace Nop.Service.Customer
{
    public static class NopCustomerServicesDefaults
    {
        /// <summary>
        /// Gets a password salt key size
        /// </summary>
        public static int PasswordSaltKeySize => 5;

        /// <summary>
        /// Gets a default hash format for customer password
        /// </summary>
        public static string DefaultHashedPasswordFormat => "SHA512";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer identifier
        /// {1} : show hidden
        /// </remarks>
        public static CacheKey CustomerRolesCacheKey => new CacheKey("Nop.customer.customerrole-{0}-{1}", CustomerCustomerRolesPrefixCacheKey);
        
        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string CustomerCustomerRolesPrefixCacheKey => "Nop.customer.customerrole";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : system name
        /// </remarks>
        public static CacheKey CustomerRolesBySystemNameCacheKey => new CacheKey("Nop.customerrole.systemname-{0}", CustomerRolesPrefixCacheKey);
        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string CustomerRolesPrefixCacheKey => "Nop.customerrole.";


    }
}
