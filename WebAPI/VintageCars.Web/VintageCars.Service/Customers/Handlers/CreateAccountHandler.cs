using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using Nop.Core.Requests.Customers;
using Nop.Service.Customer;
using VintageCars.Domain.Customer.Commands;
using VintageCars.Service.Infrastructure;
using ValidationException = VintageCars.Domain.Exceptions.ValidationException;

namespace VintageCars.Service.Customers.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Unit>
    {
        private readonly ICustomerRegistrationService _customerRegistration;
        private readonly IInfrastructureService _infrastructureService;

        public CreateAccountHandler(ICustomerRegistrationService customerRegistration, IInfrastructureService infrastructureService)
        {
            _customerRegistration = customerRegistration;
            _infrastructureService = infrastructureService;
        }

        public Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var requestRegistration = AutoMapperConfiguration.Mapper.Map<CustomerRegistrationRequest>(request);
            requestRegistration.Customer.RegisteredInStoreId = _infrastructureService.Cache.Store.Id;
            var errors = _customerRegistration.RegisterCustomer(requestRegistration);
            if (errors.Any())
                throw new ValidationException(errors.Count() > 1 ? string.Join(Environment.NewLine, errors) : errors.FirstOrDefault());
            return Unit.Task;
        }
    }
}
