using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Customer.Commands;

namespace VintageCars.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] CreateAccountCommand account)
            => await ExecuteCommandWithoutResult(account);
    }
}