using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Requests.Customers;
using Nop.Service.Localization;
using Nop.Service.Security;

namespace Nop.Service.Customer
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        private readonly CustomerSettings _customerSettings;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerService _customerService;
        private readonly IEncryptionService _encryptionService;

        public CustomerRegistrationService(CustomerSettings customerSettings, ILocalizationService localizationService, ICustomerService customerService, IEncryptionService encryptionService)
        {
            _customerSettings = customerSettings;
            _localizationService = localizationService;
            _customerService = customerService;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Errors if something goes wrong</returns>
        public virtual IEnumerable<string> RegisterCustomer(CustomerRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Customer == null)
                throw new ArgumentException("Can't load current customer");

            var result = new List<string>();
            var validations = new Dictionary<Func<bool>, string>()
            {
                {request.Customer.IsSearchEngineAccount, "Customer.RegisterCustomer.SearchEngineAccount.Validation"},
                {request.Customer.IsBackgroundTaskAccount, "Customer.RegisterCustomer.BackgroundTask.Validation"},
                {
                    () => _customerService.IsRegistered(request.Customer),
                    "Customer.RegisterCustomer.IsRegistered.Validation"
                },
            };
            foreach (var validation in validations.Where(validation => validation.Key.Invoke()))
            {
                result.Add(_localizationService.GetResource(validation.Value));
                return result;
            }
            //if (_customerSettings.UsernamesEnabled && string.IsNullOrEmpty(request.Username))
            //{
            //    result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameIsNotProvided"));
            //    return result;
            //}

            //validate unique user
            if (_customerService.GetCustomerByEmail(request.Customer?.Email) != null)
            {
                result.Add(_localizationService.GetResource("Customer.RegisterCustomer.EmailAlreadyExist.Validation"));
                return result;
            }

            //if (_customerSettings.UsernamesEnabled && _customerService.GetCustomerByUsername(request.Username) != null)
            //{
            //    result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameAlreadyExists"));
            //    return result;
            //}
            var customerPassword = new CustomerPassword
            {
                CustomerId = request.Customer.Id,
                PasswordFormat = request.PasswordFormat,
                CreatedOnUtc = DateTime.UtcNow
            };
            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    customerPassword.Password = request.Password;
                    break;
                case PasswordFormat.Encrypted:
                    customerPassword.Password = _encryptionService.EncryptText(request.Password);
                    break;
                case PasswordFormat.Hashed:
                    var saltKey = _encryptionService.CreateSaltKey(NopCustomerServicesDefaults.PasswordSaltKeySize);
                    customerPassword.PasswordSalt = saltKey;
                    customerPassword.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, _customerSettings.HashedPasswordFormat);
                    break;
            }

            _customerService.InsertCustomer(request.Customer);
            _customerService.InsertCustomerPassword(customerPassword);

            //add to 'Registered' role
            var registeredRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.RegisteredRoleName);
            if (registeredRole == null)
                throw new NopException("'Registered' role could not be loaded");

            _customerService.AddCustomerRoleMapping(new CustomerCustomerRoleMapping { CustomerId = request.Customer.Id, CustomerRoleId = registeredRole.Id });

            ////remove from 'Guests' role            
            //if (_customerService.IsGuest(request.Customer))
            //{
            //    var guestRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.GuestsRoleName);
            //    _customerService.RemoveCustomerRoleMapping(request.Customer, guestRole);
            //}
            return result;
        }

        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        protected bool PasswordsMatch(CustomerPassword customerPassword, string enteredPassword)
        {
            if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (customerPassword.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt, _customerSettings.HashedPasswordFormat);
                    break;
            }

            if (customerPassword.Password == null)
                return false;

            return customerPassword.Password.Equals(savedPassword);
        }

        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual CustomerLoginResults ValidateCustomer(string usernameOrEmail, string password)
        {
            var customer = _customerSettings.UsernamesEnabled ?
                _customerService.GetCustomerByUsername(usernameOrEmail) :
                _customerService.GetCustomerByEmail(usernameOrEmail);

            if (customer == null)
                return CustomerLoginResults.CustomerNotExist;
            if (customer.Deleted)
                return CustomerLoginResults.Deleted;
            if (!customer.Active)
                return CustomerLoginResults.NotActive;
            //only registered can login
            if (!_customerService.IsRegistered(customer))
                return CustomerLoginResults.NotRegistered;
            //check whether a customer is locked out
            if (customer.CannotLoginUntilDateUtc.HasValue && customer.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
                return CustomerLoginResults.LockedOut;

            if (!PasswordsMatch(_customerService.GetCurrentPassword(customer.Id), password))
            {
                //wrong password
                customer.FailedLoginAttempts++;
                if (_customerSettings.FailedPasswordAllowedAttempts > 0 &&
                    customer.FailedLoginAttempts >= _customerSettings.FailedPasswordAllowedAttempts)
                {
                    //lock out
                    customer.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(_customerSettings.FailedPasswordLockoutMinutes);
                    //reset the counter
                    customer.FailedLoginAttempts = 0;
                }

                _customerService.UpdateCustomer(customer);

                return CustomerLoginResults.WrongPassword;
            }

            //update login details
            customer.FailedLoginAttempts = 0;
            customer.CannotLoginUntilDateUtc = null;
            customer.RequireReLogin = false;
            customer.LastLoginDateUtc = DateTime.UtcNow;
            _customerService.UpdateCustomer(customer);

            return CustomerLoginResults.Successful;
        }
    }
}
