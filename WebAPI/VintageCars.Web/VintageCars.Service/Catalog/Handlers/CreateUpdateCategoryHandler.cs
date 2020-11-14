using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure.Mapper;
using Nop.Service.Catalog;
using VintageCars.Domain.Catalog.Commands;

namespace VintageCars.Service.Catalog.Handlers
{
    public class CreateUpdateCategoryHandler : IRequestHandler<CreateUpdateCategoryCommand, Unit>
    {
        private readonly ICategoryService _categoryService;

        public CreateUpdateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Task<Unit> Handle(CreateUpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue)
            {
                var category = AutoMapperConfiguration.Mapper.Map<Category>(request);
                _categoryService.InsertCategory(category);

                return Unit.Task;
            }

            var updateCategory = _categoryService.GetCategoryById(request.Id.Value);
            updateCategory = AutoMapperConfiguration.Mapper.Map(request, updateCategory);
            _categoryService.UpdateCategory(updateCategory);

            return Unit.Task;
        }
    }
}
