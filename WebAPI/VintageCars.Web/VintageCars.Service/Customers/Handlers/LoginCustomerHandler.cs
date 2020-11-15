using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Domain.Customers;
using Nop.Service.Customer;
using VintageCars.Domain.Customer.Commands;
using VintageCars.Domain.Customer.Responses;
using VintageCars.Service.Infrastructure;

namespace VintageCars.Service.Customers.Handlers
{
    public class LoginCustomerHandler : IRequestHandler<LoginCustomerCommand, LoginCustomerResponse>
    {
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerService _customerService;
        private readonly IJwtService _jwtService;

        public LoginCustomerHandler(ICustomerRegistrationService customerRegistrationService, ICustomerService customerService, IJwtService jwtService)
        {
            _customerRegistrationService = customerRegistrationService;
            _customerService = customerService;
            _jwtService = jwtService;
        }

        public Task<LoginCustomerResponse> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
        {
            var isUserName = !string.IsNullOrEmpty(request.Username);
            var passwordResult = _customerRegistrationService.ValidateCustomer(
                        !isUserName ? request.Email : request.Username, request.Password);

            var response = new LoginCustomerResponse()
            {
                LoginResult = passwordResult,
            };
            if (passwordResult == CustomerLoginResults.Successful)
            {
                var customer = isUserName ? _customerService.GetCustomerByUsername(request.Username) : _customerService.GetCustomerByEmail(request.Email);
                var customerRoles = _customerService.GetCustomerRoles(customer).Where(cr => cr.Active);
                response.Token = _jwtService.GenerateToken(customer, customerRoles);
                response.Roles.AddRange(customerRoles.Select(x => x.SystemName));
            }

            return Task.FromResult(response);
        }
    }
}
