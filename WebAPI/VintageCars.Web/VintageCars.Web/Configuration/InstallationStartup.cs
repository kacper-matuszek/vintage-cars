using System;
using System.Data;
using LinqToDB.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Service.Installation;

namespace VintageCars.Web.Configuration
{
    public class InstallationStartup : INopStartup
    {
        private const string Installation = "Installation";
        private readonly INopFileProvider _fileProvider;
        private static IConfiguration _installationConfiguration;
        private IInstallationService _installationService;
        public InstallationStartup()
        {
            _fileProvider = CommonHelper.DefaultFileProvider;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if(DataSettingsManager.DatabaseIsInstalled)
                return;
            _installationConfiguration = configuration.GetSection(Installation);
            Singleton<DataSettings>.Instance = new DataSettings()
            {
                DataProvider = Enum.Parse<DataProviderType>(_installationConfiguration["DataProvider"]),
            };
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (DataSettingsManager.DatabaseIsInstalled)
                return;
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
            _installationService = EngineContext.Current.Resolve<IInstallationService>();
            _installationService.InstallRequiredData();
        }


        public int Order => 3;
    }
}
