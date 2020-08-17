using System;
using System.Collections.Generic;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Service.Settings;

namespace Nop.Service.Installation
{
    public class InstallationService : IInstallationService
    {
        #region Fields

        private readonly IRepository<Core.Domain.Stores.Store> _storeRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IRepository<Language> _languageRepository;

        #endregion

        public InstallationService(IRepository<Core.Domain.Stores.Store> storeRepository,
            IRepository<CustomerRole> customerRoleRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            _customerRoleRepository = customerRoleRepository ?? throw new ArgumentNullException(nameof(customerRoleRepository));
        }

        public virtual void InstallRequiredData()
        {
            InstallStores();
            InstallLanguages();
            InstallRoles();
            InstallSettings();
        }

        #region Defaults

        protected virtual void InstallStores()
        {
            var stores = new List<Core.Domain.Stores.Store>
            {
                new Core.Domain.Stores.Store
                {
                    Name = "Vintage Cars",
                    Url = "https://localhost:44318",
                    SslEnabled = true,
                    Hosts = "vintage-cars.com,www.vintage-cars.com",
                    DisplayOrder = 1,
                    //should we set some default company info?
                    CompanyName = "Vintage Cars",
                    CompanyAddress = "your company country, state, zip, street, etc",
                    CompanyPhoneNumber = "(123) 456-78901",
                    CompanyVat = null
                }
            };

            _storeRepository.Insert(stores);
        }

        protected virtual void InstallRoles()
        {
            var crAdministrators = new CustomerRole
            {
                Name = "Administrators",
                Active = true,
                IsSystemRole = true,
                SystemName = NopCustomerDefaults.AdministratorsRoleName
            };
            var crForumModerators = new CustomerRole
            {
                Name = "Forum Moderators",
                Active = true,
                IsSystemRole = true,
                SystemName = NopCustomerDefaults.ForumModeratorsRoleName
            };
            var crRegistered = new CustomerRole
            {
                Name = "Registered",
                Active = true,
                IsSystemRole = true,
                SystemName = NopCustomerDefaults.RegisteredRoleName
            };
            var crGuests = new CustomerRole
            {
                Name = "Guests",
                Active = true,
                IsSystemRole = true,
                SystemName = NopCustomerDefaults.GuestsRoleName
            };
            var crVendors = new CustomerRole
            {
                Name = "Vendors",
                Active = true,
                IsSystemRole = true,
                SystemName = NopCustomerDefaults.VendorsRoleName
            };
            var customerRoles = new List<CustomerRole>
            {
                crAdministrators,
                crForumModerators,
                crRegistered,
                crGuests,
                crVendors
            };
            _customerRoleRepository.Insert(customerRoles);
        }

        protected virtual void InstallSettings()
        {
            var settingsService = EngineContext.Current.Resolve<ISettingService>();
            settingsService.SaveSetting(new CaptchaSettings()
            {
                ReCaptchaApiUrl = "https://www.google.com/recaptcha/api/siteverify",
                Enabled = true,
                CaptchaType = CaptchaType.CheckBoxReCaptchaV2,
                ReCaptchaV3ScoreThreshold = 0.5M,
                ReCaptchaPrivateKey = "6LcrH2oUAAAAAIEGnSLm4WtPVkU51pPa2woS5GWe",
                ReCaptchaPublicKey = "6LcrH2oUAAAAABK1XuahqJ5OYUIM7h0xCJgveTr1",
                ReCaptchaDefaultLanguage = string.Empty,
                ReCaptchaRequestTimeout = 20,
                ReCaptchaTheme = string.Empty
            });
        }

        protected virtual void InstallLanguages()
        {
            var language = new Language
            {
                Name = "English",
                LanguageCulture = "en-US",
                UniqueSeoCode = "en",
                FlagImageFileName = "us.png",
                Published = true,
                DisplayOrder = 1
            };
            _languageRepository.Insert(language);
        }

        #endregion
    }
}
