using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Tasks;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Service.Customer;
using Nop.Service.Localization;
using Nop.Service.Settings;


namespace Nop.Service.Installation
{
    public class InstallationService : IInstallationService
    {
        #region Fields
        protected readonly INopFileProvider _fileProvider;
        protected readonly IRepository<Core.Domain.Stores.Store> _storeRepository;
        protected readonly IRepository<CustomerRole> _customerRoleRepository;
        protected readonly IRepository<Language> _languageRepository;
        protected readonly IRepository<MessageTemplate> _messageTemplateRepository;
        protected readonly IRepository<EmailAccount> _emailAccountRepository;
        protected readonly IRepository<ScheduleTask> _scheduleTaskRepository;
        protected readonly ISettingService _settingsService;
        #endregion

        public InstallationService(IRepository<Core.Domain.Stores.Store> storeRepository,
            IRepository<CustomerRole> customerRoleRepository,
            IRepository<Language> languageRepository,
            IRepository<MessageTemplate> messageTemplate,
            IRepository<EmailAccount> emailAccountRepository,
            IRepository<ScheduleTask> scheduleTaskRepository,
            ISettingService settingService)
        {
            _fileProvider = CommonHelper.DefaultFileProvider;
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            _customerRoleRepository = customerRoleRepository ?? throw new ArgumentNullException(nameof(customerRoleRepository));
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
            _settingsService = settingService;
            _messageTemplateRepository = messageTemplate;
            _emailAccountRepository = emailAccountRepository;
            _scheduleTaskRepository = scheduleTaskRepository;
        }

        public virtual void InstallRequiredData()
        {
            InstallStores();
            InstallScheduleTasks();
            InstallEmailAccount();
            InstallMessageTemplate();
            InstallLanguages();
            InstallRoles();
            InstallSettings();
            InstallLocaleResources();
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
           // var settingsService = EngineContext.Current.Resolve<ISettingService>();
            _settingsService.SaveSetting(new CaptchaSettings()
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
            _settingsService.SaveSetting(new CustomerSettings()
            {
                PasswordMinLength = 8,
                PasswordRequireLowercase = true,
                PasswordRequireUppercase = true,
                PasswordRequireDigit = true,
                PasswordRequireNonAlphanumeric = true,
                UsernameValidationEnabled = true,
                UserRegistrationType = UserRegistrationType.Standard,
                DefaultPasswordFormat = PasswordFormat.Hashed,
                HashedPasswordFormat = NopCustomerServicesDefaults.DefaultHashedPasswordFormat,
                CustomerNameFormat = CustomerNameFormat.ShowUsernames,
            });

            var defaultEmailAccountId = _emailAccountRepository.Table.FirstOrDefault()?.Id ??
                                      throw new Exception("Brak domyslnego e-mail.");
            _settingsService.SaveSetting(new EmailAccountSettings()
            {
                DefaultEmailAccountId = defaultEmailAccountId,
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

        protected virtual void InstallLocaleResources()
        {
            //'English' language
            var language = _languageRepository.Table.Single(l => l.Name == "English");

            //save resources
            var directoryPath = _fileProvider.MapPath(NopInstallationDefaults.LocalizationResourcesPath);
            var pattern = $"*.{NopInstallationDefaults.LocalizationResourcesFileExtension}";
            foreach (var filePath in _fileProvider.EnumerateFiles(directoryPath, pattern))
            {
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                using var streamReader = new StreamReader(filePath);
                localizationService.ImportResourcesFromXml(language, streamReader);
            }
        }

        protected virtual void InstallEmailAccount()
        {
            var emailAccount = new EmailAccount()
            {
                Email = "test@mail.com",
                DisplayName = "Store name",
                Host = "smtp.mail.com",
                Port = 25,
                Username = "123",
                Password = "123",
                EnableSsl = false,
                UseDefaultCredentials = false
            };
            _emailAccountRepository.Insert(emailAccount);
        }

        protected virtual void InstallMessageTemplate()
        {
            var generalEmail = _emailAccountRepository.Table.FirstOrDefault() ??
                               throw new Exception("Nie można załadować domyślnego e-mail.");

            var messageTemplates = new List<MessageTemplate>()
            {
                new MessageTemplate()
                {
                    Name = MessageTemplateSystemNames.CustomerPasswordRecoveryMessage,
                    Subject = "%Store.Name%. Resetowanie hasła",
                    Body =
                        $"<a href=\"%Store.URL%\">%Store.Name%</a>{Environment.NewLine}<br />{Environment.NewLine}<br />{Environment.NewLine}Aby zresetować hasło naciśnij link <a href=\"%Customer.PasswordRecoveryURL%\">Resetowanie hasła</a>.{Environment.NewLine}<br />{Environment.NewLine}<br />{Environment.NewLine}%Store.Name%{Environment.NewLine}",
                    IsActive = true,
                    EmailAccountId = generalEmail.Id
                },
            };

            _messageTemplateRepository.Insert(messageTemplates);
        }

        protected virtual void InstallScheduleTasks()
        {
            var tasks = new List<ScheduleTask>()
            {
                new ScheduleTask
                {
                    Name = "Send emails",
                    Seconds = 60,
                    Type = "Nop.Services.Messages.QueuedMessagesSendTask, Nop.Services",
                    Enabled = true,
                    StopOnError = false
                },
            };
            _scheduleTaskRepository.Insert(tasks);
        }
        #endregion
    }
}
