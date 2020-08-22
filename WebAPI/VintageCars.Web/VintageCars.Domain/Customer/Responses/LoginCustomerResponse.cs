using Nop.Core.Domain.Customers;

namespace VintageCars.Domain.Customer.Responses
{
    public class LoginCustomerResponse
    {
        public CustomerLoginResults LoginResult { get; set; }
        public string Token { get; set; }
    }
}
