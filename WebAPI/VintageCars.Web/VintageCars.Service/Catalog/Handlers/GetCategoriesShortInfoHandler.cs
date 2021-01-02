using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Domain.Catalog;
using VintageCars.Domain.Catalog.Queries;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Common;
using VintageCars.Domain.Extensions;
using VintageCars.Service.Catalog.Services;
using VintageCars.Service.Infrastructure;

namespace VintageCars.Service.Catalog.Handlers
{
    public class GetCategoriesShortInfoHandler : IRequestHandler<GetCategoriesShortInfoQuery, PagedList<CategoryShortInfoView>>
    {
        private readonly IExtendedCategoryService _categoryService;
        private readonly IInfrastructureService _infrastructureService;

        public GetCategoriesShortInfoHandler(IExtendedCategoryService categoryService, IInfrastructureService infrastructureService)
        {
            _categoryService = categoryService;
            _infrastructureService = infrastructureService;
        }

        public Task<PagedList<CategoryShortInfoView>> Handle(GetCategoriesShortInfoQuery request, CancellationToken cancellationToken) 
            => Task.Run(() => _categoryService
                .GetAllCategories(string.Empty, _infrastructureService.Cache.Store.Id, request.Paged.PageIndex,
                    request.Paged.PageSize, overridePublished: true)
                .ConvertPagedList<Category, CategoryShortInfoView>(), cancellationToken);
    }
}
