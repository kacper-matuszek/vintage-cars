using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;
using MimeKit.Text;
using Nop.Core.Domain.Messages;

namespace Nop.Service.Messages
{
    public partial class EmailSender : IEmailSender
    {
        private readonly ISmtpBuilder _smtpBuilder;

        public EmailSender(ISmtpBuilder smtpBuilder)
        {
            _smtpBuilder = smtpBuilder;
        }

        public virtual void SendEmail(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
            string replyTo = null, string replyToName = null,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            IDictionary<string, string> headers = null)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(fromName, fromAddress));
            message.To.Add(new MailboxAddress(toName, toAddress));

            if (!string.IsNullOrEmpty(replyTo))
            {
                message.ReplyTo.Add(new MailboxAddress(replyToName, replyTo));
            }

            //BCC
            if (bcc != null)
            {
                foreach (var address in bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(new MailboxAddress(address.Trim()));
                }
            }

            //CC
            if (cc != null)
            {
                foreach (var address in cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
                {
                    message.Cc.Add(new MailboxAddress(address.Trim()));
                }
            }

            //content
            message.Subject = subject;

            //headers
            if (headers != null)
                foreach (var header in headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }

            var multipart = new Multipart("mixed")
            {
                new TextPart(TextFormat.Html) { Text = body }
            };

            message.Body = multipart;

            //send email
            using var smtpClient = _smtpBuilder.Build(emailAccount);
            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}
