using System;
using System.Collections.Generic;
using VintageCars.Domain.Catalog.Request;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Commands
{
    public class LinkCategoryAttributeValueCommand : CommandBase
    {
        public Guid CategoryId { get; set; }
        public IEnumerable<CategoryAttributeValueRequest> CategoryAttributeValues { get; set; }
    }
}
