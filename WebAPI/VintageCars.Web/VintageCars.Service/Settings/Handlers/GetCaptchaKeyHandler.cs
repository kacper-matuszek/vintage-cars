using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Service.Settings;
using Nop.Service.Store;
using Nop.Services.Logging;
using VintageCars.Domain.Exceptions;
using VintageCars.Domain.Settings.Queries;
using VintageCars.Domain.Settings.Response;

namespace VintageCars.Service.Settings.Handlers
{
    public class GetCaptchaKeyHandler : IRequestHandler<GetCaptchaKeyQuery, CaptchaKeyResponse>
    {
        private readonly ISettingService _settingService;
        private readonly IStoreService _storeService;
        private readonly ILogger _logger;

        public GetCaptchaKeyHandler(ISettingService settingService, IStoreService storeService, ILogger logger)
        {
            _settingService = settingService ?? throw new ArgumentNullException(nameof(settingService));
            _storeService = storeService ?? throw new ArgumentNullException(nameof(storeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<CaptchaKeyResponse> Handle(GetCaptchaKeyQuery request, CancellationToken cancellationToken)
        {
            var storeId = _storeService.GetAllStores().FirstOrDefault()?.Id
                          ?? throw new ResourcesNotFoundException(typeof(Store));
            var captchaSettings = _settingService.LoadSetting<CaptchaSettings>(storeId);

            if (captchaSettings.Enabled) return Task.FromResult(new CaptchaKeyResponse(captchaSettings.ReCaptchaPublicKey));

            const string message = "Captcha settings is not enabled.";
            _logger.Warning(message);
            throw new ResourcesNotFoundException(typeof(CaptchaSettings), message);
        }
    }
}
