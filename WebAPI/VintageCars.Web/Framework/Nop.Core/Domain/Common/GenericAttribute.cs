using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Common
{
    /// <summary>
    /// Represents a generic attribute
    /// </summary>
    public partial class GenericAttribute : BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// Gets or sets the key group
        /// </summary>
        public string KeyGroup { get; set; }

        /// <summary>
        /// Gets or sets the key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the store identifier
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// Gets or sets the created or updated date
        /// </summary>
        public DateTime? CreatedOrUpdatedDateUTC { get; set; }
    }
}
