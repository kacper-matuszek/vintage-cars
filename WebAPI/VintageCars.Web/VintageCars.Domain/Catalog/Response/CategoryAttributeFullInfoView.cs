using System.Collections.Generic;
using Nop.Core.Domain.Catalog;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryAttributeFullInfoView : CategoryAttributeView
    {
        public AttributeControlType AttributeControlType { get; set; }
        public IEnumerable<CategoryAttributeValueView> Values { get; set; }
    }
}
