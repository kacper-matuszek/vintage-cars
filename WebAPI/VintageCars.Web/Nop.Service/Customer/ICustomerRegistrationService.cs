using System.Collections.Generic;
using Nop.Core.Requests.Customers;

namespace Nop.Service.Customer
{
    public interface ICustomerRegistrationService
    {
        IEnumerable<string> RegisterCustomer(CustomerRegistrationRequest request);
    }
}
