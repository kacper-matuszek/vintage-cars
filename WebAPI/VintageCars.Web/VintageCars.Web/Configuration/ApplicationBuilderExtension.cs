using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Logging;
using VintageCars.Service.Infrastructure;

namespace VintageCars.Web.Configuration
{
    public static class ApplicationBuilderExtension
    {

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application, IWebHostEnvironment environment)
            => EngineContext.Current.ConfigureRequestPipeline(application, environment);

        public static void StartEngine(this IApplicationBuilder application)
        {
            var engine = EngineContext.Current;

            if (!DataSettingsManager.DatabaseIsInstalled)
                return;

            //log application start
            engine.Resolve<ILogger>().Information("Application started");

            //default caching
            engine.Resolve<IInfrastructureService>().SetDefaultCache();
        }
    }
}
