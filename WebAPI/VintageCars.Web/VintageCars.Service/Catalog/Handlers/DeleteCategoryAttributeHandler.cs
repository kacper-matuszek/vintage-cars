using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Service.Localization;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Exceptions;
using VintageCars.Service.Catalog.Services;

namespace VintageCars.Service.Catalog.Handlers
{
    public class DeleteCategoryAttributeHandler : IRequestHandler<DeleteCategoryAttributeCommand, Unit>
    {
        private readonly IExtendedCategoryService _extendedCategoryService;
        private readonly ILocalizationService _localizationService;

        public DeleteCategoryAttributeHandler(IExtendedCategoryService extendedCategoryService, ILocalizationService localizationService)
        {
            _extendedCategoryService = extendedCategoryService;
            _localizationService = localizationService;
        }

        public Task<Unit> Handle(DeleteCategoryAttributeCommand request, CancellationToken cancellationToken)
        {
            var categoryAttribute = _extendedCategoryService.GetCategoryAttribute(request.Id)
                                    ?? throw new ValidationException(
                                        _localizationService.GetResource("DeleteCategoryAttribute.NotExists.Validation"));
            _extendedCategoryService.DeleteCategoryAttribute(categoryAttribute);
            return Unit.Task;
        }
    }
}
