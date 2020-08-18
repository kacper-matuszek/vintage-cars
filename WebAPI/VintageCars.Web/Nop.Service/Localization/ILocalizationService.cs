using System;
using System.IO;
using Nop.Core.Domain.Localization;

namespace Nop.Service.Localization
{
    public partial interface ILocalizationService
    {
        void DeleteLocaleStringResource(LocaleStringResource localeStringResource);
        LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId);
        LocaleStringResource GetLocaleStringResourceByName(string resourceName, Guid languageId,
            bool logIfNotFound = true);
        void InsertLocaleStringResource(LocaleStringResource localeStringResource);
        void UpdateLocaleStringResource(LocaleStringResource localeStringResource);
        string GetResource(string resourceKey);
        string GetResource(string resourceKey, Guid languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false);
        string ExportResourcesToXml(Language language);
        void ImportResourcesFromXml(Language language, StreamReader xmlStreamReader,
            bool updateExistingResources = true);
    }
}
