using FluentValidation;
using VintageCars.Domain.Base.Validators;
using VintageCars.Domain.Catalog.Commands;

namespace VintageCars.Domain.Catalog.Validators
{
    public class CreateCategoryAttributeValidator : BaseValidator<CreateUpdateCategoryAttributeCommand>
    {
        public CreateCategoryAttributeValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("CategoryAttribute.CreateCategoryAttribute.NameEmpty.Validation"));
        }
    }
}
