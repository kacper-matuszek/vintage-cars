using System.Collections.Generic;
using Nop.Core.Domain.Customers;
using Nop.Core.Requests.Customers;

namespace Nop.Service.Customer
{
    public interface ICustomerRegistrationService
    {
        IEnumerable<string> RegisterCustomer(CustomerRegistrationRequest request);

        CustomerLoginResults ValidateCustomer(string usernameOrEmail, string password);
    }
}
