using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Catalog.Queries
{
    public class GetCategoryQuery : QueryPagedBase<PagedList<CategoryView>>
    {
        public GetCategoryQuery()
        {
        }
        public GetCategoryQuery(PagedRequest paged) : base(paged)
        {
        }
    }
}
