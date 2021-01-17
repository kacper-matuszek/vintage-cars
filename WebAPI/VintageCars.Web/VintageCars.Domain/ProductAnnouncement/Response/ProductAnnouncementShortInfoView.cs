using VintageCars.Domain.Base;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.ProductAnnouncement.Response
{
    public class ProductAnnouncementShortInfoView : BaseModelView
    {
        public string ShortDescription { get; set; }
        public PictureModel MainPicture { get; set; }
    }
}
