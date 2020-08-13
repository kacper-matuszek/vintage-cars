using Nop.Core.Domain.Security;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Settings.Queries
{
    public class GetCaptchaKeyQuery : QueryBase<string>
    {
        public GetCaptchaKeyQuery()
        {
        }
    }
}
