using System.Collections.Generic;
using VintageCars.Domain.Shared.Response;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.ProductAnnouncement.Response
{
    public class ProductAnnouncementDetailsView
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<AttributeView> Attributes { get; set; }
        public List<PictureModel> Pictures { get; set; }
    }
}
