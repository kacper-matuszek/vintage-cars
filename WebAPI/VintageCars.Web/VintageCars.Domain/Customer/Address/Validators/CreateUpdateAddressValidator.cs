using FluentValidation;
using VintageCars.Domain.Base.Validators;
using VintageCars.Domain.Customer.Address.Commands;

namespace VintageCars.Domain.Customer.Address.Validators
{
    public class CreateUpdateAddressValidator : BaseValidator<CreateUpdateAddressCommand>
    {
        public CreateUpdateAddressValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("Address.CreateUpdateAddressCommand.FirstNameNotProvided.Validation"));
            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("Address.CreateUpdateAddressCommand.LastNameNotProvided.Validation"));
        }
    }
}
