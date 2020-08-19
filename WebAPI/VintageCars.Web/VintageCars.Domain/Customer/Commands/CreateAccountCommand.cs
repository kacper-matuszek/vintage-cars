using MediatR;
using VintageCars.Domain.Settings.Base;

namespace VintageCars.Domain.Customer.Commands
{
    public class CreateAccountCommand : GoogleRecaptchaBase, IRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
