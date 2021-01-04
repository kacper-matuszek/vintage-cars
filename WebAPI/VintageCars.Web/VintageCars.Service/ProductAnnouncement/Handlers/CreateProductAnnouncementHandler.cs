using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.ProductAnnouncement.Commands;
using VintageCars.Service.ProductAnnouncement.Services;

namespace VintageCars.Service.ProductAnnouncement.Handlers
{
    public class CreateProductAnnouncementHandler : IRequestHandler<CreateProductAnnouncement, Unit>
    {
        private readonly IProductAnnouncementService _productAnnouncementService;

        public CreateProductAnnouncementHandler(IProductAnnouncementService productAnnouncementService)
        {
            _productAnnouncementService = productAnnouncementService;
        }

        public Task<Unit> Handle(CreateProductAnnouncement request, CancellationToken cancellationToken)
        {
            var productAnnouncement = AutoMapperConfiguration.Mapper.Map<Data.Models.ProductAnnouncement>(request);
            _productAnnouncementService.InsertProductAnnouncement(productAnnouncement);

            var productAnnouncementAttributes = AutoMapperConfiguration.Mapper.Map<List<ProductAnnouncementAttribute>>(request.Attributes);
            productAnnouncementAttributes?.ForEach(paa =>
            {
                _productAnnouncementService.InsertProductAnnouncementAttribute(paa);
                _productAnnouncementService.InsertProductAnnouncementAttributeMappings(new ProductAnnouncementAttributeMapping()
                {
                    ProductAnnouncementAttributeId = paa.Id,
                    ProductAnnouncementId = productAnnouncement.Id
                });
            });

            request.Pictures?.ForEach(picture =>
            {
                _productAnnouncementService.InsertProductAnnouncementPictureMapping(picture, productAnnouncement.Id);
            });

            return Unit.Task;
        }
    }
}
