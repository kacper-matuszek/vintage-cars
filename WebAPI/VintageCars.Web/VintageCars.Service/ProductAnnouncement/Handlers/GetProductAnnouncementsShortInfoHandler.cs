using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VintageCars.Domain.Common;
using VintageCars.Domain.Extensions;
using VintageCars.Domain.ProductAnnouncement.Queries;
using VintageCars.Domain.ProductAnnouncement.Response;
using VintageCars.Service.ProductAnnouncement.Services;

namespace VintageCars.Service.ProductAnnouncement.Handlers
{
    public class GetProductAnnouncementsShortInfoHandler : IRequestHandler<GetProductAnnouncementsShortInfoQuery, PagedList<ProductAnnouncementShortInfoView>>
    {
        private readonly IProductAnnouncementService _productAnnouncementService;

        public GetProductAnnouncementsShortInfoHandler(IProductAnnouncementService productAnnouncementService)
        {
            _productAnnouncementService = productAnnouncementService;
        }

        public Task<PagedList<ProductAnnouncementShortInfoView>> Handle(GetProductAnnouncementsShortInfoQuery request, CancellationToken cancellationToken)
            => Task.Run(() =>
            {
                var productAnnouncements = _productAnnouncementService
                    .GetPagedProductAnnouncements(request.Paged.PageIndex, request.Paged.PageSize)
                    .ConvertPagedList<Data.Models.ProductAnnouncement, ProductAnnouncementShortInfoView>();

                var pictureModels = _productAnnouncementService.GetMainPicturesForProductAnnouncements(productAnnouncements.Source.Select(x => x.Id));
                foreach (var (productAnnouncementId, picture) in pictureModels)
                {
                    productAnnouncements.Source.First(x => x.Id == productAnnouncementId).MainPicture = picture;
                }
                return productAnnouncements;
            }, cancellationToken);
    }
}
