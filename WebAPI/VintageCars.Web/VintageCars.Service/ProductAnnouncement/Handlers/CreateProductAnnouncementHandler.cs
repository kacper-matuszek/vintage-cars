using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VintageCars.Domain.ProductAnnouncement.Commands;

namespace VintageCars.Service.ProductAnnouncement.Handlers
{
    public class CreateProductAnnouncementHandler : IRequestHandler<CreateProductAnnouncement, Unit>
    {
        public Task<Unit> Handle(CreateProductAnnouncement request, CancellationToken cancellationToken)
        {

            return Unit.Task;
        }
    }
}
