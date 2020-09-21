using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Service.Tasks;
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

        public static void StartEngine(this IApplicationBuilder application, IApplicationLifetime applicationLifetime)
        {
            var engine = EngineContext.Current;

            if (!DataSettingsManager.DatabaseIsInstalled)
            {
                //if DatabaseIsInstalled is false after installing database, so we should restart app
                //after that useless services wont be loaded
                applicationLifetime.StopApplication();
                return;
            }

            //initialize and start schedule tasks
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();
            //log application start
            engine.Resolve<ILogger>().Information("Application started");

            //default caching
            engine.Resolve<IInfrastructureService>().SetDefaultCache();
        }
    }
}
