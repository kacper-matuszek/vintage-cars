using System;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.ProductAnnouncement.Response;

namespace VintageCars.Domain.ProductAnnouncement.Queries
{
    public class GetProductAnnouncementDetailsQuery : QueryBase<ProductAnnouncementDetailsView>
    {
        public Guid ProductAnnouncementId { get; set; }
    }
}
