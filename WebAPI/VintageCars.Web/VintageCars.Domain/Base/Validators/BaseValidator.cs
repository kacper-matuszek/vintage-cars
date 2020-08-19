using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MediatR;
using Nop.Core.Infrastructure;
using Nop.Service.Localization;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Base.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
        where T : IRequest
    {
        private readonly ILocalizationService _localizationService;
        protected BaseValidator()
        {
            _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        }

        protected string GetMessageFromKey(string key)
            => _localizationService.GetResource(key);

        protected string GetMessageFromKey(string key, params object[] @objects)
        {
            var message = GetMessageFromKey(key);
            return string.Format(message, @objects);
        }
    }
}
