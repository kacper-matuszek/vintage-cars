using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Service.Catalog;
using VintageCars.Data.Models;

namespace VintageCars.Service.Catalog.Services
{
    public interface IExtendedCategoryService : ICategoryService
    {
        void DeleteCategoryAttribute(CategoryAttribute categoryAttribute);
        void DeleteCategoryAttribute(Guid categoryAttributeId);
        IList<CategoryAttribute> GetAllCategoryAttributes();
        IList<CategoryAttribute> GetAllCategoryAttributesByCategoryId(Guid categoryId);

        IPagedList<CategoryAttribute> GetPagedCategoryAttributes(int pageIndex = 0,
            int pageSize = Int32.MaxValue);
        CategoryAttribute GetCategoryAttribute(Guid categoryAttributeId);
        void InsertCategoryAttribute(CategoryAttribute categoryAttribute);
        void UpdateCategoryAttribute(CategoryAttribute categoryAttribute);
        void DeleteCategoryMapping(CategoryAttributeMapping categoryAttributeMapping);
        void DeleteCategoryMapping(Guid categoryAttributeMappingId);
        bool HasAnyMappings(Guid categoryAttributeId);
        IList<CategoryAttributeMapping> GetCategoryAttributeMappingsByCategoryId(Guid categoryId);
        CategoryAttributeMapping GetCategoryAttributeMapping(Guid categoryAttributeMapping);
        void InsertCategoryAttributeMapping(CategoryAttributeMapping categoryAttributeMapping);
        void UpdateCategoryAttributeMapping(CategoryAttributeMapping categoryAttributeMapping);
        void DeleteCategoryAttributeValue(CategoryAttributeValue categoryAttributeValue);
        void DeleteCategoryAttributeValue(Guid categoryAttributeValueId);
        IList<CategoryAttributeValue> GetCategoryAttributeValues(Guid categoryAttributeMappingId);
        IPagedList<CategoryAttributeValue> GetPagedCategoryAttributeValues(Guid categoryId,
            Guid categoryAttributeId, int pageIndex = 0, int pageSize = Int32.MaxValue);
        CategoryAttributeValue GetCategoryAttributeValueById(Guid categoyAttributeValueId);
        void InsertCategoryAttributeValue(CategoryAttributeValue categoryAttributeValue);
        void UpdateCategoryAttributeValue(CategoryAttributeValue categoryAttributeValue);
    }
}