using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Catalog.Queries
{
    public class GetCategoriesQuery : QueryPagedBase<PagedList<CategoryView>>
    {
        public GetCategoriesQuery()
        {
        }
        public GetCategoriesQuery(PagedRequest paged) : base(paged)
        {
        }
    }
}
