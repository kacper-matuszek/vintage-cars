using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Tax;
using Nop.Service.Common;
using Nop.Service.Store;

namespace Nop.Service.Messages
{
    public partial class MessageTokenProvider : IMessageTokenProvider
    {
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IGenericAttributeService _genericAttributeService;

        public MessageTokenProvider(IStoreService storeService,
            IStoreContext storeContext,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IGenericAttributeService genericAttributeService)
        {
            _storeService = storeService;
            _storeContext = storeContext;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _genericAttributeService = genericAttributeService;
        }

        /// <summary>
        /// Add customer tokens
        /// </summary>
        /// <param name="tokens">List of already added tokens</param>
        /// <param name="customer">Customer</param>
        public virtual void AddCustomerTokens(IList<Token> tokens, Core.Domain.Customers.Customer customer)
        {
            var firstName =
                _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.FirstNameAttribute);
            var lastName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.LastNameAttribute));
            tokens.Add(new Token("Customer.Email", customer.Email));
            tokens.Add(new Token("Customer.Username", customer.Username));
            tokens.Add(new Token("Customer.FullName", $"{firstName} {lastName}"));
            tokens.Add(new Token("Customer.FirstName", firstName));
            tokens.Add(new Token("Customer.LastName", lastName));
            tokens.Add(new Token("Customer.VatNumber", _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.VatNumberAttribute)));
            tokens.Add(new Token("Customer.VatNumberStatus", ((VatNumberStatus)_genericAttributeService.GetAttribute<int>(customer, NopCustomerDefaults.VatNumberStatusIdAttribute)).ToString()));

            //note: we do not use SEO friendly URLS for these links because we can get errors caused by having .(dot) in the URL (from the email address)
            var passwordRecoveryUrl = RouteUrl(routeName: "PasswordRecoveryConfirm", routeValues: new { token = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.PasswordRecoveryTokenAttribute), guid = customer.Id });
            var accountActivationUrl = RouteUrl(routeName: "AccountActivation", routeValues: new { token = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.AccountActivationTokenAttribute), guid = customer.Id });
            var emailRevalidationUrl = RouteUrl(routeName: "EmailRevalidation", routeValues: new { token = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.EmailRevalidationTokenAttribute), guid = customer.Id });
            var wishlistUrl = RouteUrl(routeName: "Wishlist", routeValues: new { customerGuid = customer.Id });
            tokens.Add(new Token("Customer.PasswordRecoveryURL", passwordRecoveryUrl, true));
            tokens.Add(new Token("Customer.AccountActivationURL", accountActivationUrl, true));
            tokens.Add(new Token("Customer.EmailRevalidationURL", emailRevalidationUrl, true));
            tokens.Add(new Token("Wishlist.URLForCustomer", wishlistUrl, true));
        }

        /// <summary>
        /// Generates an absolute URL for the specified store, routeName and route values
        /// </summary>
        /// <param name="storeId">Store identifier; Pass 0 to load URL of the current store</param>
        /// <param name="routeName">The name of the route that is used to generate URL</param>
        /// <param name="routeValues">An object that contains route values</param>
        /// <returns>Generated URL</returns>
        public virtual string RouteUrl(Guid storeId = default, string routeName = null, object routeValues = null)
        {
            //try to get a store by the passed identifier
            var store = _storeService.GetStoreById(storeId) ?? _storeContext.CurrentStore
                ?? throw new Exception("No store could be loaded");

            //ensure that the store URL is specified
            if (string.IsNullOrEmpty(store.Url))
                throw new Exception("URL cannot be null");

            //generate a URL with an absolute path
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var url = new PathString(urlHelper.Link(routeName, routeValues));

            //remove the application path from the generated URL if exists
            var pathBase = _actionContextAccessor.ActionContext?.HttpContext?.Request?.PathBase ?? PathString.Empty;
            url.StartsWithSegments(pathBase, out url);

            //compose the result
            return Uri.EscapeUriString(WebUtility.UrlDecode($"{store.Url.TrimEnd('/')}{url}"));
        }
    }
}
