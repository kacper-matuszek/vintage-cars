using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Rest;
using Nop.Service.Localization;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Service.Catalog.Services;

namespace VintageCars.Service.Catalog.Handlers
{
    public class DeleteCategoryAttributeValueHandler : IRequestHandler<DeleteCategoryAttributeValueCommand, Unit>
    {
        private readonly IExtendedCategoryService _extendedCategoryService;
        private readonly ILocalizationService _localizationService;

        public DeleteCategoryAttributeValueHandler(IExtendedCategoryService extendedCategoryService, ILocalizationService localizationService)
        {
            _extendedCategoryService = extendedCategoryService;
            _localizationService = localizationService;
        }

        public Task<Unit> Handle(DeleteCategoryAttributeValueCommand request, CancellationToken cancellationToken)
        {
            var categoryAttributeValue = _extendedCategoryService.GetCategoryAttributeValueById(request.Id);
            if(categoryAttributeValue is null)
                throw new ValidationException(_localizationService.GetResource("DeleteCategoryAttributeValue.NotExists.Validation"));

            _extendedCategoryService.DeleteCategoryAttributeValue(categoryAttributeValue);
            return Unit.Task;
        }
    }
}
