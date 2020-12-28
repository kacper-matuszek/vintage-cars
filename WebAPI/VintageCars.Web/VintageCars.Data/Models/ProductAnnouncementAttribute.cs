using System;
using Nop.Core;

namespace VintageCars.Data.Models
{
    public class ProductAnnouncementAttribute : BaseEntity
    {
        public Guid CategoryAttributeId { get; set; }
        public Guid CategoryAttributeValueId { get; set; }
        public string Value { get; set; }
    }
}
