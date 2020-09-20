using System.Collections.Generic;
using Nop.Core.Domain.Messages;

namespace Nop.Service.Messages
{
    public interface IMessageTokenProvider
    {
        /// <summary>
        /// Add customer tokens
        /// </summary>
        /// <param name="tokens">List of already added tokens</param>
        /// <param name="customer">Customer</param>
        void AddCustomerTokens(IList<Token> tokens, Core.Domain.Customers.Customer customer);

        void AddStoreTokens(IList<Token> tokens, Core.Domain.Stores.Store store, EmailAccount emailAccount);
    }
}