using System;
using Nop.Core.Domain.Localization;

namespace Nop.Service.Localization
{
    public interface ILanguageService
    {
        Language GetLanguageById(Guid languageId);
    }
}
