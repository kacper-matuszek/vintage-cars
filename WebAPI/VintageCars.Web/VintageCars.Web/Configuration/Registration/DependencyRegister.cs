using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Nop.Service.Installation;
using Nop.Service.Localization;
using Nop.Service.Settings;
using Nop.Service.Store;
using Nop.Services.Logging;
using VintageCars.Domain.Commands.Base;

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
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<CacheKeyService>().As<ICacheKeyService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();

            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();

            builder.RegisterSource(new SettingsSource());
            RegisterMediatR(builder);

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
