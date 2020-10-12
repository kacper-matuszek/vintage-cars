using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Common;
using Nop.Service.Extensions;

namespace Nop.Service.Customer
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IRepository<Core.Domain.Customers.Customer> _customerRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IRepository<CustomerCustomerRoleMapping> _customerCustomerRoleMappingRepository;
        private readonly IRepository<CustomerAddressMapping> _customerAddressMappingRepository;
        private readonly IRepository<Address> _customerAddressRepository;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<CustomerPassword> _customerPasswordRepository;
        private readonly IGenericAttributeService _genericAttributeService;

        public CustomerService(IRepository<Core.Domain.Customers.Customer> customerRepository,
            IRepository<CustomerRole> customerRoleRepository,
            IRepository<CustomerCustomerRoleMapping> customerCustomerRoleMappingRepository,
            IRepository<CustomerAddressMapping> customerAddressMappingRepository,
            IRepository<Address> customerAddressRepository,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<CustomerPassword> customerPasswordRepository,
            IGenericAttributeService genericAttributeService)
        {
            _customerRepository = customerRepository;
            _customerRoleRepository = customerRoleRepository;
            _customerCustomerRoleMappingRepository = customerCustomerRoleMappingRepository;
            _customerAddressMappingRepository = customerAddressMappingRepository;
            _customerAddressRepository = customerAddressRepository;
            _cacheKeyService = cacheKeyService;
            _staticCacheManager = staticCacheManager;
            _customerPasswordRepository = customerPasswordRepository;
            _genericAttributeService = genericAttributeService;
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

        /// <summary>
        /// Insert the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void InsertCustomer(Core.Domain.Customers.Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            _customerRepository.Insert(customer);
        }

        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        public virtual Core.Domain.Customers.Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _customerRepository.Table
                orderby c.Id
                where c.Username == username
                select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get current customer password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer password</returns>
        public virtual CustomerPassword GetCurrentPassword(Guid customerId)
        {
            if (customerId == Guid.Empty)
                return null;

            //return the latest password
            return GetCustomerPasswords(customerId, passwordsToReturn: 1).FirstOrDefault();
        }

        /// <summary>
        /// Gets customer passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of customer passwords</returns>
        public virtual IList<CustomerPassword> GetCustomerPasswords(Guid? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
        {
            var query = _customerPasswordRepository.Table;

            //filter by customer
            if (customerId.HasValue)
                query = query.Where(password => password.CustomerId == customerId.Value);

            //filter by password format
            if (passwordFormat.HasValue)
                query = query.Where(password => password.PasswordFormatId == (int)passwordFormat.Value);

            //get the latest passwords
            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreatedOnUtc).Take(passwordsToReturn.Value);

            return query.ToList();
        }

        /// <summary>
        /// Get full name
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer full name</returns>
        public virtual string GetCustomerFullName(Core.Domain.Customers.Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var firstName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.FirstNameAttribute);
            var lastName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.LastNameAttribute);

            var fullName = string.Empty;
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                fullName = $"{firstName} {lastName}";
            else
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

                if (!string.IsNullOrWhiteSpace(lastName))
                    fullName = lastName;
            }

            return fullName;
        }

        /// <summary>
        /// Get customer role identifiers
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>Customer role identifiers</returns>
        public virtual Guid[] GetCustomerRoleIds(Core.Domain.Customers.Customer customer, bool showHidden = false)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var query = from cr in _customerRoleRepository.Table
                join crm in _customerCustomerRoleMappingRepository.Table on cr.Id equals crm.CustomerRoleId
                where crm.CustomerId == customer.Id &&
                      (showHidden || cr.Active)
                select cr.Id;

            var key = _cacheKeyService.PrepareKeyForShortTermCache(NopCustomerServicesDefaults.CustomerRoleIdsCacheKey, customer, showHidden);

            return _staticCacheManager.Get(key, () => query.ToArray());
        }

        #region Customer address mapping

        /// <summary>
        /// Remove a customer-address mapping record
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="address">Address</param>
        public virtual void RemoveCustomerAddress(Core.Domain.Customers.Customer customer, Address address)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (_customerAddressMappingRepository.Table.FirstOrDefault(m => m.AddressId == address.Id && m.CustomerId == customer.Id) is CustomerAddressMapping mapping)
            {
                if (customer.BillingAddressId == address.Id)
                    customer.BillingAddressId = null;
                if (customer.ShippingAddressId == address.Id)
                    customer.ShippingAddressId = null;

                _customerAddressMappingRepository.Delete(mapping);
            }
        }

        /// <summary>
        /// Inserts a customer-address mapping record
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="address">Address</param>
        public virtual void InsertCustomerAddress(Core.Domain.Customers.Customer customer, Address address)
        {
            if (customer is null)
                throw new ArgumentNullException(nameof(customer));

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            if (_customerAddressMappingRepository.Table.FirstOrDefault(m => m.AddressId == address.Id && m.CustomerId == customer.Id) is null)
            {
                var mapping = new CustomerAddressMapping
                {
                    AddressId = address.Id,
                    CustomerId = customer.Id
                };

                _customerAddressMappingRepository.Insert(mapping);
            }
        }

        /// <summary>
        /// Gets a list of addresses mapped to customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Result</returns>
        public virtual IList<Address> GetAddressesByCustomerId(Guid customerId)
        {
            var query = from address in _customerAddressRepository.Table
                        join cam in _customerAddressMappingRepository.Table on address.Id equals cam.AddressId
                        where cam.CustomerId == customerId
                        select address;

            var key = _cacheKeyService.PrepareKeyForShortTermCache(NopCustomerServicesDefaults.CustomerAddressesByCustomerIdCacheKey, customerId);

            return _staticCacheManager.Get(key, () => query.ToList());
        }

        /// <summary>
        /// Gets a address mapped to customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <param name="addressId">Address identifier</param>
        /// <returns>Result</returns>
        public virtual Address GetCustomerAddress(Guid customerId, Guid addressId)
        {
            if (customerId == default || addressId == default)
                return null;

            var query = from address in _customerAddressRepository.Table
                        join cam in _customerAddressMappingRepository.Table on address.Id equals cam.AddressId
                        where cam.CustomerId == customerId && address.Id == addressId
                        select address;

            var key = _cacheKeyService.PrepareKeyForShortTermCache(NopCustomerServicesDefaults.CustomerAddressCacheKeyCacheKey, customerId, addressId);

            return _staticCacheManager.Get(key, () => query.Single());
        }

        /// <summary>
        /// Gets a customer billing address
        /// </summary>
        /// <param name="customer">Customer identifier</param>
        /// <returns>Result</returns>
        public virtual Address GetCustomerBillingAddress(Core.Domain.Customers.Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException(nameof(customer));

            return GetCustomerAddress(customer.Id, customer.BillingAddressId ?? Guid.Empty);
        }

        /// <summary>
        /// Gets a customer shipping address
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Result</returns>
        public virtual Address GetCustomerShippingAddress(Core.Domain.Customers.Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException(nameof(customer));

            return GetCustomerAddress(customer.Id, customer.ShippingAddressId ?? Guid.Empty);
        }

        #endregion
    }
}
