using System;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a product-product tag mapping class
    /// </summary>
    public partial class ProductProductTagMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product tag identifier
        /// </summary>
        public Guid ProductTagId { get; set; }
    }
}