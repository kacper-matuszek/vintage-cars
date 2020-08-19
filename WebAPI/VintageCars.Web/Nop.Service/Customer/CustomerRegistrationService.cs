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
                result.Add(_localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists"));
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

            _customerService.InsertCustomerPassword(customerPassword);

            request.Customer.Active = request.IsApproved;

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
            _customerService.UpdateCustomer(request.Customer);

            return result;
        }
    }
}
