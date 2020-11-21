using System;

namespace VintageCars.Domain.Catalog.Response
{
    public class CategoryAttributeView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
