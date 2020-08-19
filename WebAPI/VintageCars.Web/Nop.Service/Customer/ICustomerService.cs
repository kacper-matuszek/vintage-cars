using System;
using System.Collections.Generic;
using System.Text;
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
        bool IsInCustomerRole(Core.Domain.Customers.Customer customer, string customerRoleSystemName, bool onlyActiveCustomerRoles = true);

        /// <summary>
        /// Gets list of customer roles
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>Result</returns>
        IList<CustomerRole> GetCustomerRoles(Core.Domain.Customers.Customer customer, bool showHidden = false);

        void AddCustomerRoleMapping(CustomerCustomerRoleMapping roleMapping);
        CustomerRole GetCustomerRoleBySystemName(string systemName);
        void InsertCustomerPassword(CustomerPassword customerPassword);
        void UpdateCustomerRole(CustomerRole customerRole);
        void UpdateCustomer(Core.Domain.Customers.Customer customer);
    }
}
