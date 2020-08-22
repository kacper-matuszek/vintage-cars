using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Caching.Extensions;
using Nop.Service.Extensions;
using Nop.Service.Settings;
using Nop.Services.Logging;

namespace Nop.Service.Localization
{
    /// <summary>
    /// Provides information about localization
    /// </summary>
    public partial class LocalizationService : ILocalizationService
    {
        #region Fields

        private readonly ICacheKeyService _cacheKeyService;
        private readonly IRepository<Language> _languageRepository;
        private readonly ILogger _logger;
        private readonly IRepository<LocaleStringResource> _lsrRepository;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public LocalizationService(ICacheKeyService cacheKeyService,
            IRepository<Language> languageRepository,
            ILogger logger,
            IRepository<LocaleStringResource> lsrRepository,
            ISettingService settingService,
            IStaticCacheManager staticCacheManager,
            LocalizationSettings localizationSettings)
        {
            _cacheKeyService = cacheKeyService;
            _languageRepository = languageRepository;
            _logger = logger;
            _lsrRepository = lsrRepository;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Insert resources
        /// </summary>
        /// <param name="resources">Resources</param>
        protected virtual void InsertLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            //insert
            _lsrRepository.Insert(resources);
        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Locale string resources</returns>
        protected virtual IList<LocaleStringResource> GetAllResources(Guid languageId)
        {
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        where l.LanguageId == languageId
                        select l;

            var locales = query.ToList();

            return locales;
        }

        /// <summary>
        /// Update resources
        /// </summary>
        /// <param name="resources">Resources</param>
        protected virtual void UpdateLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            //update
            _lsrRepository.Update(resources);
        }

        protected virtual HashSet<(string name, string value)> LoadLocaleResourcesFromStream(StreamReader xmlStreamReader, string language)
        {
            var result = new HashSet<(string name, string value)>();

            using (var xmlReader = XmlReader.Create(xmlStreamReader))
                while (xmlReader.ReadToFollowing("Language"))
                {
                    if (xmlReader.NodeType != XmlNodeType.Element)
                        continue;

                    using var languageReader = xmlReader.ReadSubtree();
                    while (languageReader.ReadToFollowing("LocaleResource"))
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.GetAttribute("Name") is string name)
                        {
                            using var lrReader = languageReader.ReadSubtree();
                            if (lrReader.ReadToFollowing("Value") && lrReader.NodeType == XmlNodeType.Element)
                                result.Add((name, lrReader.ReadString()));
                        }

                    break;
                }

            return result;
        }

        private static Dictionary<string, KeyValuePair<Guid, string>> ResourceValuesToDictionary(IEnumerable<LocaleStringResource> locales)
        {
            //format: <name, <id, value>>
            var dictionary = new Dictionary<string, KeyValuePair<Guid, string>>();
            foreach (var locale in locales)
            {
                var resourceName = locale.ResourceName.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<Guid, string>(locale.Id, locale.ResourceValue));
            }

            return dictionary;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Delete(localeStringResource);
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="localeStringResourceId">Locale string resource identifier</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId)
        {
            if (localeStringResourceId == 0)
                return null;

            return _lsrRepository.ToCachedGetById(localeStringResourceId);
        }


        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName, Guid languageId,
            bool logIfNotFound = true)
        {
            var query = from lsr in _lsrRepository.Table
                        orderby lsr.ResourceName
                        where lsr.LanguageId == languageId && lsr.ResourceName == resourceName
                        select lsr;

            var localeStringResource = query.FirstOrDefault();

            if (localeStringResource == null && logIfNotFound)
                _logger.Warning($"Resource string ({resourceName}) not found. Language ID = {languageId}");

            return localeStringResource;
        }

        /// <summary>
        /// Inserts a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Insert(localeStringResource);
        }

        /// <summary>
        /// Updates the locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Update(localeStringResource);
        }
        
        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey)
        {
            //for now only one language can be stored
            var languageId = (from language in _languageRepository.Table
                         select language.Id).FirstOrDefault();

            return languageId != default(Guid) ? GetResource(resourceKey, languageId) : string.Empty;
        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether an empty string will be returned if a resource is not found and default value is set to empty string</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey, Guid languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            var result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            //gradual loading
            var key = _cacheKeyService.PrepareKeyForDefaultCache(NopLocalizationDefaults.LocaleStringResourcesByResourceNameCacheKey
                , languageId, resourceKey);

            var query = from l in _lsrRepository.Table
                        where l.ResourceName == resourceKey
                              && l.LanguageId == languageId
                        select l.ResourceValue;

            var lsr = query.ToCachedFirstOrDefault(key);

            if (lsr != null)
                result = lsr;

            if (!string.IsNullOrEmpty(result))
                return result;

            if (logIfNotFound)
                _logger.Warning($"Resource string ({resourceKey}) is not found. Language ID = {languageId}");

            if (!string.IsNullOrEmpty(defaultValue))
            {
                result = defaultValue;
            }
            else
            {
                if (!returnEmptyIfNotFound)
                    result = resourceKey;
            }

            return result;
        }

        /// <summary>
        /// Export language resources to XML
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Result in XML format</returns>
        public virtual string ExportResourcesToXml(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));
            using var stream = new MemoryStream();
            using (var xmlWriter = new XmlTextWriter(stream, Encoding.UTF8))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Language");
                xmlWriter.WriteAttributeString("Name", language.Name);
                xmlWriter.WriteAttributeString("SupportedVersion", NopVersion.CurrentVersion);

                var resources = GetAllResources(language.Id);
                foreach (var resource in resources)
                {
                    xmlWriter.WriteStartElement("LocaleResource");
                    xmlWriter.WriteAttributeString("Name", resource.ResourceName);
                    xmlWriter.WriteElementString("Value", null, resource.ResourceValue);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        /// <summary>
        /// Import language resources from XML file
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="xmlStreamReader">Stream reader of XML file</param>
        /// <param name="updateExistingResources">A value indicating whether to update existing resources</param>
        public virtual void ImportResourcesFromXml(Language language, StreamReader xmlStreamReader, bool updateExistingResources = true)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (xmlStreamReader.EndOfStream)
                return;

            var lsNamesList = _lsrRepository.Table
                .Where(lsr => lsr.LanguageId == language.Id)
                .ToDictionary(lsr => lsr.ResourceName, lsr => lsr);

            var lrsToUpdateList = new List<LocaleStringResource>();
            var lrsToInsertList = new Dictionary<string, LocaleStringResource>();

            foreach (var (name, value) in LoadLocaleResourcesFromStream(xmlStreamReader, language.Name))
            {
                if (lsNamesList.ContainsKey(name))
                {
                    if (!updateExistingResources)
                        continue;

                    var lsr = lsNamesList[name];
                    lsr.ResourceValue = value;
                    lrsToUpdateList.Add(lsr);
                }
                else
                {
                    var lsr = new LocaleStringResource { LanguageId = language.Id, ResourceName = name, ResourceValue = value };
                    if (lrsToInsertList.ContainsKey(name))
                        lrsToInsertList[name] = lsr;
                    else
                        lrsToInsertList.Add(name, lsr);
                }
            }

            foreach (var lrsToUpdate in lrsToUpdateList)
                _lsrRepository.Update(lrsToUpdate);

            _lsrRepository.Insert(lrsToInsertList.Values);

            //clear cache
            _staticCacheManager.RemoveByPrefix(NopLocalizationDefaults.LocaleStringResourcesPrefixCacheKey);
        }
        #endregion
    }
}
