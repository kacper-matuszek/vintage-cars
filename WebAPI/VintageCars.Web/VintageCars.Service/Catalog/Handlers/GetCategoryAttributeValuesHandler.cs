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
    public class GetCategoryAttributeValuesHandler : IRequestHandler<GetCategoryAttributeValuesQuery, PagedList<CategoryAttributeValueView>>
    {
        private readonly IExtendedCategoryService _extendedcategoryservice;

        public GetCategoryAttributeValuesHandler(IExtendedCategoryService extendedcategoryservice)
        {
            _extendedcategoryservice = extendedcategoryservice;
        }

        public Task<PagedList<CategoryAttributeValueView>> Handle(GetCategoryAttributeValuesQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var paged = _extendedcategoryservice.GetPagedCategoryAttributeValues(request.CategoryId,
                    request.CategoryAttributeId, request.Paged.PageIndex, request.Paged.PageSize);
                return paged.ConvertPagedList<CategoryAttributeValue, CategoryAttributeValueView>();
            }, cancellationToken);
        }
    }
}
