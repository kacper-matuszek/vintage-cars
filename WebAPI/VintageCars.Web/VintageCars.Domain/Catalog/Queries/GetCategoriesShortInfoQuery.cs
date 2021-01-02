using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Catalog.Queries
{
    public class GetCategoriesShortInfoQuery : QueryPagedBase<PagedList<CategoryShortInfoView>>
    {
        public GetCategoriesShortInfoQuery(PagedRequest paged) : base(paged)
        {
        }
    }
}
