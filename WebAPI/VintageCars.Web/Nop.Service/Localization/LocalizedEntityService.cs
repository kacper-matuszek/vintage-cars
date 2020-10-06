using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Caching.Extensions;

namespace Nop.Service.Localization
{
    public partial class LocalizedEntityService : ILocalizedEntityService
    {
        #region Fields
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IRepository<LocalizedProperty> _localizedPropertyRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly LocalizationSettings _localizationSettings;
        #endregion

        public LocalizedEntityService(ICacheKeyService cacheKeyService,
            IRepository<LocalizedProperty> localizedPropertyRepository,
            IStaticCacheManager staticCacheManager,
            LocalizationSettings localizationSettings)
        {
            _cacheKeyService = cacheKeyService;
            _localizedPropertyRepository = localizedPropertyRepository;
            _staticCacheManager = staticCacheManager;
            _localizationSettings = localizationSettings;
        }

        #region Utilities

        /// <summary>
        /// Gets localized properties
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="localeKeyGroup">Locale key group</param>
        /// <returns>Localized properties</returns>
        public virtual IList<LocalizedProperty> GetLocalizedProperties(Guid entityId, string localeKeyGroup)
        {
            if (entityId == default || string.IsNullOrEmpty(localeKeyGroup))
                return new List<LocalizedProperty>();

            var query = from lp in _localizedPropertyRepository.Table
                orderby lp.Id
                where lp.EntityId == entityId &&
                      lp.LocaleKeyGroup == localeKeyGroup
                select lp;

            var props = query.ToList();

            return props;
        }

        /// <summary>
        /// Gets all cached localized properties
        /// </summary>
        /// <returns>Cached localized properties</returns>
        public virtual IList<LocalizedProperty> GetAllLocalizedProperties()
        {
            var query = from lp in _localizedPropertyRepository.Table
                select lp;

            return query.ToCachedList(_cacheKeyService.PrepareKeyForDefaultCache(NopLocalizationDefaults.LocalizedPropertyAllCacheKey));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Deletes a localized property
        /// </summary>
        /// <param name="localizedProperty">Localized property</param>
        public virtual void DeleteLocalizedProperty(LocalizedProperty localizedProperty)
        {
            if (localizedProperty == null)
                throw new ArgumentNullException(nameof(localizedProperty));

            _localizedPropertyRepository.Delete(localizedProperty);
        }

        /// <summary>
        /// Gets a localized property
        /// </summary>
        /// <param name="localizedPropertyId">Localized property identifier</param>
        /// <returns>Localized property</returns>
        public virtual LocalizedProperty GetLocalizedPropertyById(Guid localizedPropertyId)
        {
            if (localizedPropertyId == default)
                return null;

            return _localizedPropertyRepository.GetById(localizedPropertyId);
        }

        /// <summary>
        /// Find localized value
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="localeKeyGroup">Locale key group</param>
        /// <param name="localeKey">Locale key</param>
        /// <returns>Found localized value</returns>
        public virtual string GetLocalizedValue(Guid languageId, Guid entityId, string localeKeyGroup, string localeKey)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(NopLocalizationDefaults.LocalizedPropertyCacheKey
                , languageId, entityId, localeKeyGroup, localeKey);

            return _staticCacheManager.Get(key, () =>
            {
                var source = _localizationSettings.LoadAllLocalizedPropertiesOnStartup
                    //load all records (we know they are cached)
                    ? GetAllLocalizedProperties().AsQueryable()
                    //gradual loading
                    : _localizedPropertyRepository.Table;

                var query = from lp in source
                            where lp.LanguageId == languageId &&
                                  lp.EntityId == entityId &&
                                  lp.LocaleKeyGroup == localeKeyGroup &&
                                  lp.LocaleKey == localeKey
                            select lp.LocaleValue;

                //little hack here. nulls aren't cacheable so set it to ""
                var localeValue = query.FirstOrDefault() ?? string.Empty;

                return localeValue;
            });
        }

        /// <summary>
        /// Inserts a localized property
        /// </summary>
        /// <param name="localizedProperty">Localized property</param>
        public virtual void InsertLocalizedProperty(LocalizedProperty localizedProperty)
        {
            if (localizedProperty == null)
                throw new ArgumentNullException(nameof(localizedProperty));

            _localizedPropertyRepository.Insert(localizedProperty);
        }

        /// <summary>
        /// Updates the localized property
        /// </summary>
        /// <param name="localizedProperty">Localized property</param>
        public virtual void UpdateLocalizedProperty(LocalizedProperty localizedProperty)
        {
            if (localizedProperty == null)
                throw new ArgumentNullException(nameof(localizedProperty));

            _localizedPropertyRepository.Update(localizedProperty);
        }

        /// <summary>
        /// Save localized value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="localeValue">Locale value</param>
        /// <param name="languageId">Language ID</param>
        public virtual void SaveLocalizedValue<T>(T entity,
            Expression<Func<T, string>> keySelector,
            string localeValue,
            Guid languageId) where T : BaseEntity, ILocalizedEntity
        {
            SaveLocalizedValue<T, string>(entity, keySelector, localeValue, languageId);
        }

        /// <summary>
        /// Save localized value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="localeValue">Locale value</param>
        /// <param name="languageId">Language ID</param>
        public virtual void SaveLocalizedValue<T, TPropType>(T entity,
            Expression<Func<T, TPropType>> keySelector,
            TPropType localeValue,
            Guid languageId) where T : BaseEntity, ILocalizedEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (languageId == default)
                throw new ArgumentOutOfRangeException(nameof(languageId), "Language ID should not be 0");

            if (!(keySelector.Body is MemberExpression member))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            //load localized value (check whether it's a cacheable entity. In such cases we load its original entity type)
            var localeKeyGroup = entity.GetType().Name;
            var localeKey = propInfo.Name;

            var props = GetLocalizedProperties(entity.Id, localeKeyGroup);
            var prop = props.FirstOrDefault(lp => lp.LanguageId == languageId &&
                lp.LocaleKey.Equals(localeKey, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            var localeValueStr = CommonHelper.To<string>(localeValue);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(localeValueStr))
                {
                    //delete
                    DeleteLocalizedProperty(prop);
                }
                else
                {
                    //update
                    prop.LocaleValue = localeValueStr;
                    UpdateLocalizedProperty(prop);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(localeValueStr))
                    return;

                //insert
                prop = new LocalizedProperty
                {
                    EntityId = entity.Id,
                    LanguageId = languageId,
                    LocaleKey = localeKey,
                    LocaleKeyGroup = localeKeyGroup,
                    LocaleValue = localeValueStr
                };
                InsertLocalizedProperty(prop);
            }
        }
        #endregion
    }
}
