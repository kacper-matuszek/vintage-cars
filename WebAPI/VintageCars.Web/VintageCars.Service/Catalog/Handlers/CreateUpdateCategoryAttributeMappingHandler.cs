using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Service.Catalog.Services;

namespace VintageCars.Service.Catalog.Handlers
{
    public class CreateUpdateCategoryAttributeMappingHandler : IRequestHandler<CreateUpdateCategoryAttributeMappingCommand, Unit>
    {
        private readonly IExtendedCategoryService _extendedCategoryService;

        public CreateUpdateCategoryAttributeMappingHandler(IExtendedCategoryService extendedCategoryService)
        {
            _extendedCategoryService = extendedCategoryService;
        }

        public Task<Unit> Handle(CreateUpdateCategoryAttributeMappingCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue)
            {
                var categoryAttributeMapping = AutoMapperConfiguration.Mapper.Map<CategoryAttributeMapping>(request);
                _extendedCategoryService.InsertCategoryAttributeMapping(categoryAttributeMapping);

                return Unit.Task;
            }

            var updateCategoryAttributeMapping = _extendedCategoryService.GetCategoryAttributeMapping(request.Id.Value);
            updateCategoryAttributeMapping = AutoMapperConfiguration.Mapper.Map(request, updateCategoryAttributeMapping);
            _extendedCategoryService.UpdateCategoryAttributeMapping(updateCategoryAttributeMapping);

            return Unit.Task;
        }
    }
}
