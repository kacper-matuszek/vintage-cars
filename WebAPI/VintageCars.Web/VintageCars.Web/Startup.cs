using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using VintageCars.Web.Configuration;

namespace VintageCars.Web
{
    public class Startup
    {
        private IEngine _nopEngine;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _nopEngine = services.ConfigureApplicationServices(Configuration, _webHostEnvironment);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            _nopEngine.RegisterDependencies(builder);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApplicationLifetime applicationLifetime)
        {
            app.ConfigureRequestPipeline(env);
            app.StartEngine(applicationLifetime);
        }
    }
}
