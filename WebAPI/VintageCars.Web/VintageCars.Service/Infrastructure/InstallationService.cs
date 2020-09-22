using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Tasks;
using Nop.Data;
using Nop.Service.Settings;

namespace VintageCars.Service.Infrastructure
{
    public class InstallationService : Nop.Service.Installation.InstallationService
    {
        public InstallationService(IRepository<Store> storeRepository,
            IRepository<CustomerRole> customerRoleRepository,
            IRepository<Language> languageRepository,
            IRepository<MessageTemplate> messageTemplateRepository,
            IRepository<EmailAccount> emailAccountRepository,
            IRepository<ScheduleTask> scheduleTaskRepository,
            ISettingService settingService) 
            : base(storeRepository,
            customerRoleRepository,
            languageRepository,
            messageTemplateRepository,
            emailAccountRepository,
            scheduleTaskRepository,
            settingService)
        {
        }
    }
}
