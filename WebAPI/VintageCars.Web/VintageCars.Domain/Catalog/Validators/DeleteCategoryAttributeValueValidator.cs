using FluentValidation;
using VintageCars.Domain.Base.Validators;
using VintageCars.Domain.Catalog.Commands;

namespace VintageCars.Domain.Catalog.Validators
{
    public class DeleteCategoryAttributeValueValidator : BaseValidator<DeleteCategoryAttributeValueCommand>
    {
        public DeleteCategoryAttributeValueValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("DeleteCategoryAttributeValue.IdNotEmpty.Validation"));
        }
    }
}
