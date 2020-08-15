using System;
using System.Collections.Generic;
using System.Text;
using VintageCars.Domain.Settings.Attributes;

namespace VintageCars.Domain.Settings.Base
{
    [GoogleRecaptchaValidation]
    public abstract class GoogleRecaptchaBase
    {
        public string GoogleRecaptchaResponse { get; set; }
        public bool IsEnabled { get; set; }
    }
}
