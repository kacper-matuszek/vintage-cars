using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ubiety.Dns.Core;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Exceptions;
using VintageCars.Domain.Exceptions.Response;

namespace VintageCars.Web.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<TResult> SendAsync<TResult>(IRequest<TResult> query)
            => await _mediator.Send(query);

        protected async Task<ActionResult> ExecuteCommandWithoutResult(IRequest command)
        {
            await _mediator.Send(command);
            return Ok();
        } 

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null) return NotFound();
            return Ok(data);
        }
    }
}