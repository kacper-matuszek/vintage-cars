using System;
using Nop.Core;

namespace VintageCars.Data.Models
{
    public class CategoryAttributeValue : BaseEntity
    {
        public string Name { get; set; }
        public Guid CategoryAttributeMappingId { get; set; }
        public bool IsPreSelected { get; set; }
        public int DisplayOrder { get; set; }
    }
}
