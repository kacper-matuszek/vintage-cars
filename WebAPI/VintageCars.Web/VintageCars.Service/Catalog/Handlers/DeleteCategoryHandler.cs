using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Service.Customer;
using Nop.Service.Localization;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Exceptions;
using VintageCars.Service.Catalog.Services;
using VintageCars.Service.Infrastructure;

namespace VintageCars.Service.Catalog.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IExtendedCategoryService _extendedCategoryService;
        private readonly ILocalizationService _localizationService;
        private readonly IInfrastructureService _infrastructureService;
        private readonly ICustomerService _customerService;

        public DeleteCategoryHandler(IExtendedCategoryService extendedCategoryService, ILocalizationService localizationService, IInfrastructureService infrastructureService, ICustomerService customerService)
        {
            _extendedCategoryService = extendedCategoryService;
            _localizationService = localizationService;
            _infrastructureService = infrastructureService;
            _customerService = customerService;
        }

        public Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _extendedCategoryService.GetCategoryById(request.Id) ??
                           throw new ValidationException(
                               _localizationService.GetResource("DeleteCategory.NotExists.Validation"));
            var storeId = _infrastructureService.Cache.Store.Id;
            var customer = _customerService.GetCustomer(request.UserId);
            
            _extendedCategoryService.DeleteCategory(category, customer, storeId);

            return Unit.Task;
        }
    }
}
