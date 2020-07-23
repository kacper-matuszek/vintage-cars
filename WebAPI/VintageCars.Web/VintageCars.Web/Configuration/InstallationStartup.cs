using System;
using System.Data;
using LinqToDB.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Logging;

namespace VintageCars.Web.Configuration
{
    public class InstallationStartup : INopStartup
    {
        private readonly INopFileProvider _fileProvider;
        private const string _installation = "Installation";
        private static IConfiguration _installationConfiguration;
        public InstallationStartup()
        {
            _fileProvider = CommonHelper.DefaultFileProvider;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if(DataSettingsManager.DatabaseIsInstalled)
                return;
            _installationConfiguration = configuration.GetSection(_installation);

           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!Enum.TryParse(_installationConfiguration["DataProvider"], out DataProviderType dataProviderType))
                throw new DataException("Wrong data provider type.");

            var dataProvider = DataProviderManager.GetDataProvider(dataProviderType);
            var connectionString = _installationConfiguration["ConnectionString"];

            if (connectionString.IsNullOrEmpty())
                throw new DataException("Connection string is wrong.");

            DataSettingsManager.SaveSettings(new DataSettings()
            {
                DataProvider = dataProviderType,
                ConnectionString = connectionString
            }, _fileProvider);

            DataSettingsManager.LoadSettings(reloadSettings: true);
            if (!dataProvider.IsDatabaseExists())
            {
                dataProvider.CreateDatabase(_installationConfiguration["Collation"]);
            }

            dataProvider.InitializeDatabase();
        }


        public int Order => 3;
    }
}
