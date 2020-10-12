using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;

namespace Nop.Service.Customer
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        Core.Domain.Customers.Customer GetCustomerByEmail(string email);

        /// <summary>
        /// Gets a value indicating whether customer is registered
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="onlyActiveCustomerRoles">A value indicating whether we should look only in active customer roles</param>
        /// <returns>Result</returns>
        bool IsRegistered(Core.Domain.Customers.Customer customer, bool onlyActiveCustomerRoles = true);

        /// <summary>
        /// Gets a value indicating whether customer is in a certain customer role
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="customerRoleSystemName">Customer role system name</param>
        /// <param name="onlyActiveCustomerRoles">A value indicating whether we should look only in active customer roles</param>
        /// <returns>Result</returns>
        bool IsInCustomerRole(Core.Domain.Customers.Customer customer,
            string customerRoleSystemName, bool onlyActiveCustomerRoles = true);

        /// <summary>
        /// Gets list of customer roles
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>Result</returns>
        IList<CustomerRole> GetCustomerRoles(Core.Domain.Customers.Customer customer, bool showHidden = false);

        /// <summary>
        /// Insert a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        void InsertCustomerPassword(CustomerPassword customerPassword);

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        CustomerRole GetCustomerRoleBySystemName(string systemName);

        /// <summary>
        /// Add a customer-customer role mapping
        /// </summary>
        /// <param name="roleMapping">Customer-customer role mapping</param>
        void AddCustomerRoleMapping(CustomerCustomerRoleMapping roleMapping);

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        void UpdateCustomerRole(CustomerRole customerRole);

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void UpdateCustomer(Core.Domain.Customers.Customer customer);

        /// <summary>
        /// Insert the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void InsertCustomer(Core.Domain.Customers.Customer customer);

        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        Core.Domain.Customers.Customer GetCustomerByUsername(string username);

        /// <summary>
        /// Get current customer password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer password</returns>
        CustomerPassword GetCurrentPassword(Guid customerId);

        /// <summary>
        /// Gets customer passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of customer passwords</returns>
        IList<CustomerPassword> GetCustomerPasswords(Guid? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null);

        /// <summary>
        /// Get full name
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer full name</returns>
        string GetCustomerFullName(Core.Domain.Customers.Customer customer);

        /// <summary>
        /// Get customer role identifiers
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>Customer role identifiers</returns>
        Guid[] GetCustomerRoleIds(Core.Domain.Customers.Customer customer, bool showHidden = false);

        /// <summary>
        /// Remove a customer-address mapping record
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="address">Address</param>
        void RemoveCustomerAddress(Core.Domain.Customers.Customer customer, Address address);

        /// <summary>
        /// Inserts a customer-address mapping record
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="address">Address</param>
        void InsertCustomerAddress(Core.Domain.Customers.Customer customer, Address address);

        /// <summary>
        /// Gets a list of addresses mapped to customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Result</returns>
        IList<Address> GetAddressesByCustomerId(Guid customerId);

        /// <summary>
        /// Gets a address mapped to customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <param name="addressId">Address identifier</param>
        /// <returns>Result</returns>
        Address GetCustomerAddress(Guid customerId, Guid addressId);

        /// <summary>
        /// Gets a customer billing address
        /// </summary>
        /// <param name="customer">Customer identifier</param>
        /// <returns>Result</returns>
        Address GetCustomerBillingAddress(Core.Domain.Customers.Customer customer);

        /// <summary>
        /// Gets a customer shipping address
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Result</returns>
        Address GetCustomerShippingAddress(Core.Domain.Customers.Customer customer);
    }
}