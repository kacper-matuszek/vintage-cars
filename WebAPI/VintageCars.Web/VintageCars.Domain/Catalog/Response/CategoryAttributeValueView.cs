using System;
using VintageCars.Domain.Base;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryAttributeValueView : BaseModelView
    {
        public bool IsPreselected { get; set; }
        public int DisplayOrder { get; set; }
    }
}
