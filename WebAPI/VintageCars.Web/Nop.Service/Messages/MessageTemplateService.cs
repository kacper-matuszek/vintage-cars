using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Domain.Messages;
using Nop.Data;
using Nop.Service.Caching;

namespace Nop.Service.Messages
{
    public partial class MessageTemplateService : IMessageTemplateService
    {
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MessageTemplate> _messageTemplateRepository;

        public MessageTemplateService(ICacheKeyService cacheKeyService, IStaticCacheManager staticCacheManager, IRepository<MessageTemplate> messageTemplateRepository)
        {
            _cacheKeyService = cacheKeyService;
            _staticCacheManager = staticCacheManager;
            _messageTemplateRepository = messageTemplateRepository;
        }

        /// <summary>
        /// Gets message templates by the name
        /// </summary>
        /// <param name="messageTemplateName">Message template name</param>
        /// <param name="storeId">Store identifier; pass null to load all records</param>
        /// <returns>List of message templates</returns>
        public virtual IList<MessageTemplate> GetMessageTemplatesByName(string messageTemplateName, int? storeId = null)
        {
            if (string.IsNullOrWhiteSpace(messageTemplateName))
                throw new ArgumentException(nameof(messageTemplateName));

            var key = _cacheKeyService.PrepareKeyForDefaultCache(NopMessageDefaults.MessageTemplatesByNameCacheKey, messageTemplateName, storeId);

            return _staticCacheManager.Get(key, () =>
            {
                //get message templates with the passed name
                var templates = _messageTemplateRepository.Table
                    .Where(messageTemplate => messageTemplate.Name.Equals(messageTemplateName))
                    .OrderBy(messageTemplate => messageTemplate.Id).ToList();

                return templates;
            });
        }
    }
}
