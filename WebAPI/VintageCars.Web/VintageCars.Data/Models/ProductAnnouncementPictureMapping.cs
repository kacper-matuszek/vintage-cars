using System;
using Nop.Core;

namespace VintageCars.Data.Models
{
    public class ProductAnnouncementPictureMapping : BaseEntity
    {
        public Guid ProductAnnouncementId { get; set; }
        public Guid PictureId { get; set; }
        public bool IsMain { get; set; }
    }
}
