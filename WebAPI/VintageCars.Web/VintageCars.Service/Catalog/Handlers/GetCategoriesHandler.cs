using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Queries;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Common;
using VintageCars.Domain.Extensions;
using VintageCars.Service.Catalog.Services;
using VintageCars.Service.Infrastructure;

namespace VintageCars.Service.Catalog.Handlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, PagedList<CategoryView>>
    {
        private readonly IExtendedCategoryService _categoryService;
        private readonly IInfrastructureService _infrastructureService;

        public GetCategoriesHandler(IExtendedCategoryService categoryService, IInfrastructureService infrastructureService)
        {
            _categoryService = categoryService;
            _infrastructureService = infrastructureService;
        }

        public Task<PagedList<CategoryView>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var storeId = _infrastructureService.Cache.Store.Id;
            var categories = _categoryService
                .GetAllCategories(string.Empty, storeId, request.Paged.PageIndex, request.Paged.PageSize,
                    showHidden: true)
                .ConvertPagedList<Category, CategoryView>();

            categories.Source.ForEach(c =>
            {
                var categoryAttributeMappings = _categoryService.GetCategoryAttributeMappingsByCategoryId(c.Id);
                var categoryAttributes = _categoryService.GetAllCategoryAttributesByCategoryId(c.Id);
                c.Attributes = AutoMapperConfiguration.Mapper.Map<IEnumerable<CategoryAttributeMappingView>>(categoryAttributes); 
                c.Attributes.ToList().ForEach(cAttribute => AutoMapperConfiguration.Mapper.Map(categoryAttributeMappings.FirstOrDefault(x => x.CategoryAttributeId == cAttribute.Id), cAttribute));
            });
            return Task.FromResult(categories);
        }
    }
}
