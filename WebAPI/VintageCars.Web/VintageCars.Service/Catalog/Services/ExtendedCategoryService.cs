using System;
using System.Linq;
using System.Collections.Generic;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Caching.Extensions;
using Nop.Service.Catalog;
using Nop.Service.Customer;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog;

namespace VintageCars.Service.Catalog.Services
{
    public class ExtendedCategoryService : CategoryService, IExtendedCategoryService
    {
        #region Fields

        private readonly IRepository<CategoryAttribute> _categoryAttributeRepository;
        private readonly IRepository<CategoryAttributeMapping> _categoryAttributeMappingRepository;
        private readonly IRepository<CategoryAttributeValue> _categoryAttributeValueRepository;

        #endregion

        #region Ctors
        public ExtendedCategoryService(IRepository<Category> categoryRepository,
            IRepository<DiscountCategoryMapping> discountCategoryMappingRepository,
            IRepository<Product> productRepository,
            IRepository<ProductCategory> productCategoryRepository,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            ICustomerService customerService) : base(categoryRepository,
            discountCategoryMappingRepository,
            productRepository,
            productCategoryRepository,
            cacheKeyService,
            staticCacheManager,
            customerService)
        {
        }

        public ExtendedCategoryService(IRepository<Category> categoryRepository,
            IRepository<DiscountCategoryMapping> discountCategoryMappingRepository,
            IRepository<Product> productRepository,
            IRepository<ProductCategory> productCategoryRepository,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            ICustomerService customerService,
            IRepository<CategoryAttribute> categoryAttributeRepository,
            IRepository<CategoryAttributeMapping> categoryAttributeMappingRepository,
            IRepository<CategoryAttributeValue> categoryAttributeValueRepository) : base(categoryRepository,
            discountCategoryMappingRepository,
            productRepository,
            productCategoryRepository,
            cacheKeyService,
            staticCacheManager,
            customerService)
        {
            _categoryAttributeRepository = categoryAttributeRepository;
            _categoryAttributeMappingRepository = categoryAttributeMappingRepository;
            _categoryAttributeValueRepository = categoryAttributeValueRepository;
        }

        #endregion

        #region Methods

        #region CategoryAttributes

        public virtual void DeleteCategoryAttribute(CategoryAttribute categoryAttribute)
        {
            if(categoryAttribute == null)
                throw new ArgumentNullException(nameof(categoryAttribute));

            _categoryAttributeRepository.Delete(categoryAttribute);
        }

        public virtual void DeleteCategoryAttribute(Guid categoryAttributeId)
        {
            if(categoryAttributeId == default(Guid))
                throw new ArgumentNullException(nameof(categoryAttributeId));

            _categoryAttributeRepository.Delete(x => x.Id == categoryAttributeId);
        }

        public virtual IList<CategoryAttribute> GetAllCategoryAttributes()
        {
            var query = from ca in _categoryAttributeRepository.Table
                        orderby ca.Name
                        select ca;

            return query.ToList();
        }

        public virtual void InsertCategoryAttribute(CategoryAttribute categoryAttribute)
        {
            if(categoryAttribute == null)
                throw new ArgumentNullException(nameof(categoryAttribute));

            _categoryAttributeRepository.Insert(categoryAttribute);
        }

        public virtual void UpdateCategoryAttribute(CategoryAttribute categoryAttribute)
        {
            if(categoryAttribute == null)
                throw new ArgumentNullException(nameof(categoryAttribute));

            _categoryAttributeRepository.Update(categoryAttribute);
        }

        #endregion

        #region Category Attributes Mappings

        public virtual void DeleteCategoryMapping(CategoryAttributeMapping categoryAttributeMapping)
        {
            if(categoryAttributeMapping == null)
                throw new ArgumentNullException(nameof(categoryAttributeMapping));

            _categoryAttributeMappingRepository.Delete(categoryAttributeMapping);
        }

        public virtual void DeleteCategoryMapping(Guid categoryAttributeMappingId)
        {
            if(categoryAttributeMappingId == null)
                throw new ArgumentNullException(nameof(categoryAttributeMappingId));

            _categoryAttributeMappingRepository.Delete(x => x.Id == categoryAttributeMappingId);
        }

        public virtual IList<CategoryAttributeMapping> GetCategoryAttributeMappingsByCategoryId(Guid categoryId)
        {
            var allCacheKey = _cacheKeyService.PrepareKeyForDefaultCache(VintageCarsCatalogDefaults.CategoryAttributeMappingsAllCacheKey, categoryId);

            var query = from cam in _categoryAttributeMappingRepository.Table
                        orderby cam.DisplayOrder, cam.Id
                        where cam.CategoryId == categoryId
                        select cam;

            var attributes = query.ToCachedList(allCacheKey) ?? new List<CategoryAttributeMapping>();

            return attributes;
        }

        public virtual CategoryAttributeMapping GetCategoryAttributeMapping(Guid categoryAttributeMapping)
        {
            if(categoryAttributeMapping == default(Guid))
                throw new ArgumentNullException(nameof(categoryAttributeMapping));

            return _categoryAttributeMappingRepository.ToCachedGetById(categoryAttributeMapping);
        }

        public virtual void InsertCategoryAttributeMapping(CategoryAttributeMapping categoryAttributeMapping)
        {
            if(categoryAttributeMapping == default)
                throw new ArgumentNullException(nameof(categoryAttributeMapping));

            _categoryAttributeMappingRepository.Insert(categoryAttributeMapping);
        }

        public virtual void UpdateCategoryAttributeMapping(CategoryAttributeMapping categoryAttributeMapping)
        {
            if(categoryAttributeMapping == default)
                throw new ArgumentNullException(nameof(categoryAttributeMapping));

            _categoryAttributeMappingRepository.Update(categoryAttributeMapping);
        }

        #endregion

        #region Category Attributes Values

        public virtual void DeleteCategoryAttributeValue(CategoryAttributeValue categoryAttributeValue)
        {
            if(categoryAttributeValue == default)
                throw new ArgumentNullException(nameof(categoryAttributeValue));

            _categoryAttributeValueRepository.Delete(categoryAttributeValue);
        }

        public virtual void DeleteCategoryAttributeValue(Guid categoryAttributeValueId)
        {
            if(categoryAttributeValueId == default)
                throw new ArgumentNullException(nameof(categoryAttributeValueId));

            _categoryAttributeValueRepository.Delete(x => x.Id == categoryAttributeValueId);
        }

        public virtual IList<CategoryAttributeValue> GetCategoryAttributeValues(Guid categoryAttributeMappingId)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(VintageCarsCatalogDefaults.CategoryAttributeValuesCacheKey, categoryAttributeMappingId);

            var query = from cav in _categoryAttributeValueRepository.Table
                        orderby cav.DisplayOrder, cav.Id
                        where cav.CategoryAttributeMappingId == categoryAttributeMappingId
                        select cav;

            return query.ToCachedList(key);
        }

        public virtual CategoryAttributeValue GetCategoryAttributeValueById(Guid categoyAttributeValueId)
        {
            if(categoyAttributeValueId == default(Guid))
                throw new ArgumentNullException(nameof(categoyAttributeValueId));

            return _categoryAttributeValueRepository.ToCachedGetById(categoyAttributeValueId);
        }

        public virtual void InsertCategoryAttributeValue(CategoryAttributeValue categoryAttributeValue)
        {
            if(categoryAttributeValue == null)
                throw new ArgumentNullException(nameof(categoryAttributeValue));

            _categoryAttributeValueRepository.Insert(categoryAttributeValue);
        }

        public virtual void UpdateCategoryAttributeValue(CategoryAttributeValue categoryAttributeValue)
        {
            if(categoryAttributeValue == default)
                throw new ArgumentNullException(nameof(categoryAttributeValue));

            _categoryAttributeValueRepository.Update(categoryAttributeValue);
        }
        #endregion

        #endregion

    }
}
