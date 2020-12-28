using VintageCars.Data.Models.Base;

namespace VintageCars.Data.Models
{
    public class ProductAnnouncement : BaseCreationEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
