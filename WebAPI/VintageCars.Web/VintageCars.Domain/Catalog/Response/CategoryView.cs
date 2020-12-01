using System;
using System.Collections.Generic;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool IsArchival { get; set; }
        public IEnumerable<CategoryAttributeMappingView> Attributes { get; set; }
    }
}
