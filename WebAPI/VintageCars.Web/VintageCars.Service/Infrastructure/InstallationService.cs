using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Service.Settings;

namespace VintageCars.Service.Infrastructure
{
    public class InstallationService : Nop.Service.Installation.InstallationService
    {
        public InstallationService(IRepository<Store> storeRepository, IRepository<CustomerRole> customerRoleRepository, IRepository<Language> languageRepository, ISettingService settingService) : base(storeRepository, customerRoleRepository, languageRepository, settingService)
        {
        }
    }
}
