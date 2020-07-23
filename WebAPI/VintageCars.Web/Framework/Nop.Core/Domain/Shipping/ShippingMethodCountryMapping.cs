using System;

namespace Nop.Core.Domain.Shipping
{
    /// <summary>
    /// Represents a shipping method-country mapping class
    /// </summary>
    public partial class ShippingMethodCountryMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the shipping method identifier
        /// </summary>
        public Guid ShippingMethodId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public Guid CountryId { get; set; }
    }
}