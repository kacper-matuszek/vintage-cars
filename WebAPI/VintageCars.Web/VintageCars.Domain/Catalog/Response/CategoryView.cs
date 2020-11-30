using System.Collections.Generic;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<CategoryAttributeMappingView> Attributes { get; set; }
    }
}
