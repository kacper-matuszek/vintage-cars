using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nop.Core;
using VintageCars.Data.Models;
using VintageCars.Domain.Utils;

namespace VintageCars.Service.ProductAnnouncement.Services
{
    public interface IProductAnnouncementService
    {
        void InsertProductAnnouncement(Data.Models.ProductAnnouncement productAnnouncement);
        void UpdateProductAnnouncement(Data.Models.ProductAnnouncement productAnnouncement);
        void DeleteProductAnnouncement(Guid productAnnouncementId);

        IPagedList<Data.Models.ProductAnnouncement> GetPagedProductAnnouncements(int pageIndex = 0,
            int pageSize = Int32.MaxValue);

        void InsertProductAnnouncementAttribute(ProductAnnouncementAttribute productAnnouncementAttribute);

        void UpdateProductAnnouncementAttribute(
            ProductAnnouncementAttribute productAnnouncementAttribute);

        IList<ProductAnnouncementAttribute> GetProductAnnouncementAttributes(Guid productAnnouncementId);
        void InsertProductAnnouncementAttributeMappings(ProductAnnouncementAttributeMapping productAnnouncementAttributeMapping);
        IList<ProductAnnouncementAttributeMapping> GetProductAnnouncementAttributeMappings(Guid productAnnouncementId);
        void DeleteProductAnnouncemenetAttributeMapping(Guid productAnnouncementAttributeMapping);
        void DeleteProductAnnouncementAttributeMappings(Guid productAnnouncementId);
        void InsertProductAnnouncementPictureMapping(PictureModel picture, Guid productAnnouncementId);
        IList<ProductAnnouncementPictureMapping> GetProductAnnouncementPictureMappings(Expression<Func<ProductAnnouncementPictureMapping, bool>> predicate);
        void DeleteProductAnnouncementPictureMapping(Guid productAnnouncementPictureMappingId);
        void DeleteProductAnnoucementPictureMappings(Guid productAnnouncementId);
        IDictionary<Guid, PictureModel> GetMainPicturesForProductAnnouncements(IEnumerable<Guid> productAnnouncementIds);
    }
}