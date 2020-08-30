using Nop.Core.Domain.Security;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Settings.Response;

namespace VintageCars.Domain.Settings.Queries
{
    public class GetCaptchaKeyQuery : QueryBase<CaptchaKeyResponse>
    {
        public GetCaptchaKeyQuery()
        {
        }
    }
}
