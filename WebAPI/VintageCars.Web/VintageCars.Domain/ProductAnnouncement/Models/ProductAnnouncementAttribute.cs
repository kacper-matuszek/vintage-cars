using System;
using System.Collections.Generic;
using System.Text;
using VintageCars.Domain.Base;

namespace VintageCars.Domain.ProductAnnouncement.Models
{
    public class ProductAnnouncementAttribute : IBusinessEntity
    {
        public Guid? Id { get; set; }
        public Guid CategoryAttributeId { get; set; }
        public Guid? CategoryAttributeValueId { get; set; }
        public string Value { get; set; }
    }
}
