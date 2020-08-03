using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Nop.Data;

namespace Nop.Service.Installation
{
    public class InstallationService : IInstallationService
    {
        #region Fields

        private readonly IRepository<Core.Domain.Stores.Store> _storeRepository;

        #endregion

        public InstallationService(IRepository<Core.Domain.Stores.Store> storeRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
        }

        public virtual void InstallRequiredData()
        {
            InstallStores();
        }

        #region Defaults

        protected virtual void InstallStores()
        {
            var stores = new List<Core.Domain.Stores.Store>
            {
                new Core.Domain.Stores.Store
                {
                    Name = "Vintage Cars",
                    Url = "http://localhost:44318",
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
        #endregion
    }
}
