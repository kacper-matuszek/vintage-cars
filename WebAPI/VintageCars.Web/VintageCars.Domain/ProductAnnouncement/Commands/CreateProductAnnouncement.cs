using System;
using System.Collections.Generic;
using VintageCars.Domain.Base;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.ProductAnnouncement.Models;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.ProductAnnouncement.Commands
{
    public class CreateProductAnnouncement : CommandBase, IBusinessEntity
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<ProductAnnouncementAttribute> Attributes { get; set; }
        public List<PictureModel> Pictures { get; set; }
    }
}
