using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Stores;
using Nop.Data;
using VintageCars.Domain.Extensions;

namespace VintageCars.Service.Infrastructure
{
    public class InfrastructureService : IInfrastructureService
    {
        private readonly IStaticCacheManager _cacheManager;
        private readonly IRepository<Store> _storeRepository;
        private readonly IRepository<Language> _languageRepository;
        private DefaultCache _cache;

        public InfrastructureService(IStaticCacheManager cacheManager, IRepository<Store> storeRepository, IRepository<Language> languageRepository)
        {
            _cacheManager = cacheManager;
            _storeRepository = storeRepository;
            _languageRepository = languageRepository;
        }

        public DefaultCache Cache
        {
            get
            {
                if(_cache == null)
                    LoadCache();
                return _cache;
            }
            private set => _cache = value;
        }

        public void SetDefaultCache()
        {
            var idFuncs = new List<Func<BaseEntity>>()
            {
                () => _languageRepository.Table.FirstOrDefault(),
                () => _storeRepository.Table.FirstOrDefault(),
            };
            foreach (var enFunc in idFuncs)
            {
                var entity = enFunc.Invoke();
                if (entity == null || entity.Id.IsEmpty()) continue;

                var key = string.Format(NopCachingDefaults.NopObjectCacheKey, entity.GetType().Name);
                var cacheKey = new CacheKey(key, 1440);
                if(!_cacheManager.IsSet(cacheKey))
                    _cacheManager.Set(cacheKey, entity);
            }
        }

        private void LoadCache()
        {
            var language = _cacheManager.Get<Language>(new CacheKey(string.Format(NopCachingDefaults.NopObjectCacheKey, nameof(Language))), null);
            var store = _cacheManager.Get<Store>(new CacheKey(string.Format(NopCachingDefaults.NopObjectCacheKey, nameof(Store))), null);

            Cache = new DefaultCache(store, language);
        }
    }
}
