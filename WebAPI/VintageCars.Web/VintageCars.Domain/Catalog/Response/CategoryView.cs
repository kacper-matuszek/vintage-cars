using System;
using System.Collections.Generic;
using VintageCars.Domain.Base;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryView : BaseModelView
    {
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool IsArchival { get; set; }
        public IEnumerable<CategoryAttributeMappingView> Attributes { get; set; }
    }
}
