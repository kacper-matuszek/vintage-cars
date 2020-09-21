using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Customer.Commands
{
    public class RecoverPasswordCommand : CommandBase
    {
        public string Email { get; set; }
    }
}
