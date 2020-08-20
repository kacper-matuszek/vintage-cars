using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Domain.Customers;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Extensions;

namespace Nop.Service.Customer
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IRepository<Core.Domain.Customers.Customer> _customerRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IRepository<CustomerCustomerRoleMapping> _customerCustomerRoleMappingRepository;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<CustomerPassword> _customerPasswordRepository;

        public CustomerService(IRepository<Core.Domain.Customers.Customer> customerRepository, IRepository<CustomerRole> customerRoleRepository, IRepository<CustomerCustomerRoleMapping> customerCustomerRoleMappingRepository, ICacheKeyService cacheKeyService, IStaticCacheManager staticCacheManager, IRepository<CustomerPassword> customerPasswordRepository)
        {
            _customerRepository = customerRepository;
            _customerRoleRepository = customerRoleRepository;
            _customerCustomerRoleMappingRepository = customerCustomerRoleMappingRepository;
            _cacheKeyService = cacheKeyService;
            _staticCacheManager = staticCacheManager;
            _customerPasswordRepository = customerPasswordRepository;
        }

        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        public virtual Core.Domain.Customers.Customer GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _customerRepository.Table
                orderby c.Id
                where c.Email == email
                select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Gets a value indicating whether customer is registered
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="onlyActiveCustomerRoles">A value indicating whether we should look only in active customer roles</param>
        /// <returns>Result</returns>
        public virtual bool IsRegistered(Core.Domain.Customers.Customer customer, bool onlyActiveCustomerRoles = true)
        {
            return IsInCustomerRole(customer, NopCustomerDefaults.RegisteredRoleName, onlyActiveCustomerRoles);
        }

        /// <summary>
        /// Gets a value indicating whether customer is in a certain customer role
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="customerRoleSystemName">Customer role system name</param>
        /// <param name="onlyActiveCustomerRoles">A value indicating whether we should look only in active customer roles</param>
        /// <returns>Result</returns>
        public virtual bool IsInCustomerRole(Core.Domain.Customers.Customer customer,
            string customerRoleSystemName, bool onlyActiveCustomerRoles = true)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (string.IsNullOrEmpty(customerRoleSystemName))
                throw new ArgumentNullException(nameof(customerRoleSystemName));

            var customerRoles = GetCustomerRoles(customer, !onlyActiveCustomerRoles);

            return customerRoles?.Any(cr => cr.SystemName == customerRoleSystemName) ?? false;
        }

        /// <summary>
        /// Gets list of customer roles
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>Result</returns>
        public virtual IList<CustomerRole> GetCustomerRoles(Core.Domain.Customers.Customer customer, bool showHidden = false)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var query = from cr in _customerRoleRepository.Table
                join crm in _customerCustomerRoleMappingRepository.Table on cr.Id equals crm.CustomerRoleId
                where crm.CustomerId == customer.Id &&
                      (showHidden || cr.Active)
                select cr;

            var key = _cacheKeyService.PrepareKeyForShortTermCache(NopCustomerServicesDefaults.CustomerRolesCacheKey, customer, showHidden);

            return _staticCacheManager.Get(key, () => query.ToList());
        }

        /// <summary>
        /// Insert a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        public virtual void InsertCustomerPassword(CustomerPassword customerPassword)
        {
            if (customerPassword == null)
                throw new ArgumentNullException(nameof(customerPassword));

            _customerPasswordRepository.Insert(customerPassword);
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        public virtual CustomerRole GetCustomerRoleBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var key = _cacheKeyService.PrepareKeyForDefaultCache(NopCustomerServicesDefaults.CustomerRolesBySystemNameCacheKey, systemName);

            var query = from cr in _customerRoleRepository.Table
                orderby cr.Id
                where cr.SystemName == systemName
                select cr;
            var customerRole = query.ToCachedFirstOrDefault(key);

            return customerRole;
        }

        /// <summary>
        /// Add a customer-customer role mapping
        /// </summary>
        /// <param name="roleMapping">Customer-customer role mapping</param>
        public void AddCustomerRoleMapping(CustomerCustomerRoleMapping roleMapping)
        {
            if (roleMapping is null)
                throw new ArgumentNullException(nameof(roleMapping));

            _customerCustomerRoleMappingRepository.Insert(roleMapping);
        }

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void UpdateCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException(nameof(customerRole));

            _customerRoleRepository.Update(customerRole);
        }

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void UpdateCustomer(Core.Domain.Customers.Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            _customerRepository.Update(customer);
        }

    }
}
