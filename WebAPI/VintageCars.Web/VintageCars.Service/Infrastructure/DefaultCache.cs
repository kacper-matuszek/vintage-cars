using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Stores;

namespace VintageCars.Service.Infrastructure
{
    public class DefaultCache
    {
        public Store Store { get; }
        public Language Language { get; }

        public DefaultCache(Store store, Language language)
        {
            Store = store;
            Language = language;
        }
    }
}
