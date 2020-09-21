using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Service.Customer;
using Nop.Service.Messages;
using VintageCars.Domain.Customer.Commands;
using VintageCars.Domain.Exceptions;
using VintageCars.Service.Infrastructure;

namespace VintageCars.Service.Customers.Handlers
{
    public class RecoveryPasswordHandler : IRequestHandler<RecoverPasswordCommand, Unit>
    {
        private readonly IWorkflowMessageService _workflowService;
        private readonly ICustomerService _customerService;
        private readonly IInfrastructureService _infrastructureService;

        public RecoveryPasswordHandler(IWorkflowMessageService workflowService,
            ICustomerService customerService,
            IInfrastructureService infrastructureService)
        {
            _workflowService = workflowService;
            _customerService = customerService;
            _infrastructureService = infrastructureService;
        }

        public Task<Unit> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            var languageId = _infrastructureService.Cache.Language.Id;
            var customer = _customerService.GetCustomerByEmail(request.Email);
            var response = _workflowService.SendCustomerPasswordRecoveryMessage(customer, languageId);
            if(!response.Any())
                throw new ResourcesNotFoundException("Nie udało się wysłać e-mail do resetowania hasła.");
            return Unit.Task;
        }
    }
}
