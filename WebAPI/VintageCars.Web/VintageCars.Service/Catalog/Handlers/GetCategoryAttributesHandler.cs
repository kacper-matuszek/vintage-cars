using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Queries;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Common;
using VintageCars.Domain.Extensions;
using VintageCars.Service.Catalog.Services;

namespace VintageCars.Service.Catalog.Handlers
{
    public class GetCategoryAttributesHandler : IRequestHandler<GetCategoryAttributesQuery, PagedList<CategoryAttributeView>>
    {
        private readonly IExtendedCategoryService _extendedCategoryService;

        public GetCategoryAttributesHandler(IExtendedCategoryService extendedCategoryService)
        {
            _extendedCategoryService = extendedCategoryService;
        }

        public Task<PagedList<CategoryAttributeView>> Handle(GetCategoryAttributesQuery request, CancellationToken cancellationToken)
        {
            var pagedList = _extendedCategoryService.GetPagedCategoryAttributes(request.Paged.PageIndex, request.Paged.PageSize);
            return Task.FromResult(pagedList.ConvertPagedList<CategoryAttribute, CategoryAttributeView>());
        }
    }
}
