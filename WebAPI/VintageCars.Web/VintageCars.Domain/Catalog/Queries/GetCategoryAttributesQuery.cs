using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Catalog.Queries
{
    public class GetCategoryAttributesQuery : QueryPagedBase<PagedList<CategoryAttributeView>>
    {
        public GetCategoryAttributesQuery()
        {
        }

        public GetCategoryAttributesQuery(PagedRequest paged) : base(paged)
        {
        }
    }
}
