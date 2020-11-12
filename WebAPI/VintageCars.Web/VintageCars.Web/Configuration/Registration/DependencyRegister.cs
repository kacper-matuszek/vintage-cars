using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Configuration;
using Nop.Core.Domain.Stores;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Catalog;
using Nop.Service.Common;
using Nop.Service.Country;
using Nop.Service.Customer;
using Nop.Service.Discounts;
using Nop.Service.Installation;
using Nop.Service.Localization;
using Nop.Service.Messages;
using Nop.Service.Products;
using Nop.Service.Security;
using Nop.Service.Settings;
using Nop.Service.Shipping;
using Nop.Service.Store;
using Nop.Service.Tasks;
using Nop.Services.Logging;
using VintageCars.Service.Catalog.Services;
using VintageCars.Service.Customers.Address;
using VintageCars.Service.Infrastructure;
using VintageCars.Service.Messages;
using InstallationService = VintageCars.Service.Infrastructure.InstallationService;

namespace VintageCars.Web.Configuration.Registration
{
    public class DependencyRegister : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //file provider
            builder.RegisterType<NopFileProvider>().As<INopFileProvider>().InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //data layer
            builder.RegisterType<DataProviderManager>().As<IDataProviderManager>().InstancePerDependency();
            builder.Register(context => context.Resolve<IDataProviderManager>().DataProvider).As<INopDataProvider>().InstancePerDependency();

            //repositories
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<MemoryCacheManager>()
                .As<ILocker>()
                .As<IStaticCacheManager>()
                .SingleInstance();

            //services
            builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<CacheKeyService>().As<ICacheKeyService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRegistrationService>().As<ICustomerRegistrationService>().InstancePerLifetimeScope();
            builder.RegisterType<InfrastructureService>().As<IInfrastructureService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageTokenProviderExtended>().As<IMessageTokenProvider>().InstancePerLifetimeScope();
            builder.RegisterType<MessageTemplateService>().As<IMessageTemplateService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailAccountService>().As<IEmailAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<Tokenizer>().As<ITokenizer>().InstancePerLifetimeScope();
            builder.RegisterType<QueuedEmailService>().As<IQueuedEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkflowMessageService>().As<IWorkflowMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<SmtpBuilder>().As<ISmtpBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<ManufacturerService>().As<IManufacturerService>().InstancePerLifetimeScope();
            builder.RegisterType<DiscountService>().As<IDiscountService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ExtendedCategoryService>().As<IExtendedCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizedEntityService>().As<ILocalizedEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductTagService>().As<IProductTagService>().InstancePerLifetimeScope();
            builder.RegisterType<ShippingService>().As<IShippingService>().InstancePerLifetimeScope();
            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerLifetimeScope();
            builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();
            builder.RegisterType<StateProvinceService>().As<IStateProvinceService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeService>().As<IProductAttributeService>().InstancePerLifetimeScope();

            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();

            builder.RegisterSource(new SettingsSource());
            RegisterMediatR(builder);
            RegisterJobs(builder);

            if (!DataSettingsManager.DatabaseIsInstalled)
                builder.RegisterType<InstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
        }

        private static void RegisterMediatR(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            var mediatRTypes = new Dictionary<Type, Assembly[]>
            {
                { typeof(IRequest<>), GetAssembliesWithEndName("Domain") },
                { typeof(IRequestHandler<,>), GetAssembliesWithEndName("Service") }
            };

            foreach (var (mediaType, assemblies) in mediatRTypes)
                builder.RegisterAssemblyTypes(assemblies)
                    .Where(type => type.IsClosedTypeOf(mediaType))
                    .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetAssembliesWithEndName("Service"))
                .Where(type => typeof(IJobExtension).IsAssignableFrom(type))
                .AsSelf().InstancePerLifetimeScope();
        }

        private static Assembly[] GetAssembliesWithEndName(string endName)
            => AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.GetName().Name.EndsWith(endName))
                .ToArray();

        public int Order => 0;
    }
    

    /// <summary>
    /// Setting source
    /// </summary>
    public class SettingsSource : IRegistrationSource
    {
        private static readonly MethodInfo _buildMethod =
            typeof(SettingsSource).GetMethod("BuildRegistration", BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Registrations for
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="registrations">Registrations</param>
        /// <returns>Registrations</returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Autofac.Core.Service service,
            Func<Autofac.Core.Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts == null || !typeof(ISettings).IsAssignableFrom(ts.ServiceType)) yield break;
            var buildMethod = _buildMethod.MakeGenericMethod(ts.ServiceType);
            yield return (IComponentRegistration)buildMethod.Invoke(null, null);
        }

        private static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    Store store;

                    try
                    {
                        store = c.Resolve<IStoreService>().GetAllStores().FirstOrDefault();
                    }
                    catch
                    {
                        if (!DataSettingsManager.DatabaseIsInstalled)
                            store = null;
                        else
                            throw;
                    }

                    var currentStoreId = store?.Id ?? Guid.Empty;

                    //uncomment the code below if you want load settings per store only when you have two stores installed.
                    //var currentStoreId = c.Resolve<IStoreService>().GetAllStores().Count > 1
                    //    c.Resolve<IStoreContext>().CurrentStore.Id : 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    try
                    {
                        return c.Resolve<ISettingService>().LoadSetting<TSettings>(currentStoreId);
                    }
                    catch
                    {
                        if (DataSettingsManager.DatabaseIsInstalled)
                            throw;
                    }

                    return default;
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        /// <summary>
        /// Is adapter for individual components
        /// </summary>
        public bool IsAdapterForIndividualComponents => false;
    }
}
