using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Catalog.Queries
{
    public class GetCategoryAttributesCommand : QueryPagedBase<PagedList<CategoryAttributeView>>
    {
        public GetCategoryAttributesCommand()
        {
        }

        public GetCategoryAttributesCommand(PagedRequest paged) : base(paged)
        {
        }
    }
}
