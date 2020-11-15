using System.Linq;
using FluentValidation;
using VintageCars.Domain.Base.Validators;
using VintageCars.Domain.Catalog.Commands;

namespace VintageCars.Domain.Catalog.Validators
{
    public class LinkCategoryAttributeValueValidator : BaseValidator<LinkCategoryAttributeValueCommand>
    {
        public LinkCategoryAttributeValueValidator()
        {
            RuleFor(lcav => lcav.CategoryId)
                .NotEmpty()
                .WithMessage(
                    GetMessageFromKey(
                        "LinkCategoryAttributeValue.CategoryIdEmpty.Validation"));

            RuleFor(lcav => lcav.CategoryAttributeValues)
                .Must(attrVals => attrVals != null && attrVals.GroupBy(av => av.CategoryAttributeId).Select(av => new
                {
                    Count = av.Count(x => x.IsPreSelected),
                }).Any(av => av.Count == 1))
                .WithMessage(GetMessageFromKey("LinkCategoryAttributeValue.CategoryAttributeValue.PreselectedDuplication.Validation"));

            RuleForEach(lc => lc.CategoryAttributeValues).ChildRules(categoryAttributeValue =>
            {
                categoryAttributeValue.RuleFor(cav => cav.Name)
                    .NotEmpty()
                    .WithMessage(
                        GetMessageFromKey("LinkCategoryAttributeValue.CategoryAttributeValue.NameEmpty.Validation"));

                categoryAttributeValue.RuleFor(cav => cav.CategoryAttributeId)
                    .NotEmpty()
                    .WithMessage(
                        GetMessageFromKey(
                            "LinkCategoryAttributeValue.CategoryAttributeValue.CategoryAttributeIdEmpty.Validation"));
            });
        }
    }
}
