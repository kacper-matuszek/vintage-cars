using System;
using System.Collections.Generic;
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
        private readonly IMediator _mediator;

        public CreateUpdateCategoryHandler(ICategoryService categoryService, IMediator mediator)
        {
            _categoryService = categoryService;
            _mediator = mediator;
        }

        public Task<Unit> Handle(CreateUpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue)
            {
                var category = AutoMapperConfiguration.Mapper.Map<Category>(request);
                _categoryService.InsertCategory(category);

                CreateUpdateAttributeMappings(category.Id, request.AttributeMappings);

                return Unit.Task;
            }

            var updateCategory = _categoryService.GetCategoryById(request.Id.Value);
            updateCategory = AutoMapperConfiguration.Mapper.Map(request, updateCategory);
            _categoryService.UpdateCategory(updateCategory);

            CreateUpdateAttributeMappings(request.Id.Value, request.AttributeMappings);

            return Unit.Task;
        }

        private async void CreateUpdateAttributeMappings(Guid categoryId, IEnumerable<CreateUpdateCategoryAttributeMappingCommand> attributeMappings)
        {
            if (attributeMappings is null)
                return;

            foreach (var attributeMappingCommand in attributeMappings)
            {
                attributeMappingCommand.CategoryId = categoryId;
                await _mediator.Send(attributeMappingCommand);
            }
        }
    }
}
