using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Core.Infrastructure;
using Nop.Service.Settings;
using Nop.Service.Store;
using Nop.Services.Logging;
using VintageCars.Domain.Exceptions;
using VintageCars.Domain.Settings.Base;

namespace VintageCars.Domain.Settings.Attributes
{
    public class GoogleRecaptchaValidationAttribute : ValidationAttribute
    {
        private readonly ILogger _logger;

        public GoogleRecaptchaValidationAttribute()
        {
            _logger = EngineContext.Current.Resolve<ILogger>()
                      ?? throw new ArgumentNullException(nameof(ILogger));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is GoogleRecaptchaBase recaptchaModel))
                throw new Exception($"{GetType().Name} wrong object to valid !");

            var errorResult = new Lazy<ValidationResult>(
                () => new ValidationResult("Google reCaptcha validation failed",
                    new string[] {validationContext.MemberName}));
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return errorResult.Value;

            var settingService = EngineContext.Current.Resolve<ISettingService>()
                ?? throw new ArgumentNullException(nameof(ISettingService));
            var storeService = EngineContext.Current.Resolve<IStoreService>()
                ?? throw new ArgumentNullException(nameof(IStoreService));

            var storeId = storeService.GetAllStores().FirstOrDefault()?.Id
                          ?? throw new ResourcesNotFoundException(typeof(Store));
            var captchaSettings = settingService.LoadSetting<CaptchaSettings>(storeId);

            if(recaptchaModel.IsEnabled && !captchaSettings.Enabled)
                throw new ResourcesNotFoundException("Captcha settings is not enabled.");

            if (captchaSettings.Enabled != recaptchaModel.IsEnabled || !recaptchaModel.IsEnabled)
                return ValidationResult.Success;
            var client = new HttpClient();
            var httpResponse = client
                .GetAsync(
                    $"{captchaSettings.ReCaptchaApiUrl}?secret={captchaSettings.ReCaptchaPrivateKey}&response={recaptchaModel.GoogleRecaptchaResponse}")
                .Result;

            if (httpResponse.StatusCode != HttpStatusCode.OK)
                return errorResult.Value;

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
            dynamic jsonData = JObject.Parse(jsonResponse);
            return jsonData.success != true.ToString().ToLower() ? errorResult.Value : ValidationResult.Success;
        }
    }
}
