using System;
using Nop.Core;

namespace VintageCars.Data.Models
{
    public class ProductAnnouncementAttributeMapping : BaseEntity
    {
        public Guid ProductAnnouncementId { get; set; }
        public Guid ProductAnnouncementAttributeId { get; set; }
    }
}
