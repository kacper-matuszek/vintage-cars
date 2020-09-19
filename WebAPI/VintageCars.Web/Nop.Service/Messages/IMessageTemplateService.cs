using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Messages;

namespace Nop.Service.Messages
{
    public interface IMessageTemplateService
    {
        IList<MessageTemplate> GetMessageTemplatesByName(string messageTemplateName, Guid? storeId = null);
    }
}
