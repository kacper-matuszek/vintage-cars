using Microsoft.Extensions.Configuration;

namespace Nop.Service.Installation
{
    public interface IInstallationService
    {
        public void InstallRequiredData();
    }
}