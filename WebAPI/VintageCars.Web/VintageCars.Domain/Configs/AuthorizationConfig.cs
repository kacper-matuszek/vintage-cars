using System;
using System.Collections.Generic;
using System.Text;

namespace VintageCars.Domain.Configs
{
    public partial class AuthorizationConfig
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
    }
}
