using System;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Catalog.Queries
{
    public class GetCategoryAttributeValuesQuery : QueryPagedBase<PagedList<CategoryAttributeValueView>>
    {
        public Guid CategoryId { get; set; }
        public Guid CategoryAttributeId { get; set; }

        public GetCategoryAttributeValuesQuery()
        {
        }
        public GetCategoryAttributeValuesQuery(Guid categoryId, Guid categoryAttributeId, PagedRequest paged) 
            : base(paged)
        {
            CategoryId = categoryId;
            CategoryAttributeId = categoryAttributeId;
        }
    }
}
