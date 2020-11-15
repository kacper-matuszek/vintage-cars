using System.Collections.Generic;
using Nop.Core.Domain.Customers;

namespace VintageCars.Domain.Customer.Responses
{
    public class LoginCustomerResponse
    {
        public LoginCustomerResponse()
        {
            Roles = new List<string>();
        }
        public CustomerLoginResults LoginResult { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; }
    }
}
