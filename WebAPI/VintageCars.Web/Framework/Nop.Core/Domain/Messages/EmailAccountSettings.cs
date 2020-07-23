using System;
using Nop.Core.Configuration;

namespace Nop.Core.Domain.Messages
{
    /// <summary>
    /// Email account settings
    /// </summary>
    public class EmailAccountSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a store default email account identifier
        /// </summary>
        public Guid DefaultEmailAccountId { get; set; }
    }
}
