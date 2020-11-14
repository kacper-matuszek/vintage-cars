using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Service.Catalog.Services;

namespace VintageCars.Service.Catalog.Handlers
{
    public class CreateUpdateCategoryAttributeHandler : IRequestHandler<CreateUpdateCategoryAttributeCommand, Unit>
    {
        private readonly IExtendedCategoryService _extendedCategoryService;

        public CreateUpdateCategoryAttributeHandler(IExtendedCategoryService extendedCategoryService)
        {
            _extendedCategoryService = extendedCategoryService;
        }

        public Task<Unit> Handle(CreateUpdateCategoryAttributeCommand request, CancellationToken cancellationToken)
        {
            CategoryAttribute categoryAttribute;
            if (request.Id.HasValue)
            {
                categoryAttribute = _extendedCategoryService.GetCategoryAttribute(request.Id.Value);
                categoryAttribute = AutoMapperConfiguration.Mapper.Map<CreateUpdateCategoryAttributeCommand, CategoryAttribute>(request, categoryAttribute);
                _extendedCategoryService.UpdateCategoryAttribute(categoryAttribute);
                
                return Unit.Task;
            }

            categoryAttribute = AutoMapperConfiguration.Mapper.Map<CategoryAttribute>(request);
            _extendedCategoryService.InsertCategoryAttribute(categoryAttribute);

            return Unit.Task;
        }
    }
}
