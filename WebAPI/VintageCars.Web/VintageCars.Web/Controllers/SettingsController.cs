using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Settings.Queries;

namespace VintageCars.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : BaseController
    {
        public SettingsController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet("captcha-key")]
        public async Task<ActionResult<string>> GetCaptchaKey()
            => Single(await SendAsync(new GetCaptchaKeyQuery()));
    }
}