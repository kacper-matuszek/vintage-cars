using System;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Service.Caching.Extensions;

namespace Nop.Service.Localization
{
    public partial class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> _languageRepository;

        public LanguageService(IRepository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }

        /// <summary>
        /// Gets a language
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Language</returns>
        public virtual Language GetLanguageById(Guid languageId)
        {
            return languageId == Guid.Empty ? null : _languageRepository.ToCachedGetById(languageId);
        }
    }
}
