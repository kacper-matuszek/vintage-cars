using System;
using Nop.Core.Domain.Catalog;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryAttributeMappingView : CategoryAttributeView
    {
        public AttributeControlType AttributeControlType { get; set; }

        public Guid CategoryAttributeId { get;set; }
    }
}
