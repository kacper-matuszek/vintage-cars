using System;
using System.Collections.Generic;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Queries
{
    public class GetCategoryAttributesWithValuesQuery : QueryBase<List<CategoryAttributeFullInfoView>>
    {
        public Guid CategoryId { get; set; }
    }
}
