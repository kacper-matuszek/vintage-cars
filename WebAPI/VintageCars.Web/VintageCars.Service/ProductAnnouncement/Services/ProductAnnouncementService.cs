using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Data;
using Nop.Service.Caching.Extensions;
using VintageCars.Data.Models;
using VintageCars.Domain.Shared.Response;
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
        private readonly IRepository<CategoryAttribute> _categoryAttributeRepository;
        private readonly IRepository<CategoryAttributeValue> _categoryAttributeValueRepository;

        #endregion

        #region Ctors

        public ProductAnnouncementService(IRepository<Data.Models.ProductAnnouncement> productAnnouncementRepository,
            IRepository<ProductAnnouncementAttribute> productAnnouncementAttributeRepository,
            IRepository<ProductAnnouncementAttributeMapping> productAnnouncementAttributeMappingRepository,
            IRepository<ProductAnnouncementPictureMapping> productAnnouncementPictureMappingRepository,
            IRepository<Picture> pictureRepository,
            IRepository<PictureBinary> pictureBinaryRepository,
            IRepository<CategoryAttribute> categoryAttributeRepository,
            IRepository<CategoryAttributeValue> categoryAttributeValueRepository)
        {
            _productAnnouncementRepository = productAnnouncementRepository;
            _productAnnouncementAttributeRepository = productAnnouncementAttributeRepository;
            _productAnnouncementAttributeMappingRepository = productAnnouncementAttributeMappingRepository;
            _productAnnouncementPictureMappingRepository = productAnnouncementPictureMappingRepository;
            _pictureRepository = pictureRepository;
            _prictureBinaryRepository = pictureBinaryRepository;
            _categoryAttributeRepository = categoryAttributeRepository;
            _categoryAttributeValueRepository = categoryAttributeValueRepository;
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

        public virtual Data.Models.ProductAnnouncement GetProductAnnouncement(Guid productAnnouncementId)
        {
            if(productAnnouncementId == default(Guid))
                throw new ArgumentNullException(nameof(productAnnouncementId));

            return _productAnnouncementRepository.ToCachedGetById(productAnnouncementId);
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

        public virtual List<AttributeView> GetAttributes(Guid productAnnouncementId)
        {
            if(productAnnouncementId == default(Guid))
                throw new ArgumentNullException(nameof(productAnnouncementId));

            return (from paam in _productAnnouncementAttributeMappingRepository.Table
                            join paa in _productAnnouncementAttributeRepository.Table on paam.ProductAnnouncementAttributeId equals paa.Id
                            join ca in _categoryAttributeRepository.Table on paa.CategoryAttributeId equals ca.Id
                            join cav in _categoryAttributeValueRepository.Table on paa.CategoryAttributeValueId equals cav.Id into paCv
                            from res in paCv.DefaultIfEmpty()
                            select new AttributeView()
                            {
                                Id = paa.Id,
                                Name = ca.Name,
                                Value = res.Name ?? paa.Value
                            }).ToList();

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

        #region Product Announcement Picture

        public virtual IDictionary<Guid, PictureModel> GetMainPicturesForProductAnnouncements(IEnumerable<Guid> productAnnouncementIds)
        {
            var picturesWithIds = (from papm in _productAnnouncementPictureMappingRepository.Table
                                  join pic in _pictureRepository.Table on papm.PictureId equals pic.Id
                                  join picBin in _prictureBinaryRepository.Table on pic.Id equals picBin.PictureId
                                  where papm.IsMain && productAnnouncementIds.Contains(papm.ProductAnnouncementId)
                                  select new
                                  {
                                      papm.ProductAnnouncementId,
                                      PictureModel = new PictureModel()
                                      {
                                          Id = pic.Id,
                                          AltAttribute = pic.AltAttribute,
                                          TitleAttribute = pic.TitleAttribute,
                                          MimeType = pic.MimeType,
                                          DataAsByteArray = picBin.BinaryData,
                                          IsMain = papm.IsMain
                                      }
                                  }).ToDictionary(p => p.ProductAnnouncementId, p => p.PictureModel);

            foreach (var (_, pictureModel) in picturesWithIds)
            {
                pictureModel.DataAsBase64 = pictureModel.GetDataAsBase64();
            }
            return picturesWithIds;
        }

        public virtual List<PictureModel> GetPictures(Guid productAnnouncementId)
        {
            if(productAnnouncementId == default(Guid))
                throw new ArgumentNullException(nameof(productAnnouncementId));

            var pictures = (from papm in _productAnnouncementPictureMappingRepository.Table
                join pic in _pictureRepository.Table on papm.PictureId equals pic.Id
                join picBin in _prictureBinaryRepository.Table on pic.Id equals picBin.PictureId
                where productAnnouncementId == papm.ProductAnnouncementId
                select new PictureModel()
                {
                    Id = pic.Id,
                    AltAttribute = pic.AltAttribute,
                    TitleAttribute = pic.TitleAttribute,
                    MimeType = pic.MimeType,
                    DataAsByteArray = picBin.BinaryData,
                    IsMain = papm.IsMain
                }).ToList();

            foreach (var picture in pictures)
            {
                picture.DataAsBase64 = picture.GetDataAsBase64();
            }

            return pictures;
        }

        #endregion

        #endregion
    }
}
