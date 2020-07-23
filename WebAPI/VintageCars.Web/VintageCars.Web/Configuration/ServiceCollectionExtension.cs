using System;
using System.Linq;
using System.Net;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nop.Core;
using Nop.Core.Infrastructure;
using VintageCars.Domain.Configs;

namespace VintageCars.Web.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureSecurity(this IServiceCollection services,
            IConfiguration configuration)
        {
            var authorizationConfig = new AuthorizationConfig();
            configuration.Bind(nameof(AuthorizationConfig).Replace("Config", null),authorizationConfig);

            if(string.IsNullOrEmpty(authorizationConfig.SecretKey))
                throw new ArgumentNullException("Jwt secret key is empty.");

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationConfig.SecretKey)),
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidIssuer = authorizationConfig.Issuer
                    };
                });

            return services;
        }

        public static IMvcBuilder RegisterValidators(this IMvcBuilder mvcBuilder) =>
            mvcBuilder.AddFluentValidation(configuration =>
            {
                //register all available validators from Nop assemblies
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                    .OfType<AssemblyPart>()
                    .Where(part => part.Name.StartsWith("Nop", StringComparison.InvariantCultureIgnoreCase) || 
                                   part.Name.StartsWith("VintageCars", StringComparison.InvariantCultureIgnoreCase))
                    .Select(part => part.Assembly);
                configuration.RegisterValidatorsFromAssemblies(assemblies);

                //implicit/automatic validation of child properties
                configuration.ImplicitlyValidateChildProperties = true;
            });

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <param name="webHostEnvironment">Hosting environment</param>
        /// <returns>Configured service provider</returns>
        public static IEngine ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            services.ConfigureStartupConfig<AuthorizationConfig>(configuration);
            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //create engine and configure service provider
            var engine = EngineContext.Create();

            //create default file provider
            CommonHelper.DefaultFileProvider = new NopFileProvider(webHostEnvironment);

            engine.ConfigureServices(services, configuration);

            return engine;
        }

        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
