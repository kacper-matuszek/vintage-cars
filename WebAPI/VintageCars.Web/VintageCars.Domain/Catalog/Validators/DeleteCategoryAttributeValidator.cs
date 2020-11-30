using FluentValidation;
using VintageCars.Domain.Base.Validators;
using VintageCars.Domain.Catalog.Commands;

namespace VintageCars.Domain.Catalog.Validators
{
    public class DeleteCategoryAttributeValidator : BaseValidator<DeleteCategoryAttributeCommand>
    {
        public DeleteCategoryAttributeValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("DeleteCategoryAttribute.IdNotEmpty.Validation"));
        }
    }
}
