using System;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryAttributeValueView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPreselected { get; set; }
        public int DisplayOrder { get; set; }
    }
}
