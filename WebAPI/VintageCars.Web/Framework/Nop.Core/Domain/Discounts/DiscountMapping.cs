using System;

namespace Nop.Core.Domain.Discounts
{
    public abstract partial class DiscountMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the discount identifier
        /// </summary>
        public Guid DiscountId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public abstract Guid EntityId { get; set; }
    }
}
