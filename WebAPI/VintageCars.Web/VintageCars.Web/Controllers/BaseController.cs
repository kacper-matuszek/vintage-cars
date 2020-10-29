using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Web.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected Guid UserId
        {
            get
            {
                Guid.TryParse(GetClaim(JwtRegisteredClaimNames.Sub), out var result);
                return result;
            }
        }

        public BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<TResult> SendAsync<TResult>(IRequest<TResult> query)
            => await _mediator.Send(query);

        protected async Task<TResult> SendAsync<TResult>(AuthorizationCommandBase<TResult> query)
            where TResult : class
        {
            query.UserId = UserId;
            return await _mediator.Send(query);
        }

        protected async Task<ActionResult> ExecuteCommandWithoutResult(IRequest command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        protected async Task<ActionResult> ExecuteCommandWithoutResult(AuthorizationCommandBase command)
        {
            command.UserId = UserId;
            await _mediator.Send(command);
            return Ok();
        }

        protected ActionResult<T> Single<T>(T data, bool allowNullData = false)
        {
            if (data == null && !allowNullData) return NotFound();
            return Ok(data);
        }

        private string GetClaim(string registeredClaim)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                return identity.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == registeredClaim))?.Value;
            }

            return null;
        }
    }
}