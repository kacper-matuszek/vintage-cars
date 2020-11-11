using System;
using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace VintageCars.Data.Models
{
    public class CategoryAttributeMapping : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Guid CategoryAttributeId { get; set; }
        public int AttributeControlTypeId { get; set; }
        public int DisplayOrder { get; set; }

        public AttributeControlType AttributeControlType
        {
            get => (AttributeControlType) AttributeControlTypeId;
            set => AttributeControlTypeId = (int)value;
        }
    }
}
