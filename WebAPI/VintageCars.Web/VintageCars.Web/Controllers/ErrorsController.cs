using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Logging;
using VintageCars.Domain.Exceptions;
using VintageCars.Domain.Exceptions.Response;

namespace VintageCars.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger _logger;

        public ErrorsController(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("error")]
        public ErrorDetails Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            var code = exception switch
            {
                ValidationException _ => 400,
                ResourcesNotFoundException _ => 404,
                UnexpectedException _ => 500,
                _ => 500,
            };

            Response.StatusCode = code;
            if(code == 500)
                _logger.Error(exception?.Message, exception);
            return code == 500 ? new ErrorDetails("Unexpected error. Contact with Administrator.", code) : new ErrorDetails(exception, code);
        }
    }
}
