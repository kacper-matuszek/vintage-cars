using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Customer.Responses;

namespace VintageCars.Domain.Customer.Commands
{
    public class LoginCustomerCommand : CommandBase<LoginCustomerResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
