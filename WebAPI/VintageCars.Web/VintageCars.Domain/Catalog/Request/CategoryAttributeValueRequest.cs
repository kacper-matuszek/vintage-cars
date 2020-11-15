using System;
using VintageCars.Domain.Base;

namespace VintageCars.Domain.Catalog.Request
{
    public class CategoryAttributeValueRequest : IBusinessEntity
    {
        public Guid? Id { get; set; }
        public Guid CategoryAttributeId { get; set; }
        public string Name { get; set; }
        public bool IsPreSelected { get; set; }
        public int DisplayOrder { get; set; }
    }
}
