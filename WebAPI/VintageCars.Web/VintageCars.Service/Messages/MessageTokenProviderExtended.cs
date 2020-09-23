using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain;
using Nop.Core.Domain.Stores;
using Nop.Service.Common;
using Nop.Service.Messages;
using Nop.Service.Store;
using VintageCars.Service.Infrastructure;

namespace VintageCars.Service.Messages
{
    public class MessageTokenProviderExtended : MessageTokenProvider
    {
        private readonly IInfrastructureService _infraService;

        public MessageTokenProviderExtended(IStoreService storeService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IGenericAttributeService genericAttributeService,
            StoreInformationSettings storeInformationSettings,
            IInfrastructureService infrastructureService) 
            : base(storeService,
            urlHelperFactory,
            actionContextAccessor,
            genericAttributeService,
            storeInformationSettings)
        {
            _infraService = infrastructureService;
        }

        protected override Store GetCurrentStore()
        {
            return _infraService.Cache.Store;
        }
    }
}
