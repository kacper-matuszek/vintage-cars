using System.Collections.Generic;
using Nop.Core.Domain.Messages;

namespace Nop.Service.Messages
{
    public interface IEmailSender
    {
        void SendEmail(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
            string replyTo = null, string replyToName = null,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            IDictionary<string, string> headers = null);
    }
}