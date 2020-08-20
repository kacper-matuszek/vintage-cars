using FluentValidation;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Service.Localization;
using VintageCars.Domain.Base.Validators;
using VintageCars.Domain.Customer.Commands;
using VintageCars.Domain.Extensions;

namespace VintageCars.Domain.Customer.Validators
{
    public class CreateAccountValidator : BaseValidator<CreateAccountCommand>
    {
        private readonly CustomerSettings _customerSettings;

        public CreateAccountValidator()
        {
            _customerSettings = EngineContext.Current.Resolve<CustomerSettings>();
            SetRules();
        }

        public void SetRules()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("Customer.RegisterCustomer.EmailNotProvided.Validation"))
                .EmailAddress()
                .WithMessage(GetMessageFromKey("Customer.RegisterCustomer.IsNotEmail.Validation"));

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage(GetMessageFromKey("Customer.RegisterCustomer.PasswordNotProvided.Validation"))
                .MinimumLength(_customerSettings.PasswordMinLength)
                .WithMessage(GetMessageFromKey("Customer.RegisterCustomer.PasswordMinLength.Validation",
                    _customerSettings.PasswordMinLength))
                .Matches("[a-z]")
                .SetCurrentValidation(_customerSettings.PasswordRequireLowercase,
                    GetMessageFromKey("Customer.RegisterCustomer.PasswordLowercase.Validation"))
                .Matches("[A-Z]")
                .SetCurrentValidation(_customerSettings.PasswordRequireUppercase,
                    GetMessageFromKey("Customer.RegisterCustomer.PasswordUppercase.Validation"))
                .Matches("[0-9]")
                .SetCurrentValidation(_customerSettings.PasswordRequireDigit,
                    GetMessageFromKey("Customer.RegisterCustomer.PasswordDigit.Validation"))
                .Matches("[^a-zA-Z0-9]")
                .SetCurrentValidation(_customerSettings.PasswordRequireNonAlphanumeric,
                    GetMessageFromKey("Customer.RegisterCustomer.PasswordAlphanumeric.Validation"));

            RuleFor(r => r.Username)
                .NotEmpty()
                .SetCurrentValidation(_customerSettings.UsernameValidationEnabled,
                    GetMessageFromKey("Customer.RegisterCustomer.UsernameEmpty.Validation"));
        }
    }
}
