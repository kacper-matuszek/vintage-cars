using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Catalog.Queries;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Service.Catalog.Services;

namespace VintageCars.Service.Catalog.Handlers
{
    public class GetCategoryAttributesWithValuesHandler : IRequestHandler<GetCategoryAttributesWithValuesQuery, List<CategoryAttributeFullInfoView>>
    {
        private readonly IExtendedCategoryService _categoryService;

        public GetCategoryAttributesWithValuesHandler(IExtendedCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Task<List<CategoryAttributeFullInfoView>> Handle(GetCategoryAttributesWithValuesQuery request, CancellationToken cancellationToken)
            => Task.Run(() =>
            {
                var attributes = _categoryService.GetAllCategoryAttributesByCategoryId(request.CategoryId);
                var attributeMappings = _categoryService.GetCategoryAttributeMappingsByCategoryId(request.CategoryId);
                
                var mappedAttributes = AutoMapperConfiguration.Mapper.Map<List<CategoryAttributeFullInfoView>>(attributes);
                mappedAttributes.ForEach(attribute =>
                {
                    var attributeMapping = attributeMappings.FirstOrDefault(x => x.CategoryAttributeId == attribute.Id);

                    attribute = AutoMapperConfiguration.Mapper.Map(attributeMapping, attribute);
                    attribute.Values = AutoMapperConfiguration.Mapper.Map<IEnumerable<CategoryAttributeValueView>>(_categoryService.GetCategoryAttributeValues(attributeMapping.Id));
                });

                return mappedAttributes;
            }, cancellationToken);
    }
}
