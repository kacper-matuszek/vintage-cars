using System;
using FluentScheduler;
using Nop.Service.Tasks;
using Nop.Services.Logging;

namespace Nop.Service.Messages.Task
{
    /// <summary>
    /// Represents a task for sending queued message 
    /// </summary>
    public partial class QueuedMessagesSendTask : IJobExtension
    {
        #region Fields

        private readonly IEmailAccountService _emailAccountService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IQueuedEmailService _queuedEmailService;

        #endregion

        #region Ctor

        public QueuedMessagesSendTask(IEmailAccountService emailAccountService,
            IEmailSender emailSender,
            ILogger logger,
            IQueuedEmailService queuedEmailService)
        {
            _emailAccountService = emailAccountService;
            _emailSender = emailSender;
            _logger = logger;
            _queuedEmailService = queuedEmailService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute()
        {
            OnBeforeExecute?.Invoke(this, new EventArgs());
            const int maxTries = 3;
            var queuedEmails = _queuedEmailService.SearchEmails(null, null, null, null,
                true, true, maxTries, false, 0, 500);
            foreach (var queuedEmail in queuedEmails)
            {
                var bcc = string.IsNullOrWhiteSpace(queuedEmail.Bcc)
                            ? null
                            : queuedEmail.Bcc.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var cc = string.IsNullOrWhiteSpace(queuedEmail.CC)
                            ? null
                            : queuedEmail.CC.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    _emailSender.SendEmail(_emailAccountService.GetEmailAccountById(queuedEmail.EmailAccountId),
                        queuedEmail.Subject,
                        queuedEmail.Body,
                       queuedEmail.From,
                       queuedEmail.FromName,
                       queuedEmail.To,
                       queuedEmail.ToName,
                       queuedEmail.ReplyTo,
                       queuedEmail.ReplyToName,
                       bcc,
                       cc);

                    queuedEmail.SentOnUtc = DateTime.UtcNow;
                }
                catch (Exception exc)
                {
                    _logger.Error($"Error sending e-mail. {exc.Message}", exc);
                }
                finally
                {
                    queuedEmail.SentTries += 1;
                    _queuedEmailService.UpdateQueuedEmail(queuedEmail);
                }
            }
            OnAfterExecute?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Events
        public event EventHandler OnAfterExecute;
        public event EventHandler OnBeforeExecute;
        #endregion
    }
}
