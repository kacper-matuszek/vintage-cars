using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Data;
using VintageCars.Data.Models;
using VintageCars.Domain.Utils;

namespace VintageCars.Service.ProductAnnouncement.Services
{
    public class ProductAnnouncementService : IProductAnnouncementService
    {
        #region Fields

        private readonly IRepository<Data.Models.ProductAnnouncement> _productAnnouncementRepository;
        private readonly IRepository<ProductAnnouncementAttribute> _productAnnouncementAttributeRepository;
        private readonly IRepository<ProductAnnouncementAttributeMapping> _productAnnouncementAttributeMappingRepository;
        private readonly IRepository<ProductAnnouncementPictureMapping> _productAnnouncementPictureMappingRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<PictureBinary> _prictureBinaryRepository;

        #endregion

        #region Ctors

        public ProductAnnouncementService(IRepository<Data.Models.ProductAnnouncement> productAnnouncementRepository,
            IRepository<ProductAnnouncementAttribute> productAnnouncementAttributeRepository,
            IRepository<ProductAnnouncementAttributeMapping> productAnnouncementAttributeMappingRepository,
            IRepository<ProductAnnouncementPictureMapping> productAnnouncementPictureMappingRepository,
            IRepository<Picture> pictureRepository,
            IRepository<PictureBinary> pictureBinaryRepository)
        {
            _productAnnouncementRepository = productAnnouncementRepository;
            _productAnnouncementAttributeRepository = productAnnouncementAttributeRepository;
            _productAnnouncementAttributeMappingRepository = productAnnouncementAttributeMappingRepository;
            _productAnnouncementPictureMappingRepository = productAnnouncementPictureMappingRepository;
            _pictureRepository = pictureRepository;
            _prictureBinaryRepository = pictureBinaryRepository;
        }

        #endregion

        #region Methods

        #region ProductAnnouncement

        public virtual void InsertProductAnnouncement(Data.Models.ProductAnnouncement productAnnouncement)
        {
            if(productAnnouncement == null)
                throw new ArgumentNullException(nameof(productAnnouncement));

            _productAnnouncementRepository.Insert(productAnnouncement);
        }

        public virtual void UpdateProductAnnouncement(Data.Models.ProductAnnouncement productAnnouncement)
        {
            if(productAnnouncement == null)
                throw new ArgumentNullException(nameof(productAnnouncement));

            _productAnnouncementRepository.Update(productAnnouncement);
        }

        public virtual void DeleteProductAnnouncement(Guid productAnnouncementId)
        {
            if(productAnnouncementId == default(Guid))
                throw new ArgumentNullException(nameof(productAnnouncementId));

            _productAnnouncementRepository.Delete(x => x.Id == productAnnouncementId);
        }

        public virtual IPagedList<Data.Models.ProductAnnouncement> GetPagedProductAnnouncements(int pageIndex = 0,
            int pageSize = Int32.MaxValue)
        {
            return new PagedList<Data.Models.ProductAnnouncement>(_productAnnouncementRepository.Table.OrderBy(pa => pa.Name), pageIndex, pageSize);
        }

        #endregion

        #region ProductAnnouncement Attribute

        public virtual void InsertProductAnnouncementAttribute(ProductAnnouncementAttribute productAnnouncementAttribute)
        {
            if(productAnnouncementAttribute == null)
                throw new ArgumentNullException(nameof(productAnnouncementAttribute));

            _productAnnouncementAttributeRepository.Insert(productAnnouncementAttribute);
        }

        public virtual void UpdateProductAnnouncementAttribute(
            ProductAnnouncementAttribute productAnnouncementAttribute)
        {
            if(productAnnouncementAttribute == null)
                throw new ArgumentNullException(nameof(productAnnouncementAttribute));

            _productAnnouncementAttributeRepository.Update(productAnnouncementAttribute);
        }

        public virtual IList<ProductAnnouncementAttribute> GetProductAnnouncementAttributes(Guid productAnnouncementId)
        {
            if(productAnnouncementId == default(Guid))
                return new List<ProductAnnouncementAttribute>();

            var query = from paam in _productAnnouncementAttributeMappingRepository.Table
                        join paa in _productAnnouncementAttributeRepository.Table on paam.ProductAnnouncementAttributeId equals paa.Id
                        where paam.ProductAnnouncementId == productAnnouncementId
                        select paa;

            return query.ToList();
        }

        #endregion

        #region Product Announcement Attribute Mappings

        public virtual void InsertProductAnnouncementAttributeMappings(ProductAnnouncementAttributeMapping productAnnouncementAttributeMapping)
        {
            if(productAnnouncementAttributeMapping == null)
                throw new ArgumentNullException(nameof(productAnnouncementAttributeMapping));

            _productAnnouncementAttributeMappingRepository.Insert(productAnnouncementAttributeMapping);
        }

        public virtual IList<ProductAnnouncementAttributeMapping> GetProductAnnouncementAttributeMappings(Guid productAnnouncementId)
        {
            return productAnnouncementId == default(Guid)
                ? new List<ProductAnnouncementAttributeMapping>()
                : _productAnnouncementAttributeMappingRepository.Table
                    .Where(paam => paam.ProductAnnouncementId == productAnnouncementId).ToList();
        }

        public virtual void DeleteProductAnnouncemenetAttributeMapping(Guid productAnnouncementAttributeMapping)
        {
            if(productAnnouncementAttributeMapping == null)
                throw new ArgumentNullException(nameof(productAnnouncementAttributeMapping));

            _productAnnouncementAttributeMappingRepository.Delete(paam => paam.Id == productAnnouncementAttributeMapping);
        }

        public virtual void DeleteProductAnnouncementAttributeMappings(Guid productAnnouncementId)
        {
            if(productAnnouncementId == default(Guid))
                throw new ArgumentNullException(nameof(productAnnouncementId));

            var productAnnouncemenetMappings = GetProductAnnouncementAttributeMappings(productAnnouncementId);

            _productAnnouncementAttributeMappingRepository.Delete(productAnnouncemenetMappings);
        }
        #endregion

        #region Product Announcement Picture Mappings

        public virtual void InsertProductAnnouncementPictureMapping(PictureModel picture, Guid productAnnouncementId)
        {
            if(picture == null)
                throw new ArgumentNullException(nameof(picture));

            if(picture.Id == default(Guid))
                picture.Id = Guid.NewGuid();

            _pictureRepository.Insert(picture);
            _productAnnouncementPictureMappingRepository.Insert(new ProductAnnouncementPictureMapping()
            {
                ProductAnnouncementId =  productAnnouncementId,
                PictureId = picture.Id,
                IsMain = picture.IsMain,
            });

            var binary = new PictureBinary()
            {
                Id = Guid.NewGuid(),
                BinaryData = picture.DataAsByteArray?.Length == null || picture.DataAsByteArray?.Length  == 0 ? picture.GetDataAsByteArray() : picture.DataAsByteArray,
                PictureId = picture.Id
            };
            _prictureBinaryRepository.Insert(binary);
        }

        public virtual IList<ProductAnnouncementPictureMapping> GetProductAnnouncementPictureMappings(Expression<Func<ProductAnnouncementPictureMapping, bool>> predicate)
        {
            return _productAnnouncementPictureMappingRepository.Table.Where(predicate).ToList();
        }

        public virtual void DeleteProductAnnouncementPictureMapping(Guid productAnnouncementPictureMappingId)
        {
            if(productAnnouncementPictureMappingId == default(Guid))
                throw new ArgumentNullException(nameof(productAnnouncementPictureMappingId));

            _productAnnouncementPictureMappingRepository.Delete(papm => papm.Id == productAnnouncementPictureMappingId);
        }

        public virtual void DeleteProductAnnoucementPictureMappings(Guid productAnnouncementId)
        {
            if(productAnnouncementId == default(Guid))
                throw new ArgumentNullException(nameof(productAnnouncementId));

            var productAnnouncementPictureMappings = GetProductAnnouncementPictureMappings(papm => papm.ProductAnnouncementId == productAnnouncementId);
            _productAnnouncementPictureMappingRepository.Delete(productAnnouncementPictureMappings);
        }

        #endregion

        #endregion
    }
}
