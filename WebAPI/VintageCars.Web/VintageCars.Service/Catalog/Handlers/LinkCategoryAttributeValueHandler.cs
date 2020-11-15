using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Rest;
using Nop.Core.Infrastructure.Mapper;
using Nop.Service.Localization;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Service.Catalog.Services;

namespace VintageCars.Service.Catalog.Handlers
{
    public class LinkCategoryAttributeValueHandler : IRequestHandler<LinkCategoryAttributeValueCommand, Unit>
    {
        private readonly IExtendedCategoryService _extendedCategoryService;
        private readonly ILocalizationService _localizationService;

        public LinkCategoryAttributeValueHandler(IExtendedCategoryService extendedCategoryService, ILocalizationService localizationService)
        {
            _extendedCategoryService = extendedCategoryService;
            _localizationService = localizationService;
        }

        public Task<Unit> Handle(LinkCategoryAttributeValueCommand request, CancellationToken cancellationToken)
        {
            Valid(request);
            foreach (var categoryAttributeValue in request.CategoryAttributeValues)
            {
                if (categoryAttributeValue.Id.HasValue)
                {
                    var updateCategoryAttributeValue = _extendedCategoryService.GetCategoryAttributeValueById(categoryAttributeValue.Id.Value);
                    updateCategoryAttributeValue = AutoMapperConfiguration.Mapper.Map(categoryAttributeValue, updateCategoryAttributeValue);
                    _extendedCategoryService.UpdateCategoryAttributeValue(updateCategoryAttributeValue);

                    continue;
                }

                var categoryAttribute = _extendedCategoryService
                    .GetCategoryAttributeMappingsByCategoryId(request.CategoryId).First(am => am.CategoryAttributeId == categoryAttributeValue.CategoryAttributeId);
                var addAttributeValue = AutoMapperConfiguration.Mapper.Map<CategoryAttributeValue>(categoryAttributeValue);
                addAttributeValue.CategoryAttributeMappingId = categoryAttribute.Id;
                _extendedCategoryService.InsertCategoryAttributeValue(addAttributeValue);
            }

            return Unit.Task;
        }

        private void Valid(LinkCategoryAttributeValueCommand request)
        {
            var categoryId = request.CategoryId;
            var categoryAttributes = _extendedCategoryService.GetCategoryAttributeMappingsByCategoryId(categoryId);
            var attributesNotLinked = request.CategoryAttributeValues.Select(x => x.CategoryAttributeId).Distinct()
                .Except(categoryAttributes.Select(x => x.CategoryAttributeId));

            if (!attributesNotLinked?.Any() ?? true) return;

            var categoryAttributeId = request.CategoryAttributeValues.First(cav => categoryAttributes.All(ca => ca.Id != cav.CategoryAttributeId)).CategoryAttributeId;
            var categoryAttributeName = _extendedCategoryService.GetCategoryAttribute(categoryAttributeId).Name;
            var message =
                _localizationService.GetResource(
                    "LinkCategoryAttributeValue.CategoryAttributeNotLinkedWithCategory.Validation");
            throw new ValidationException(string.Format(message, categoryAttributeName));
        }
    }
}
