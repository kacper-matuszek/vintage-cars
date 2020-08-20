using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using Nop.Core.Requests.Customers;
using Nop.Service.Customer;
using VintageCars.Domain.Customer.Commands;
using ValidationException = VintageCars.Domain.Exceptions.ValidationException;

namespace VintageCars.Service.Customers.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Unit>
    {
        private readonly ICustomerRegistrationService _customerRegistration;

        public CreateAccountHandler(ICustomerRegistrationService customerRegistration)
        {
            _customerRegistration = customerRegistration;
        }

        public Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var requestRegistration = AutoMapperConfiguration.Mapper.Map<CustomerRegistrationRequest>(request);
            var errors = _customerRegistration.RegisterCustomer(requestRegistration);
            if (errors.Any())
                throw new ValidationException(errors.Count() > 1 ? string.Join(Environment.NewLine, errors) : errors.FirstOrDefault());
            return Unit.Task;
        }
    }
}
