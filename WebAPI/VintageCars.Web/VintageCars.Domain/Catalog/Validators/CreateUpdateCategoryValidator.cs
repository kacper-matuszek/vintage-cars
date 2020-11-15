using System.Linq;
using FluentValidation;
using VintageCars.Domain.Base.Validators;
using VintageCars.Domain.Catalog.Commands;

namespace VintageCars.Domain.Catalog.Validators
{
    public class CreateUpdateCategoryValidator : BaseValidator<CreateUpdateCategoryCommand>
    {
        public CreateUpdateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("CategoryAttribute.CreateCategoryAttribute.NameEmpty.Validation"));
            RuleFor(c => c.AttributeMappings)
                .Must(am => am == null || am.GroupBy(m => m.CategoryAttributeId).Any(m => m.Count() == 1))
                .WithMessage(GetMessageFromKey("CategoryAttributeMapping.Attribute.Validation"));
        }
    }
}
