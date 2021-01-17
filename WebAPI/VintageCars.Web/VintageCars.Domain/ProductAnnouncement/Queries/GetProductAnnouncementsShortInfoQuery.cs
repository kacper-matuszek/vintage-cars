using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.ProductAnnouncement.Response;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.ProductAnnouncement.Queries
{
    public class GetProductAnnouncementsShortInfoQuery : QueryPagedBase<PagedList<ProductAnnouncementShortInfoView>>
    {
        public GetProductAnnouncementsShortInfoQuery(PagedRequest paged)
            : base(paged)
        {
        }
    }
}
