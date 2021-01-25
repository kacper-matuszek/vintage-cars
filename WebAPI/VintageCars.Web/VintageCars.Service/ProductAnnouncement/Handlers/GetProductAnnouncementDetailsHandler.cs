using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.ProductAnnouncement.Queries;
using VintageCars.Domain.ProductAnnouncement.Response;
using VintageCars.Service.Catalog.Services;
using VintageCars.Service.ProductAnnouncement.Services;

namespace VintageCars.Service.ProductAnnouncement.Handlers
{
    public class GetProductAnnouncementDetailsHandler : IRequestHandler<GetProductAnnouncementDetailsQuery, ProductAnnouncementDetailsView>
    {
        private readonly IProductAnnouncementService _productAnnouncementService;
        private readonly IExtendedCategoryService _extendedCategoryService;

        public GetProductAnnouncementDetailsHandler(IProductAnnouncementService productAnnouncementService, IExtendedCategoryService extendedCategoryService)
        {
            _productAnnouncementService = productAnnouncementService;
            _extendedCategoryService = extendedCategoryService;
        }

        public Task<ProductAnnouncementDetailsView> Handle(GetProductAnnouncementDetailsQuery request, CancellationToken cancellationToken)
            => Task.Run(() =>
            {
                var productAnnouncementDetails = AutoMapperConfiguration.Mapper.Map<ProductAnnouncementDetailsView>(_productAnnouncementService.GetProductAnnouncement(request.ProductAnnouncementId));
                productAnnouncementDetails.Pictures = _productAnnouncementService.GetPictures(request.ProductAnnouncementId);
                productAnnouncementDetails.Attributes = _productAnnouncementService.GetAttributes(request.ProductAnnouncementId);
                return productAnnouncementDetails;
            }, cancellationToken);
    }
}
